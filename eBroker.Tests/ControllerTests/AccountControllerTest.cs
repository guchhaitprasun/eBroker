using System;
using System.Collections.Generic;
using System.Text;
using eBroker.Shared.DTOs;
using eBroker.Shared.Enums;
using eBroker.Shared.Helpers;
using eBroker.WebAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace eBroker.Tests.ControllerTests
{
    public class AccountControllerTest
    {
        private readonly AccountsController Controller = null;

        public AccountControllerTest()
        {
            Controller = new AccountsController();
        }


        [Fact]
        public void GetUserAccountByDmatID_Returns_Correct_Account_Details_Status_200()
        {
            //Arrange 
            var dmatNumber = "1234-5678-9012-3456";
            var expectedStatusCode = 200; //Ok Result

            //Act
            var actionResult = Controller.GetUserAccountByDmatID(dmatNumber);

            //Assert
            var result = actionResult as ObjectResult;
            Assert.Equal(expectedStatusCode, result.StatusCode);
        }

        [Fact]
        public void GetUserAccountByDmatID_Returns_Empty_Account_Details_Status_400()
        {
            //Arrange 
            var dmatNumber = "1234567890123456";
            var expectedStatusCode = 400; //Bad Request

            //Act 
            var actionResult = Controller.GetUserAccountByDmatID(dmatNumber);

            //Assert
            var result = actionResult as ObjectResult;
            Assert.Equal(expectedStatusCode, result.StatusCode);
        }

        [Fact]
        public void AddFund_Returns_TransactionStatusMessage_Status_200()
        {
            //Arrange 
            var fundObj = new Fund{
                DmatNumber = "1234567890123456", 
                Amount = 1000
            };
            var expectedStatusCode = 200; //Success

            //Act 
            var actionResult = Controller.AddFundsInDMATAccount(fundObj);

            //Assert
            var result = actionResult as ObjectResult;
            Assert.Equal(expectedStatusCode, result.StatusCode);
        }
    }
}
