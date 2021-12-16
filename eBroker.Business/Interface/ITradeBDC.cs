using eBroker.Shared.DTOs;
using eBroker.Shared.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using static eBroker.Shared.Enums.Constants;

namespace eBroker.Business.Interface
{
    public interface ITradeBDC
    {
        public DataContainer<IList<StockDTO>> GetAllStocks();

        public DataContainer<bool> ValidateAndInitiateTrade(Trade tradeDetails, TradeType tradeType);
    }
}
