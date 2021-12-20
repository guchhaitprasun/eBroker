using System;
using System.Collections.Generic;
using System.Text;

namespace eBroker.Shared.DTOs
{
    /// <summary>
    /// Container For User Portfolio details
    /// </summary>
    public class UserPortfolioDTO
    {
        public UserPortfolioDTO()
        {
            UserId = int.MinValue;
            StockId = int.MinValue;
            StockQty = int.MinValue;
            InvestedAmount = decimal.MinValue;
            IsActive = false;
            StockDTO = new StockDTO();
        }

        public int RecordId { get; set; }
        public int UserId { get; set; }
        public int StockId { get; set; }
        public int StockQty { get; set; }
        public decimal InvestedAmount { get; set; }
        public bool IsActive { get; set; }
        public StockDTO StockDTO { get; set; }
    }
}
