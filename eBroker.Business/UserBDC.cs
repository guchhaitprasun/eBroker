using System;
using System.Collections.Generic;
using System.Text;
using eBroker.Shared.DTOs;
using eBroker.Shared.Helpers;
using eBroker.DAL;
using eBroker.Shared.Enums;

namespace eBroker.Business
{
    public class UserBDC
    {
        public DataContainer<UserDTO> AuthentictaeUser(UserDTO user)
        {
            DataContainer<UserDTO> returnValue = new DataContainer<UserDTO>();
            try
            {
                UserDAC userDAC = new UserDAC();
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
                UserDAC userDAC = new UserDAC();
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
