using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace eBroker.Data.Database
{
    public partial class TradeHistory
    {
        [Key]
        [Column("TradeID")]
        public int TradeId { get; set; }
        [Column("UserID")]
        public int? UserId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? TradeDate { get; set; }
        public int? TradeType { get; set; }
        [Column("StockID")]
        public int? StockId { get; set; }
        public int? StockQty { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? Amount { get; set; }

        [ForeignKey(nameof(StockId))]
        [InverseProperty("TradeHistory")]
        public virtual Stock Stock { get; set; }
        [ForeignKey(nameof(TradeType))]
        [InverseProperty("TradeHistory")]
        public virtual TradeType TradeTypeNavigation { get; set; }
        [ForeignKey(nameof(UserId))]
        [InverseProperty("TradeHistory")]
        public virtual User User { get; set; }
    }
}
