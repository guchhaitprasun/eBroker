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
        /// <summary>
        /// Gets all the stock from the database
        /// </summary>
        /// <returns></returns>
        DataContainer<IList<StockDTO>> GetAllStocks();

        /// <summary>
        /// Get specefic stock based on ID
        /// </summary>
        /// <param name="stockID"></param>
        /// <returns></returns>
        DataContainer<StockDTO> GetStockByID(int stockID);

        /// <summary>
        /// Checks if Stock is in user portfolio or not
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="stockId"></param>
        /// <returns></returns>
        DataContainer<bool> IsStockInUserPortfolio(int userId, int stockId);

        /// <summary>
        /// Checks id stock in user portfolio suffice the sell request 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="stockId"></param>
        /// <param name="quantityToSell"></param>
        /// <returns></returns>
        DataContainer<bool> IsStockQuantitySufficeForSell(int userId, int stockId, int quantityToSell);

        /// <summary>
        /// Process Stock Purchase 
        /// </summary>
        /// <param name="account"></param>
        /// <param name="trade"></param>
        /// <param name="purchaseAmount"></param>
        /// <param name="tradeTime"></param>
        /// <returns></returns>
        bool ProcessPurchase(AccountDTO account, Trade trade, decimal purchaseAmount, DateTime tradeTime);

        /// <summary>
        /// Process Stock Selling
        /// </summary>
        /// <param name="account"></param>
        /// <param name="trade"></param>
        /// <param name="sellingGainAmount"></param>
        /// <param name="brokerage"></param>
        /// <param name="tradeTime"></param>
        /// <returns></returns>
        bool ProcessSelling(AccountDTO account, Trade trade, decimal sellingGainAmount, decimal brokerage, DateTime tradeTime);
    }
}
