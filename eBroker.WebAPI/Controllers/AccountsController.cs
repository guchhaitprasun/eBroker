using eBroker.Business;
using eBroker.Business.Interface;
using eBroker.Shared.DTOs;
using eBroker.Shared.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eBroker.WebAPI.Controllers
{
    /// <summary>
    /// Accounts APIs Implementation
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private static IAccountBDC accountBDC;

        /// <summary>
        /// Constructor
        /// </summary>
        public AccountsController()
        {
            accountBDC = new AccountBDC();
        }

        /// <summary>
        /// Get account detials by DMAT Number 
        /// </summary>
        /// <param name="dmatID"></param>
        /// <returns></returns>
        [HttpGet, Route("Get/getAllAccountDetailsByDMATNumber/{dmatID}")]
        public IActionResult GetUserAccountByDmatID(string dmatID)
        {
            DataContainer<AccountDTO> accounts = accountBDC.GetAccountDetailsByDematID(dmatID);
            if (accounts.isValidData && accounts.Data.AccountId > 0)
            {
                return Ok(accounts.Data);
            }
            else
            {
                return BadRequest(new AccountDTO());
            }
        }

        /// <summary>
        /// Add Funds to the DMAT Account (Above 1L processing charges 0.05%)
        /// </summary>
        /// <param name="fund"></param>
        /// <returns></returns>
        [HttpPut, Route("Get/addFunds")]
        public IActionResult AddFundsInDMATAccount(Fund fund)
        {
            DataContainer<bool> response = accountBDC.AddFunds(fund);
            return Ok(response.Message);
        }
    }
}
