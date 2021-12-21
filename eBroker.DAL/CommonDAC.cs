using eBroker.DAL.Interface;
using eBroker.Data.Database;
using eBroker.Data.Mapper;
using eBroker.Shared.DTOs;
using eBroker.Shared.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eBroker.DAL
{
    public class CommonDAC : ICommonDAC
    {
        private ObjectMapper mapper;
        private EBrokerDbContext dbContext;

        public CommonDAC()
        {
            mapper = new ObjectMapper();
            dbContext = new EBrokerDbContext();
        }

        public DataContainer<IList<TradeTypeDTO>> GetTradeType()
        {
            DataContainer<IList<TradeTypeDTO>> tradeTypeListObj = new DataContainer<IList<TradeTypeDTO>>();
            tradeTypeListObj.Data = MapTradeTypeList(dbContext.TradeType.ToList());

            return tradeTypeListObj;
        }
        public DataContainer<IList<TradeHistoryDTO>> GetTradeHistory(int userId)
        {
            DataContainer<IList<TradeHistoryDTO>> tradeTypeListObj = new DataContainer<IList<TradeHistoryDTO>>();
            tradeTypeListObj.Data = MapTradeHistoryList(dbContext.TradeHistory.ToList());

            return tradeTypeListObj;
        }

        #region Private Function
        private IList<TradeTypeDTO> MapTradeTypeList(List<TradeType> tradetype)
        {
            IList<TradeTypeDTO> returnData = new List<TradeTypeDTO>();
            foreach (var item in tradetype)
            {
                returnData.Add(mapper.MapTradeTypeToTradeTypeDTO(item));
            }

            return returnData;
        }

        private IList<TradeHistoryDTO> MapTradeHistoryList(List<TradeHistory> tradeHistory)
        {
            IList<TradeHistoryDTO> returnData = new List<TradeHistoryDTO>();
            foreach (var item in tradeHistory)
            {
                returnData.Add(mapper.MapTradeHistoryToTradeHistoryDTO(item));
            }

            return returnData;
        }
        #endregion
    }
}
