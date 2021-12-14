using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace eBroker.Data.Database
{
    public partial class Account
    {
        [Key]
        [Column("AccountID")]
        public int AccountId { get; set; }
        [Column("UserID")]
        public int? UserId { get; set; }
        [StringLength(50)]
        public string DmatAccountNumber { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? AvailableBalance { get; set; }
        public bool? IsActive { get; set; }

        [ForeignKey(nameof(UserId))]
        [InverseProperty("Account")]
        public virtual User User { get; set; }
    }
}
