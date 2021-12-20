using eBroker.Shared.DTOs;
using eBroker.Shared.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace eBroker.DAL.Interface
{
    public interface IUserDAC
    {
        /// <summary>
        /// Check for user credentials in the database
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        DataContainer<UserDTO> AuthenticatUser(UserDTO user);
        
        /// <summary>
        /// Gets User Portfolio from database
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        DataContainer<UserDTO> GetUserPortfolio(int userId);
    }
}
