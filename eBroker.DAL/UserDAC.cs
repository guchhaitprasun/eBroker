using eBroker.DAL.Interface;
using eBroker.Data.Database;
using eBroker.Data.Mapper;
using eBroker.Shared.DTOs;
using eBroker.Shared.Enums;
using eBroker.Shared.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eBroker.DAL
{
    public class UserDAC : IUserDAC
    {
        private ObjectMapper mapper = null;
        public UserDAC()
        {
            mapper = new ObjectMapper();
        }


        public DataContainer<UserDTO> AuthenticatUser(UserDTO user)
        {
            DataContainer<UserDTO> authUser = new DataContainer<UserDTO>();

            try
            {
                using (var dbContext = new EBrokerDbContext())
                {
                    authUser.Data = mapper.MapUserToUserDTO(dbContext.User.Where(o => o.EmailAddress == user.EmailAddress && o.Password == user.Password).FirstOrDefault());
                    if(authUser.Data != null && authUser.Data.UserId > 0)
                    {
                        authUser.isValidData = true;
                        authUser.Message = Constants.LoginSuccess;
                    }
                    else
                    {
                        authUser.Message = Constants.LoginFailed;
                    }
                    
                }
            }
            catch (Exception ex)
            {
                authUser.Message = Constants.LoginFailed + " " + ex.Message;
            }

            return authUser;
        }

        public DataContainer<UserDTO> GetUserPortfolio(int userId)
        {
            DataContainer<UserDTO> userDetails = new DataContainer<UserDTO>();

            try
            {
                using (var dbContext = new EBrokerDbContext())
                {
                    User user = dbContext.User.Include("UserPortfolio").Include("UserPortfolio.Stock").FirstOrDefault(c => c.UserId == userId);
                    if (user != null && user.UserId > 0)
                    {
                        userDetails.Data = mapper.MapUserToUserDTO(user);
                        userDetails.isValidData = true;
                    }
                    else
                    {
                        userDetails.Data = new UserDTO();
                    }
                }
            }
            catch (Exception ex)
            {
                userDetails.Message = Constants.DACException + ex.Message;
            }

            return userDetails;
        }
    }
}
