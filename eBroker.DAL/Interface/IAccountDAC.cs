using eBroker.Shared.DTOs;
using eBroker.Shared.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace eBroker.DAL.Interface
{
    public interface IAccountDAC
    {
        public DataContainer<AccountDTO> GetAccountDetailsByDematID(string dmatNumber);

        public DataContainer<bool> AddFunds(string dmatNumber, decimal amount);
    }
}
