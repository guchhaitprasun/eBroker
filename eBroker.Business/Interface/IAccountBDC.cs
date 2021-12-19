using eBroker.Shared.DTOs;
using eBroker.Shared.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace eBroker.Business.Interface
{
    public interface IAccountBDC
    {
        DataContainer<bool> AddFunds(Fund fund);

        DataContainer<AccountDTO> GetAccountDetailsByDematID(string dmatNumber);
    }
}
