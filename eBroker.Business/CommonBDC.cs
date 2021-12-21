using eBroker.Business.Interface;
using eBroker.DAL;
using eBroker.DAL.Interface;
using eBroker.Shared.DTOs;
using eBroker.Shared.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace eBroker.Business
{
    public class CommonBDC : ICommonBDC
    {
        private ICommonDAC commonDAC;
        public CommonBDC()
        {
            commonDAC = new CommonDAC();
        }
        public DataContainer<IList<TradeTypeDTO>> GetTradeType()
        {
            return commonDAC.GetTradeType();
        }
        public DataContainer<IList<TradeHistoryDTO>> GetTradeHistory(int userId)
        {
            return commonDAC.GetTradeHistory(userId);
        }
    }
}
