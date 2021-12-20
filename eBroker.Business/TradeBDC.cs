using eBroker.Business.Interface;
using eBroker.DAL;
using eBroker.DAL.Interface;
using eBroker.Shared.DTOs;
using eBroker.Shared.Enums;
using eBroker.Shared.Helpers;
using eBroker.Shared.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using static eBroker.Shared.Enums.Constants;

namespace eBroker.Business
{
    public class TradeBDC : ITradeBDC
    {
        private readonly TimeSpan OpenTime = new TimeSpan(9, 0, 0);
        private readonly TimeSpan CloseTime = new TimeSpan(15, 0, 0);
        private IDateTimeHelper _dateTimeHelper;
        private ITradeDAC tradeDAC;
        private IAccountBDC accountBDC;
        private DbContextOptions _dbContextOptions;

        #region Public Methods

        public TradeBDC(IDateTimeHelper dateTimeHelper = null, DbContextOptions dbContextOptions = null)
        {
            if(dateTimeHelper != null)
            {
                _dateTimeHelper = dateTimeHelper;
            }
            else
            {
                _dateTimeHelper = new DateTimeHelper();
            }

            if(dbContextOptions != null)
            {
                _dbContextOptions = dbContextOptions;
                tradeDAC = new TradeDAC(_dbContextOptions);
                accountBDC = new AccountBDC(_dbContextOptions);
            }
            else
            {
                tradeDAC = new TradeDAC();
                accountBDC = new AccountBDC();
            }
        }

        public DataContainer<IList<StockDTO>> GetAllStocks()
        {
            DataContainer<IList<StockDTO>> returnVal = new DataContainer<IList<StockDTO>>();
            try
            {
                returnVal = tradeDAC.GetAllStocks();

                if (!returnVal.isValidData)
                {
                    returnVal.Message = Constants.StocksNotAvailabe;
                }
            }
            catch (Exception ex)
            {

                returnVal.Message = BDCException + ex.Message;
            }
            return returnVal;
        }
        public DataContainer<bool> ValidateAndInitiateTrade(Trade tradeDetails, TradeType tradeType)
        {
            DataContainer<bool> returnValue = new DataContainer<bool>();
            DataContainer<bool> tradingPossible = isTradingPossible();

            if (tradingPossible.isValidData && tradingPossible.Data)
            {
                DataContainer<StockDTO> stockDetials = GetStockByID(tradeDetails.StockID);
                DataContainer<AccountDTO> accountDetails = GetUserDetails(tradeDetails.DmatAccountnumber);

                if (stockDetials.isValidData && accountDetails.isValidData)
                {
                    returnValue = ProcessTrade(tradeDetails, tradeType, stockDetials.Data, accountDetails.Data);
                }
                else if (!stockDetials.isValidData)
                {
                    returnValue.Data = false;
                    returnValue.isValidData = true;
                    returnValue.Message = StocksInvalid;
                }
                else if (!accountDetails.isValidData)
                {
                    returnValue.Data = false;
                    returnValue.isValidData = true;
                    returnValue.Message = AccountInvalid;
                }

                return returnValue;
            }
            else
            {
                return tradingPossible;
            }

        }
        public decimal CalculateBrokrage(decimal shareSellvalue)
        {
            decimal brokerage = shareSellvalue * (decimal)0.05;
            return brokerage < 20 ? 20 : brokerage;
        }

        #endregion 

        #region Private Helper Methods

        /// <summary>
        /// Checks if trading is within 9AM to 3PM & from Mon to Fri or not
        /// </summary>
        /// <returns></returns>
        private DataContainer<bool> isTradingPossible()
        {
            DateTime currentDateTime = _dateTimeHelper.GetDateTimeNow();
            TimeSpan currentTime = currentDateTime.TimeOfDay;
            DataContainer<bool> dataContainer = new DataContainer<bool>();

            if (currentDateTime.DayOfWeek == DayOfWeek.Saturday || currentDateTime.DayOfWeek == DayOfWeek.Sunday)
            {
                dataContainer.Data = false;
                dataContainer.Message = TradingCloseSatSunMessage;
            }
            else if (currentTime >= OpenTime && currentTime <= CloseTime)
            {
                dataContainer.Data = true;
                dataContainer.Message = TradingOpenMessage;
            }
            else
            {
                dataContainer.Data = false;
                dataContainer.Message = TradingCloseMessage;
            }
            dataContainer.isValidData = true;
            return dataContainer;
        }

        /// <summary>
        /// Get the Stock Information 
        /// </summary>
        /// <param name="stockID">Stock Id to get the inforation</param>
        /// <returns></returns>
        private DataContainer<StockDTO> GetStockByID(int stockID)
        {
            return tradeDAC.GetStockByID(stockID);
        }

        /// <summary>
        /// Get User Information From the database
        /// </summary>
        /// <param name="dmatId"></param>
        /// <returns></returns>
        private DataContainer<AccountDTO> GetUserDetails(string dmatId)
        {
            return accountBDC.GetAccountDetailsByDematID(dmatId);
        }

        /// <summary>
        /// Process Trading based on the requested trade type (Buy/Sell)
        /// </summary>
        /// <param name="tradeDetails"></param>
        /// <param name="tradeType"></param>
        /// <param name="stockDetials"></param>
        /// <param name="accountDetails"></param>
        /// <returns></returns>
        private DataContainer<bool> ProcessTrade(Trade tradeDetails, TradeType tradeType, StockDTO stockDetials, AccountDTO accountDetails)
        {
            DataContainer<bool> returnValue = new DataContainer<bool>();

            if(tradeType == TradeType.Buy)
            {
                returnValue = BuyStocks(tradeDetails, stockDetials, accountDetails);
            }
            else if(tradeType == TradeType.Sell)
            {
                returnValue = SellStocks(tradeDetails, stockDetials, accountDetails);
            }

            return returnValue;
        }

        /// <summary>
        /// Buy Stocks
        /// </summary>
        /// <param name="tradeDetails"></param>
        /// <param name="stockDetail"></param>
        /// <param name="accountDetails"></param>
        /// <returns></returns>
        private DataContainer<bool> BuyStocks(Trade tradeDetails, StockDTO stockDetail, AccountDTO accountDetails)
        {
            DataContainer<bool> returnValue = new DataContainer<bool>();
            decimal purchaseAmountRequired = stockDetail.Price * (decimal)tradeDetails.EquityQuantity;

            if(accountDetails.AvailableBalance >= purchaseAmountRequired)
            {
                returnValue.Data = tradeDAC.ProcessPurchase(accountDetails, tradeDetails, purchaseAmountRequired, _dateTimeHelper.GetDateTimeNow());
                returnValue.Message = returnValue.Data ? PurchaseSuccess : PurchaseFailed;
            }
            else
            {
                returnValue.Data = false;
                returnValue.Message = InsufficientFunds;
            }

            returnValue.isValidData = true;
            return returnValue;
        }

        /// <summary>
        /// Sell Stocks
        /// </summary>
        /// <param name="tradeDetails"></param>
        /// <param name="stockDetail"></param>
        /// <param name="accountDetails"></param>
        /// <returns></returns>
        private DataContainer<bool> SellStocks(Trade tradeDetails, StockDTO stockDetail, AccountDTO accountDetails)
        {
            DataContainer<bool> returnValue = new DataContainer<bool>();

            if(tradeDAC.IsStockInUserPortfolio(accountDetails.UserId, tradeDetails.StockID).Data)
            {
                if(tradeDAC.IsStockQuantitySufficeForSell(accountDetails.UserId, tradeDetails.StockID, tradeDetails.EquityQuantity).Data)
                {
                    decimal sellingGain = stockDetail.Price * (decimal)tradeDetails.EquityQuantity;
                    decimal brokerage = CalculateBrokrage(sellingGain);
                    sellingGain = sellingGain - brokerage;

                    returnValue.Data = tradeDAC.ProcessSelling(accountDetails, tradeDetails, sellingGain, brokerage, _dateTimeHelper.GetDateTimeNow());
                    returnValue.Message = returnValue.Data ? EquitySoldSuccess.Replace(SellingGainPlaceholder, sellingGain.ToString()).Replace(BrokeragePlaceholder, brokerage.ToString()) : SellingFailed;
                }
                else
                {
                    returnValue.Data = false;
                    returnValue.Message = LessEquityErrorMessage;
                }
            }
            else
            {
                returnValue.Data = false;
                returnValue.Message = EquityNotExist;
            }

            returnValue.isValidData = true;
            return returnValue;
        }
        
        #endregion
    }
}
