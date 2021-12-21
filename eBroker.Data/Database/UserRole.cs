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
    /// <summary>
    /// Excluding from code coverage since the object was not used in implementation yet
    /// </summary>
    [ExcludeFromCodeCoverage]
    public partial class UserRole
    {
        [Key]
        [Column("UserRoleID")]
        public int UserRoleId { get; set; }
        [Column("UserID")]
        public int? UserId { get; set; }
        [Column("RoleID")]
        public int? RoleId { get; set; }

        [ForeignKey(nameof(RoleId))]
        [InverseProperty("UserRole")]
        public virtual Role Role { get; set; }
        [ForeignKey(nameof(UserId))]
        [InverseProperty("UserRole")]
        public virtual User User { get; set; }
    }
}
