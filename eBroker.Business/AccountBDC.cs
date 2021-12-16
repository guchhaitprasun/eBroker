using eBroker.Business.Interface;
using eBroker.DAL;
using eBroker.DAL.Interface;
using eBroker.Shared.DTOs;
using eBroker.Shared.Enums;
using eBroker.Shared.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace eBroker.Business
{
    public class AccountBDC : IAccountBDC
    {
        public DataContainer<AccountDTO> GetAccountDetailsByDematID(string dmatNumber)
        {
            DataContainer<AccountDTO> returnValue = new DataContainer<AccountDTO>();
            try
            {
                IAccountDAC accountDAC = new AccountDAC();
                returnValue = accountDAC.GetAccountDetailsByDematID(dmatNumber);
            }
            catch (Exception ex)
            {
                returnValue.Message = Constants.BDCException + ex.Message;
            }
            return returnValue;
        }

        public DataContainer<bool> AddFunds(Fund fund)
        {
            DataContainer<bool> returnValue = new DataContainer<bool>();
            try
            {
                if(fund.Amount > 0)
                {
                    IAccountDAC accountDAC = new AccountDAC();
                    returnValue = accountDAC.AddFunds(fund.DmatNumber, fund.Amount);
                    returnValue.Message = returnValue.isValidData && returnValue.Data ? 
                        Constants.FundAddSuccess.Replace(Constants.Amount, fund.Amount.ToString()).Replace(Constants.DMATNumber, fund.DmatNumber).Replace(Constants.ProcessingCharges, fund.ProcessingCharges.ToString()) 
                        : Constants.DMATNotExist.Replace(Constants.DMATNumber, fund.DmatNumber);
                } else
                {
                    returnValue.Message = Constants.FundValidation;

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
