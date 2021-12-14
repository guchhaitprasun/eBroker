using eBroker.DAL;
using eBroker.Shared.DTOs;
using eBroker.Shared.Enums;
using eBroker.Shared.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using static eBroker.Shared.Enums.Constants;

namespace eBroker.Business
{
    public class TradeBDC
    {
        private readonly TimeSpan OpenTime = new TimeSpan(9, 0, 0);
        private readonly TimeSpan CloseTime = new TimeSpan(15, 0, 0);

        public DataContainer<IList<StockDTO>> GetAllStocks()
        {
            DataContainer<IList<StockDTO>> returnVal = new DataContainer<IList<StockDTO>>();
            try
            {
                TradeDAC stockDac = new TradeDAC();
                returnVal = stockDac.GetAllStocks();
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
                else
                {
                    returnValue.Data = false;
                    returnValue.isValidData = true;
                    returnValue.Message = TradeFailed;
                }

                return returnValue;
            }
            else
            {
                return tradingPossible;
            }

        }

        #region Private Helper Methods

        private DataContainer<bool> isTradingPossible()
        {
            DateTime currentDateTime = DateTime.Now;
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

        private DataContainer<StockDTO> GetStockByID(int stockID)
        {
            TradeDAC tradeDac = new TradeDAC();
            return tradeDac.GetStockByID(stockID);
        }

        private DataContainer<AccountDTO> GetUserDetails(string dmatId)
        {
            AccountBDC accountBDC = new AccountBDC();
            return accountBDC.GetAccountDetailsByDematID(dmatId);
        }

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
            else
            {
                returnValue.Data = false;
                returnValue.isValidData = false;
                returnValue.Message = InavlidOperation;
            }

            return returnValue;
        }

        private DataContainer<bool> BuyStocks(Trade tradeDetails, StockDTO stockDetail, AccountDTO accountDetails)
        {
            DataContainer<bool> returnValue = new DataContainer<bool>();
            TradeDAC tradeDAC = new TradeDAC();
            decimal purchaseAmountRequired = stockDetail.Price * (decimal)tradeDetails.EquityQuantity;

            if(accountDetails.AvailableBalance >= purchaseAmountRequired)
            {
                returnValue.Data = tradeDAC.ProcessPurchase(accountDetails, tradeDetails, purchaseAmountRequired, TradeType.Buy);
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

        private DataContainer<bool> SellStocks(Trade tradeDetails, StockDTO stockDetail, AccountDTO accountDetails)
        {
            TradeDAC tradeDAC = new TradeDAC();
            DataContainer<bool> returnValue = new DataContainer<bool>();

            if(tradeDAC.IsStockInUserPortfolio(accountDetails.UserId, tradeDetails.StockID).Data)
            {
                if(tradeDAC.IsStockQuantitySufficeForSell(accountDetails.UserId, tradeDetails.StockID, tradeDetails.EquityQuantity).Data)
                {
                    decimal sellingGain = stockDetail.Price * (decimal)tradeDetails.EquityQuantity;
                    decimal brokerage = calculateBrokrage(sellingGain);
                    sellingGain = sellingGain - brokerage;

                    returnValue.Data = tradeDAC.ProcessSelling(stockDetail, accountDetails, tradeDetails, sellingGain, brokerage, TradeType.Sell);
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

        private decimal calculateBrokrage(decimal shareSellvalue)
        {
            decimal brokerage = shareSellvalue * (decimal)0.05;
            return brokerage < 20 ? 20 : brokerage;
        }
        #endregion
    }
}
