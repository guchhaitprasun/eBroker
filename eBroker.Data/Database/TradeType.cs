using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace eBroker.Data.Database
{
    public partial class TradeType
    {
        public TradeType()
        {
            TradeHistory = new HashSet<TradeHistory>();
        }

        [Key]
        [Column("TypeID")]
        public int TypeId { get; set; }
        [StringLength(100)]
        public string TradeName { get; set; }
        public bool? IsActive { get; set; }

        [InverseProperty("TradeTypeNavigation")]
        public virtual ICollection<TradeHistory> TradeHistory { get; set; }
    }
}
