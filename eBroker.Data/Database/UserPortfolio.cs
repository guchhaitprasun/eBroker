using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace eBroker.Data.Database
{
    public partial class UserPortfolio
    {
        [Key]
        [Column("RecordID")]
        public int RecordId { get; set; }
        [Column("UserID")]
        public int? UserId { get; set; }
        [Column("StockID")]
        public int? StockId { get; set; }
        public int? StockQty { get; set; }
        [Column(TypeName = "decimal(12, 2)")]
        public decimal? InvestedAmount { get; set; }
        [Column("isActive")]
        public bool? IsActive { get; set; }

        [ForeignKey(nameof(StockId))]
        [InverseProperty("UserPortfolio")]
        public virtual Stock Stock { get; set; }
        [ForeignKey(nameof(UserId))]
        [InverseProperty("UserPortfolio")]
        public virtual User User { get; set; }
    }
}
