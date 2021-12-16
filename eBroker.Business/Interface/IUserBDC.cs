using eBroker.Shared.DTOs;
using eBroker.Shared.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace eBroker.Business.Interface
{
    public interface IUserBDC
    {
        public DataContainer<UserDTO> GetUserPortfolio(int userId);

        public DataContainer<UserDTO> AuthentictaeUser(UserDTO user);
    }
}
