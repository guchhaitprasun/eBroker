using eBroker.Shared.DTOs;
using eBroker.Shared.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace eBroker.Business.Interface
{
    public interface IAccountBDC
    {
        public DataContainer<bool> AddFunds(Fund fund);

        public DataContainer<AccountDTO> GetAccountDetailsByDematID(string dmatNumber);
    }
}
