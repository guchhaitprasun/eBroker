using System;
using System.Collections.Generic;
using System.Text;

namespace eBroker.Shared.DTOs
{
    public class TradeTypeDTO
    {
        public int TypeId { get; set; }
        public string TradeName { get; set; }
        public bool IsActive { get; set; }
    }
}
