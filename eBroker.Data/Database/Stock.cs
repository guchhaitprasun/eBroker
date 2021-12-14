using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace eBroker.Data.Database
{
    public partial class Stock
    {
        public Stock()
        {
            TradeHistory = new HashSet<TradeHistory>();
            UserPortfolio = new HashSet<UserPortfolio>();
        }

        [Key]
        [Column("StockID")]
        public int StockId { get; set; }
        [StringLength(100)]
        public string StockName { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? Price { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? DayLow { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? DayHigh { get; set; }
        public bool? IsActive { get; set; }

        [InverseProperty("Stock")]
        public virtual ICollection<TradeHistory> TradeHistory { get; set; }
        [InverseProperty("Stock")]
        public virtual ICollection<UserPortfolio> UserPortfolio { get; set; }
    }
}
