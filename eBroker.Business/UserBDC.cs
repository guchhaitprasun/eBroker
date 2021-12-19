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
                if (returnValue.isValidData)
                {
                    returnValue.Message = Constants.LoginSuccess;
                }
                else
                {
                    returnValue.Message = Constants.LoginFailed;
                }
            }
            catch (Exception ex)
            {
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

                if (!returnValue.isValidData)
                {
                    returnValue.Message = Constants.UserNotExist;
                }
                else if (returnValue.isValidData && returnValue.Data.UserPortfolioDTOs.Count <= 0)
                {
                    returnValue.isValidData = false;
                    returnValue.Message = Constants.PortfolioNotExist;
                }

            }
            catch (Exception ex)
            {
                returnValue.Message = Constants.BDCException + ex.Message;
            }

            return returnValue;

        }
    }
}
