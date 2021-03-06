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
    public partial class Role
    {
        public Role()
        {
            UserRole = new HashSet<UserRole>();
        }

        [Key]
        [Column("RoleID")]
        public int RoleId { get; set; }
        [StringLength(100)]
        public string RoleName { get; set; }
        public bool? IsAdminAccess { get; set; }

        [InverseProperty("Role")]
        public virtual ICollection<UserRole> UserRole { get; set; }
    }
}
