using eBroker.Data.Database;
using eBroker.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace eBroker.Data.Mapper
{
    public class ObjectMapper
    {
        public UserDTO MapUserToUserDTO(User user)
        {
            UserDTO userDetails = new UserDTO();
            if (user != null)
            {
                userDetails.UserId = user.UserId;
                userDetails.FirstName = user.FirstName;
                userDetails.LastName = user.LastName;
                userDetails.EmailAddress = user.EmailAddress;

                if (user.UserPortfolio != null && user.UserPortfolio.Count > 0)
                {
                    foreach (UserPortfolio portfolio in user.UserPortfolio)
                    {
                        userDetails.UserPortfolioDTOs.Add(MapUserPortfolioToUserPortfolioDTO(portfolio));
                    }
                }
            }

            return userDetails;
        }

        public StockDTO MapStockToStockDTO(Stock stock)
        {
            StockDTO stockDetail = new StockDTO();
            if (stock != null)
            {
                stockDetail.StockId = stock.StockId;
                stockDetail.StockName = stock.StockName;
                stockDetail.Price = stock.Price.HasValue ? stock.Price.Value : decimal.MinValue;
                stockDetail.DayLow = stock.DayLow.HasValue ? stock.DayLow.Value : decimal.MinValue;
                stockDetail.DayHigh = stock.DayHigh.HasValue ? stock.DayHigh.Value : decimal.MinValue;
                stockDetail.IsActive = stock.IsActive.HasValue ? stock.IsActive.Value : false;
            }

            return stockDetail;
        }

        public UserPortfolioDTO MapUserPortfolioToUserPortfolioDTO(UserPortfolio portfolio)
        {
            UserPortfolioDTO portfolioDTO = new UserPortfolioDTO();
            if (portfolio != null)
            {
                portfolioDTO.RecordId = portfolio.RecordId;
                portfolioDTO.UserId = portfolio.UserId.HasValue ? portfolio.UserId.Value : int.MinValue;
                portfolioDTO.StockId = portfolio.StockId.HasValue ? portfolio.StockId.Value : int.MinValue;
                portfolioDTO.StockQty = portfolio.StockQty.HasValue ? portfolio.StockQty.Value: int.MinValue;
                portfolioDTO.InvestedAmount = portfolio.InvestedAmount.HasValue ? portfolio.InvestedAmount.Value : decimal.MinValue;
                portfolioDTO.IsActive = portfolio.IsActive.HasValue ? portfolio.IsActive.Value : false;

                if (portfolio.Stock != null)
                {
                    portfolioDTO.StockDTO = MapStockToStockDTO(portfolio.Stock);
                }
            }

            return portfolioDTO;
        }

        public AccountDTO MapAccountsToAccountsDTO(Account account)
        {
            AccountDTO accountDTO = new AccountDTO();

            if (account != null)
            {
                accountDTO.AccountId = account.AccountId;
                accountDTO.UserId = account.UserId.HasValue ? account.UserId.Value : int.MinValue;
                accountDTO.DmatAccountNumber = account.DmatAccountNumber;
                accountDTO.AvailableBalance = account.AvailableBalance.HasValue ? account.AvailableBalance.Value : decimal.MinValue;
                accountDTO.IsActive = account.IsActive.HasValue ? account.IsActive.Value : false;
            }

            if(account.User != null)
            {
                accountDTO.user = MapUserToUserDTO(account.User);
            }

            return accountDTO;
        }

        public TradeTypeDTO MapTradeTypeToTradeTypeDTO(TradeType tradeType)
        {
            TradeTypeDTO tradeTypeDTO = new TradeTypeDTO();

            if (tradeType != null)
            {
                tradeTypeDTO.TypeId = tradeType.TypeId;
                tradeTypeDTO.TradeName = tradeType.TradeName;
                tradeTypeDTO.IsActive = tradeType.IsActive.HasValue ? tradeType.IsActive.Value : false;
            }

            return tradeTypeDTO;
        }

        public TradeHistoryDTO MapTradeHistoryToTradeHistoryDTO(TradeHistory tradeHistory)
        {
            TradeHistoryDTO tradeHistoryDTO = new TradeHistoryDTO();

            if (tradeHistory != null)
            {
                tradeHistoryDTO.TradeId = tradeHistory.TradeId;
                tradeHistoryDTO.UserId = tradeHistory.UserId;
                tradeHistoryDTO.TradeDate = tradeHistory.TradeDate;
                tradeHistoryDTO.TradeType = tradeHistory.TradeType;
                tradeHistoryDTO.StockId = tradeHistory.StockId;
                tradeHistoryDTO.StockQty = tradeHistory.StockQty;
                tradeHistoryDTO.Amount = tradeHistory.Amount;

                tradeHistoryDTO.Stock = MapStockToStockDTO(tradeHistory.Stock);
                tradeHistoryDTO.TradeTypeDetails = MapTradeTypeToTradeTypeDTO(tradeHistory.TradeTypeNavigation);
                tradeHistoryDTO.User = MapUserToUserDTO(tradeHistory.User);
            }

            return tradeHistoryDTO;
        }
    }
}
