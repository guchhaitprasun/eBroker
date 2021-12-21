using eBroker.Shared.DTOs;
using eBroker.Shared.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace eBroker.Business.Interface
{
    public interface ICommonBDC
    {
        DataContainer<IList<TradeTypeDTO>> GetTradeType();
        DataContainer<IList<TradeHistoryDTO>> GetTradeHistory(int userId);
    }
}
