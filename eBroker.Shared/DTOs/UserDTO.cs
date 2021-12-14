using System;
using System.Collections.Generic;
using System.Text;

namespace eBroker.Shared.DTOs
{
    public class UserDTO
    {
        public UserDTO()
        {
            AccountDTOs = new HashSet<AccountDTO>();
            UserPortfolioDTOs = new HashSet<UserPortfolioDTO>();
            Password = "<encrypted>";
        }
        public int UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string EmailAddress { get; set; }

        public string Password { get; set; }

        public ICollection<UserPortfolioDTO> UserPortfolioDTOs { get; set; }

        public ICollection<AccountDTO> AccountDTOs { get; set; }

    }
}
