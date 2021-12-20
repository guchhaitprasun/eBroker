using eBroker.Shared.DTOs;
using eBroker.Shared.Enums;
using eBroker.Shared.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace eBroker.DAL.Interface
{
    public interface ITradeDAC
    {
        public DataContainer<IList<StockDTO>> GetAllStocks();

        public DataContainer<StockDTO> GetStockByID(int stockID);

        public DataContainer<bool> IsStockInUserPortfolio(int userId, int stockId);

        public DataContainer<bool> IsStockQuantitySufficeForSell(int userId, int stockId, int quantityToSell);

        public bool ProcessPurchase(AccountDTO account, Trade trade, decimal purchaseAmount, DateTime tradeTime);

        public bool ProcessSelling(AccountDTO account, Trade trade, decimal sellingGainAmount, decimal brokerage, DateTime tradeTime);
    }
}
