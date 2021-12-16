using eBroker.Shared.DTOs;
using eBroker.Shared.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace eBroker.DAL.Interface
{
    public interface IUserDAC
    {
        public DataContainer<UserDTO> AuthenticatUser(UserDTO user);
        public DataContainer<UserDTO> GetUserPortfolio(int userId);
    }
}
