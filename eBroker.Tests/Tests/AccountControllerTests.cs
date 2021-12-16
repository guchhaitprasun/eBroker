using System;
using System.Collections.Generic;
using System.Text;
using eBroker.Shared.DTOs;
using eBroker.Shared.Enums;
using eBroker.Shared.Helpers;
using eBroker.WebAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace eBroker.Tests.Tests
{
    public class AccountControllerTests
    {
        private readonly AccountsController Controller = null;

        public AccountControllerTests()
        {
            Controller = new AccountsController();
        }


        [Fact]
        public void GetUserAccountByDmatID_Returns_Correct_Account_Details_Status_200()
        {
            //Arrange 
            var dmatNumber = "1234-5678-9012-3456";
            var expected = 200; //Ok Result

            //Act
            var actionResult = Controller.GetUserAccountByDmatID(dmatNumber);

            //Assert
            var result = actionResult as ObjectResult;
            Assert.Equal(expected, result.StatusCode);
        }

        [Fact]
        public void GetUserAccountByDmatID_Returns_Empty_Account_Details_Status_400()
        {
            //Arrange 
            var dmatNumber = "1234567890123456";
            var expected = 400; //Bad Request

            //Act 
            var actionResult = Controller.GetUserAccountByDmatID(dmatNumber);

            //Assert
            var result = actionResult as ObjectResult;
            Assert.Equal(expected, result.StatusCode);
        }

        [Fact]
        public void HelperClass_StaticMethod_Validation_Return_0_for_ProcessingCharge_LessOrEqualTo_100000()
        {
            //Arrange
            int _Amount = 100000;
            int expected = 0;

            //Act
            Fund payload = new Fund
            {
                Amount = _Amount
            };

            //Assert
            Assert.Equal(expected, payload.ProcessingCharges);


        }

        [Fact]
        public void HelperClass_StaticMethod_Validation_Return_ProcessingCharge_for_Above_100000()
        {
            //Arrange
            int _Amount = 100001;
            decimal expected = 5000.05m;

            //Act
            Fund payload = new Fund
            {
                Amount = _Amount
            };

            //Assert
            Assert.Equal(expected, payload.ProcessingCharges);


        }

        [Fact]
        public void AddFundsToUserAccount_Returns_Validation_ErrorMessage_For_Minimum_Value()
        {
            //Arrange 
            Fund payload = new Fund
            {
                DmatNumber = "1234-5678-9012-3456",
                Amount = 0
            };
            var expected = "Min Rs 1 is required for fund addition";

            //Act 
            var actionResult = Controller.AddFundsInDMATAccount(payload);

            //Assert
            var result = actionResult as ObjectResult;
            Assert.Equal(expected, result.Value);
        }

        [Fact]
        public void AddFundsToUserAccount_Returns_Success_Message_For_Fund_Addition()
        {
            //Arrange 
            Fund payload = new Fund
            {
                DmatNumber = "1234-5678-9012-3456",
                Amount = 10000
            };
            var expected = Constants.FundAddSuccess.Replace(Constants.Amount, payload.Amount.ToString())
                .Replace(Constants.DMATNumber, payload.DmatNumber)
                .Replace(Constants.ProcessingCharges, payload.ProcessingCharges.ToString());

            //Act 
            var actionResult = Controller.AddFundsInDMATAccount(payload);

            //Assert
            var result = actionResult as ObjectResult;
            Assert.Equal(expected, result.Value);
        }

        [Fact]
        public void AddFundsToUserAccount_Returns_Success_Message_For_Fund_Addition_With_Processing_Charge_Above_100000()
        {
            //Arrange 
            Fund payload = new Fund
            {
                DmatNumber = "1234-5678-9012-3456",
                Amount = 100001
            };

            var expected = Constants.FundAddSuccess.Replace(Constants.Amount, payload.Amount.ToString())
                .Replace(Constants.DMATNumber, payload.DmatNumber)
                .Replace(Constants.ProcessingCharges, payload.ProcessingCharges.ToString());

            //Act 
            var actionResult = Controller.AddFundsInDMATAccount(payload);

            //Assert
            var result = actionResult as ObjectResult;
            Assert.Equal(expected, result.Value);
        }

        
    }
}
