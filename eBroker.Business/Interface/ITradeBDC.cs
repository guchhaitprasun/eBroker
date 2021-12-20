using eBroker.Shared.DTOs;
using eBroker.Shared.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using static eBroker.Shared.Enums.Constants;

namespace eBroker.Business.Interface
{
    /// <summary>
    /// Interface for Trade Business Dev Centre 
    /// </summary>
    public interface ITradeBDC
    {
        /// <summary>
        /// Get Lists of Stock available for tradeing
        /// </summary>
        /// <returns></returns>
        DataContainer<IList<StockDTO>> GetAllStocks();

        /// <summary>
        /// Checks if Trading is possible and based on trade type call buy or sell stock processes.
        /// </summary>
        /// <param name="tradeDetails"></param>
        /// <param name="tradeType"></param>
        /// <returns></returns>
        DataContainer<bool> ValidateAndInitiateTrade(Trade tradeDetails, TradeType tradeType);

        /// <summary>
        /// Calculate brokerage on the amount user is selling in the market
        /// </summary>
        /// <param name="shareSellvalue"></param>
        /// <returns></returns>
        decimal CalculateBrokrage(decimal shareSellvalue);
    }
}
