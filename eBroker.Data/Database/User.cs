using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace eBroker.Data.Database
{
    public partial class User
    {
        public User()
        {
            Account = new HashSet<Account>();
            TradeHistory = new HashSet<TradeHistory>();
            UserPortfolio = new HashSet<UserPortfolio>();
            UserRole = new HashSet<UserRole>();
        }

        [Key]
        [Column("UserID")]
        public int UserId { get; set; }
        [StringLength(100)]
        public string FirstName { get; set; }
        [StringLength(100)]
        public string LastName { get; set; }
        [StringLength(100)]
        public string EmailAddress { get; set; }
        [StringLength(100)]
        public string Password { get; set; }

        [InverseProperty("User")]
        public virtual ICollection<Account> Account { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<TradeHistory> TradeHistory { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<UserPortfolio> UserPortfolio { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<UserRole> UserRole { get; set; }
    }
}
