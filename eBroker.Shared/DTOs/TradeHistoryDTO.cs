using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace eBroker.Shared.DTOs
{
    public class TradeHistoryDTO
    {
        public TradeHistoryDTO()
        {
            TradeDate = DateTime.Now;
            UserId = int.MinValue;
            TradeType = int.MinValue;
            StockId = int.MinValue;
            StockQty = int.MinValue;
            Amount = decimal.MinValue;

            Stock = new StockDTO();
            TradeTypeDetails = new TradeTypeDTO();
            User = new UserDTO();

        }

        public int TradeId { get; set; }
        public int? UserId { get; set; }
        public DateTime? TradeDate { get; set; }
        public int? TradeType { get; set; }
        public int? StockId { get; set; }
        public int? StockQty { get; set; }
        public decimal? Amount { get; set; }
        public StockDTO Stock { get; set; }
        public TradeTypeDTO TradeTypeDetails { get; set; }
        public UserDTO User { get; set; }
    }
}
