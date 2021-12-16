using System;
using System.Collections.Generic;
using System.Text;
using eBroker.Shared.DTOs;
using eBroker.Shared.Helpers;
using eBroker.DAL;
using eBroker.Shared.Enums;
using eBroker.DAL.Interface;
using eBroker.Business.Interface;

namespace eBroker.Business
{
    public class UserBDC : IUserBDC
    {
        public DataContainer<UserDTO> AuthentictaeUser(UserDTO user)
        {
            DataContainer<UserDTO> returnValue = new DataContainer<UserDTO>();
            try
            {
                IUserDAC userDAC = new UserDAC();
                returnValue = userDAC.AuthenticatUser(user);
            }
            catch (Exception ex)
            {

                returnValue.isValidData = false;
                returnValue.Message = Constants.BDCException + ex.Message;
            }

            return returnValue;
        }

        public DataContainer<UserDTO> GetUserPortfolio(int userId)
        {
            DataContainer<UserDTO> returnValue = new DataContainer<UserDTO>();
            try
            {
                IUserDAC userDAC = new UserDAC();
                returnValue = userDAC.GetUserPortfolio(userId);
            }
            catch (Exception ex)
            {

                returnValue.isValidData = false;
                returnValue.Message = Constants.BDCException + ex.Message;
            }

            return returnValue;

        }
    }
}
