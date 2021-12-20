using eBroker.Shared.DTOs;
using eBroker.Shared.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace eBroker.DAL.Interface
{
    public interface IAccountDAC
    {
        /// <summary>
        /// query for Account Details using the requested DMAT Id
        /// </summary>
        /// <param name="dmatNumber">Account Number assigned to user by stock brokers</param>
        /// <returns>Account Details of user with user Id, Available Balance etc. Wrapped inside a DataContainer class</returns>
        DataContainer<AccountDTO> GetAccountDetailsByDematID(string dmatNumber);

        /// <summary>
        /// query to Add Funds in database
        /// </summary>
        /// <param name="fund">fund Object comprise of information required for transaction</param>
        /// <returns>Success/Failure Flag for fund addition</returns>
        DataContainer<bool> AddFunds(string dmatNumber, decimal amount);
    }
}
