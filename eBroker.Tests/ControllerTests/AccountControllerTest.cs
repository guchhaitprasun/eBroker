using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using eBroker.Shared.DTOs;
using eBroker.Shared.Enums;
using eBroker.Shared.Helpers;
using eBroker.WebAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace eBroker.Tests.ControllerTests
{
    /// <summary>
    /// SYSTEM UNDER TEST: eBroker.WebAPI (Presentation Layer)
    /// </summary>
    public class AccountControllerTest
    {
        private readonly AccountsController Controller = null;

        public AccountControllerTest()
        {
            Controller = new AccountsController();
        }


        [Fact, Description("Ensure valid status code is porvided for valid user")]
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

        [Fact, Description("Ensure Bad request status code on providing wrong DMAT Id")]
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

        [Fact, Description("Ensure Method can be called and providing valid HTTP Status code for Adding funds")]
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
