using System;
using System.Collections.Generic;
using System.Text;

namespace eBroker.Shared.DTOs
{
    public class StockDTO
    {
        public StockDTO()
        {
            Price = decimal.MinValue;
            DayLow = decimal.MinValue;
            DayHigh = decimal.MinValue;
            IsActive = false;
        }

        public int StockId { get; set; }
        public string StockName { get; set; }
        public decimal Price { get; set; }
        public decimal DayLow { get; set; }
        public decimal DayHigh { get; set; }
        public bool IsActive { get; set; }
    }
}
