using eBroker.Shared.DTOs;
using eBroker.Shared.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace eBroker.Business.Interface
{
    /// <summary>
    /// Interface for Account Business Dev Centre
    /// </summary>
    public interface IAccountBDC
    {
        /// <summary>
        /// Function to Add Funds in user account
        /// </summary>
        /// <param name="fund">fund Object comprise of information required for transaction</param>
        /// <returns>Success/Failure Flag for fund addition</returns>
        DataContainer<bool> AddFunds(Fund fund);

        /// <summary>
        /// Get Account Details for the requested DMAT Id
        /// </summary>
        /// <param name="dmatNumber">Account Number assigned to user by stock brokers</param>
        /// <returns>Account Details of user with user Id, Available Balance etc. Wrapped inside a DataContainer class</returns>
        DataContainer<AccountDTO> GetAccountDetailsByDematID(string dmatNumber);
    }
}
