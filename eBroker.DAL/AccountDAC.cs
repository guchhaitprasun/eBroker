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
    /// <summary>
    /// Data Access Center for Accounts
    /// </summary>
    public class AccountDAC : IAccountDAC
    {
        private ObjectMapper Mapper = null;
        private EBrokerDbContext dbContext;

        /// <summary>
        /// Constructor Overload
        /// </summary>
        public AccountDAC()
        {
            Mapper = new ObjectMapper();
            dbContext = new EBrokerDbContext();
        }

        /// <summary>
        /// Constructor Overload for datbase mocking and using in memory database
        /// </summary>
        /// <param name="dbContextOptions"></param>
        public AccountDAC(DbContextOptions dbContextOptions)
        {
            var _dbContextOptions = (DbContextOptions<EBrokerDbContext>)dbContextOptions;
            Mapper = new ObjectMapper();
            dbContext = new EBrokerDbContext(_dbContextOptions);
        }

 
        public DataContainer<AccountDTO> GetAccountDetailsByDematID(string dmatNumber)
        {
            DataContainer<AccountDTO> response = new DataContainer<AccountDTO>();
            try
            {
                var data = dbContext.Account.Where(o => o.DmatAccountNumber == dmatNumber).FirstOrDefault();
                if (data != null)
                {
                    response.Data = Mapper.MapAccountsToAccountsDTO(data);
                    response.isValidData = true;
                }   
            }
            catch (Exception ex)
            {
                response.Message = Constants.DACException + ex.Message;
            }

            return response;
        }

        public DataContainer<bool> AddFunds(string dmatNumber, decimal amount)
        {
            DataContainer<bool> response = new DataContainer<bool>();

            try
            {
                var account = dbContext.Account.Where(o => o.DmatAccountNumber == dmatNumber && o.IsActive.HasValue && o.IsActive.Value).FirstOrDefault();
                if (account != null && account.UserId > 0)
                {
                    account.AvailableBalance = account.AvailableBalance + amount;
                    response.Data = dbContext.SaveChanges() > 0 ? true : false;
                    response.isValidData = true;
                }
                else
                {
                    response.Data = false;
                }
            }
            catch (Exception ex)
            {
                response.Message = Constants.DACException + ex.Message;
            }

            return response;
        }
    }
}
