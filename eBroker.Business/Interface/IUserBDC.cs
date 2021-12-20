using eBroker.Shared.DTOs;
using eBroker.Shared.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace eBroker.Business.Interface
{
    public interface IUserBDC
    {
        /// <summary>
        /// Get all the stocks aquired by user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        DataContainer<UserDTO> GetUserPortfolio(int userId);

        /// <summary>
        /// Verify the credentials (just a mock for login)
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        DataContainer<UserDTO> AuthentictaeUser(UserDTO user);
    }
}
