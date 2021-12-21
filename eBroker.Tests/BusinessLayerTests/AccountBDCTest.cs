using eBroker.Business;
using eBroker.Business.Interface;
using eBroker.Shared.Enums;
using eBroker.Shared.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xunit;

namespace eBroker.Tests.BusinessLayerTests
{
    /// <summary>
    /// SYSTEM UNDER TEST: eBroker.Business (Business Layer)
    /// </summary>
    public class AccountBDCTest
    {
        private readonly IAccountBDC accountBDC;

        public AccountBDCTest()
        {
            accountBDC = new AccountBDC();
        }

        [Fact, Description("Ensure valid account details provided for valid DMAT number")]
        public void GetAccountDetailsByDematID_Returns_AccountDetails_Success()
        {
            //Arrange
            var dmatNumber = "1234-5678-9012-3456";

            //Act
            var result = accountBDC.GetAccountDetailsByDematID(dmatNumber);

            //Assert
            Assert.NotNull(result.Data);
        }

        [Fact, Description("Ensure no details provided for invalid DMAT number")]
        public void GetAccountDetailsByDematID_Returns_AccountDetails_Failure()
        {
            //Arrange
            var dmatNumber = "1234567890123456";

            //Act
            var result = accountBDC.GetAccountDetailsByDematID(dmatNumber);

            //Assert
            Assert.Null(result.Data);
        }

        [Fact, Description("Ensure validation message on requesting negative funds addition")]
        public void AddFunds_Returns_Validation_ErrorMessage_For_Minimum_Value()
        {
            //Arrange 
            Fund payload = new Fund
            {
                DmatNumber = "1234-5678-9012-3456",
                Amount = 0
            };
            var expectedMessage = "Min Rs 1 is required for fund addition";

            //Act 
            var result = accountBDC.AddFunds(payload);

            //Assert
            Assert.Equal(expectedMessage, result.Message);
        }

        [Fact, Description("Ensure validation message on requesting negative funds addition")]
        public void AddFunds_Returns_Exception()
        {
            //Arrange 
            var expectedMessage = "Logical Operations Exception on BDC \n Object reference not set to an instance of an object.";

            //Act 
            var result = accountBDC.AddFunds(null);

            //Assert
            Assert.Equal(expectedMessage, result.Message);
        }

        [Fact, Description("Ensure valid message on adding funds")]
        public void AddFunds_Returns_Success_Message_For_Fund_Addition()
        {
            //Arrange 
            Fund payload = new Fund
            {
                DmatNumber = "1234-5678-9012-3456",
                Amount = 100000
            };
            var expectedMessage = Constants.FundAddSuccess.Replace(Constants.Amount, payload.Amount.ToString())
                .Replace(Constants.DMATNumber, payload.DmatNumber)
                .Replace(Constants.ProcessingCharges, payload.ProcessingCharges.ToString());

            //Act 
            var result = accountBDC.AddFunds(payload);

            //Assert
            Assert.Equal(expectedMessage, result.Message);
        }

        [Fact, Description("Ensure valid message on fund addition with processing charges")]
        public void AddFunds_Returns_Success_Message_For_Fund_Addition_With_Processing_Charge_Above_100000()
        {
            //Arrange 
            Fund payload = new Fund
            {
                DmatNumber = "1234-5678-9012-3456",
                Amount = 100001
            };

            var expectedMessage = Constants.FundAddSuccess.Replace(Constants.Amount, payload.Amount.ToString())
                .Replace(Constants.DMATNumber, payload.DmatNumber)
                .Replace(Constants.ProcessingCharges, payload.ProcessingCharges.ToString());

            //Act 
            var result = accountBDC.AddFunds(payload);

            //Assert
            Assert.Equal(expectedMessage, result.Message);
        }
    }
}
