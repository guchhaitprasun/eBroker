using eBroker.Business;
using eBroker.Business.Interface;
using eBroker.Shared.Enums;
using eBroker.Shared.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace eBroker.Tests.BusinessLayerTests
{
    public class AccountBDCTest
    {
        private readonly IAccountBDC accountBDC;

        public AccountBDCTest()
        {
            accountBDC = new AccountBDC();
        }

        [Fact]
        public void GetAccountDetailsByDematID_Returns_AccountDetails_Success()
        {
            //Arrange
            var dmatNumber = "1234-5678-9012-3456";

            //Act
            var result = accountBDC.GetAccountDetailsByDematID(dmatNumber);

            //Assert
            Assert.NotNull(result.Data);
        }

        [Fact]
        public void GetAccountDetailsByDematID_Returns_AccountDetails_Failure()
        {
            //Arrange
            var dmatNumber = "1234567890123456";

            //Act
            var result = accountBDC.GetAccountDetailsByDematID(dmatNumber);

            //Assert
            Assert.Null(result.Data);
        }

        [Fact]
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

        [Fact]
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

        [Fact]
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
