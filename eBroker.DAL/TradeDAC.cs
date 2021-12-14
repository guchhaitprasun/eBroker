using eBroker.Data.Database;
using eBroker.Data.Mapper;
using eBroker.Shared.DTOs;
using eBroker.Shared.Enums;
using eBroker.Shared.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eBroker.DAL
{
    public class TradeDAC
    {
        private ObjectMapper mapper = null;

        public TradeDAC()
        {
            mapper = new ObjectMapper();
        }

        public DataContainer<IList<StockDTO>> GetAllStocks()
        {
            DataContainer<IList<StockDTO>> returnVal = new DataContainer<IList<StockDTO>>();
            try
            {
                using (var dbContext = new EBrokerDbContext())
                {
                    returnVal.Data = MapStockList(dbContext.Stock.Where(o => o.IsActive.HasValue && o.IsActive.Value).ToList());
                    if (returnVal.Data.Count > 0)
                    {
                        returnVal.isValidData = true;
                    }
                    else
                    {
                        returnVal.Message = Constants.StocksNotAvailabe;
                    }
                }
            }
            catch (Exception ex)
            {
                returnVal.Message = Constants.DACException + ex.Message;
            }

            return returnVal;
        }

        public DataContainer<StockDTO> GetStockByID(int stockID)
        {
            DataContainer<StockDTO> returnVal = new DataContainer<StockDTO>();

            try
            {
                using (var dbContext = new EBrokerDbContext())
                {
                    var data = dbContext.Stock.Where(o => o.StockId == stockID && o.IsActive.HasValue && o.IsActive.Value).FirstOrDefault();
                    if (data != null)
                    {
                        returnVal.Data = mapper.MapStockToStockDTO(data);
                        returnVal.isValidData = true;
                    }
                    else
                    {
                        returnVal.Message = Constants.StocksInvalid;
                    }
                }
            }
            catch (Exception ex)
            {
                returnVal.Message = Constants.DACException + ex.Message;
            }

            return returnVal;

        }

        public DataContainer<bool> IsStockInUserPortfolio(int userId, int stockId)
        {
            DataContainer<bool> retVal = new DataContainer<bool>();
            try
            {
                using (var dbContext = new EBrokerDbContext())
                {
                    retVal.Data = dbContext.UserPortfolio.Any(o => o.UserId == userId && o.StockId == stockId);
                    retVal.isValidData = true;
                }

            }
            catch (Exception ex)
            {

                retVal.Message = Constants.DACException + ex.Message;
            }

            return retVal;
        }

        public DataContainer<bool> IsStockQuantitySufficeForSell(int userId, int stockId, int quantityToSell)
        {
            DataContainer<bool> retVal = new DataContainer<bool>();
            try
            {
                using (var dbContext = new EBrokerDbContext())
                {
                    retVal.Data = dbContext.UserPortfolio.Any(o => o.UserId == userId && o.StockId == stockId && o.StockQty >= quantityToSell);
                    retVal.isValidData = true;
                }
            }
            catch (Exception ex)
            {

                retVal.Message = Constants.DACException + ex.Message;
            }

            return retVal;

        }

        public bool ProcessPurchase(AccountDTO account, Trade trade, decimal purchaseAmount, Constants.TradeType tradeType)
        {
            return UpdateAccountDetails(account, purchaseAmount) && AddTradeHistory(account, trade, purchaseAmount, tradeType) && AddUpdateUserPortfolio(account, trade, purchaseAmount);
        }

        public bool ProcessSelling(StockDTO stocks, AccountDTO account, Trade trade, decimal sellingGainAmount, decimal brokerage, Constants.TradeType tradeType)
        {
            return UpdateAccountDetails(account, -sellingGainAmount) && AddTradeHistory(account, trade, sellingGainAmount, tradeType) && AddUpdateUserPortfolioAfterSell(account, trade, sellingGainAmount + brokerage);
        }




        #region Private Helper Functions
        private IList<StockDTO> MapStockList(IList<Stock> stockList)
        {
            IList<StockDTO> _stockList = new List<StockDTO>();
            foreach (Stock stock in stockList)
            {
                _stockList.Add(mapper.MapStockToStockDTO(stock));
            }

            return _stockList;
        }

        #endregion

        #region Private Database Functions

        private bool UpdateAccountDetails(AccountDTO account, decimal purchaseAmount)
        {
            using (var dbContext = new EBrokerDbContext())
            {
                var userAccount = dbContext.Account.Where(o => o.UserId == account.UserId && o.DmatAccountNumber == account.DmatAccountNumber).FirstOrDefault();
                if (userAccount != null)
                {
                    userAccount.AvailableBalance = userAccount.AvailableBalance - purchaseAmount;
                    dbContext.SaveChanges();
                    return true;
                }

                return false;
            }
        }

        private bool AddTradeHistory(AccountDTO account, Trade trade, decimal purchaseAmount, Constants.TradeType tradeType)
        {
            using (var dbContext = new EBrokerDbContext())
            {
                TradeHistory details = new TradeHistory();

                details.UserId = account.UserId;
                details.TradeDate = DateTime.Now;
                details.TradeType = (int)tradeType;
                details.StockId = trade.StockID;
                details.StockQty = trade.EquityQuantity;
                details.Amount = purchaseAmount;

                dbContext.TradeHistory.Add(details);
                dbContext.SaveChanges();

                return true;
            }
        }

        private bool AddUpdateUserPortfolio(AccountDTO account, Trade tradeDetails, decimal purchaseAmount)
        {
            using (var dbContext = new EBrokerDbContext())
            {
                var portfolio = dbContext.UserPortfolio.Where(o => o.UserId == account.UserId && o.StockId == tradeDetails.StockID).FirstOrDefault();

                if (portfolio != null)
                {
                    portfolio.StockQty = portfolio.StockQty + tradeDetails.EquityQuantity;
                    portfolio.InvestedAmount = portfolio.InvestedAmount + purchaseAmount;
                }
                else
                {
                    UserPortfolio _portfolio = new UserPortfolio();
                    _portfolio.UserId = account.UserId;
                    _portfolio.StockId = tradeDetails.StockID;
                    _portfolio.StockQty = tradeDetails.EquityQuantity;
                    _portfolio.InvestedAmount = purchaseAmount;
                    _portfolio.IsActive = true;

                    dbContext.UserPortfolio.Add(_portfolio);
                }

                dbContext.SaveChanges();
                return true;
            }
        }

        private bool AddUpdateUserPortfolioAfterSell(AccountDTO account, Trade tradeDetails, decimal sellAmount)
        {
            using (var dbContext = new EBrokerDbContext())
            {
                var portfolio = dbContext.UserPortfolio.Where(o => o.UserId == account.UserId && o.StockId == tradeDetails.StockID).FirstOrDefault();

                if (portfolio != null)
                {
                    int remainingStocks = portfolio.StockQty.Value - tradeDetails.EquityQuantity;
                    portfolio.StockQty = remainingStocks;
                    portfolio.IsActive = remainingStocks > 0 ? true : false;
                    portfolio.InvestedAmount = portfolio.InvestedAmount - sellAmount;
                }
                else
                {
                    return false;
                }
                dbContext.SaveChanges();
                return true;
            }
        }
        #endregion

    }
}
