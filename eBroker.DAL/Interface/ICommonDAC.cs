using eBroker.Shared.DTOs;
using eBroker.Shared.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace eBroker.DAL.Interface
{
    public interface ICommonDAC
    {
        DataContainer<IList<TradeTypeDTO>> GetTradeType();
        DataContainer<IList<TradeHistoryDTO>> GetTradeHistory(int userId);
    }
}
