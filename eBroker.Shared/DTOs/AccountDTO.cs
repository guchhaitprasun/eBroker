using System;
using System.Collections.Generic;
using System.Text;

namespace eBroker.Shared.DTOs
{
    /// <summary>
    /// Container for Account details
    /// </summary>
    public class AccountDTO
    {

        public AccountDTO()
        {
            AccountId = int.MinValue;
            UserId = int.MinValue;
            DmatAccountNumber = String.Empty;
            AvailableBalance = decimal.MinValue;
            IsActive = false;
        }

        public int AccountId { get; set; }
        public int UserId { get; set; }
        public string DmatAccountNumber { get; set; }
        public decimal AvailableBalance { get; set; }
        public bool IsActive { get; set; }
    }
}
