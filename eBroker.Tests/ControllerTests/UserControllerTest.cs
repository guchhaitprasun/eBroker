using eBroker.Shared.DTOs;
using eBroker.WebAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xunit;

namespace eBroker.Tests.ControllerTests
{
    /// <summary>
    /// SYSTEM UNDER TEST: eBroker.WebAPI (Presentation Layer)
    /// </summary>
    public class UserControllerTest
    {
        private readonly UserController Controller = null;

        public UserControllerTest()
        {
            Controller = new UserController();
        }

        [Fact, Description("Ensure Login API Called with status code 200")]
        public void UserAuthentcation_Returns_Success_Status_200()
        {
            //Arrange
            var userObj = new UserDTO
            {
                EmailAddress = "prasun.guchhait@nagp.com",
                Password = "Passw0rd"
            };
            var expectedStatusCode = 200; //Ok Result

            //Act
            var actionResult = Controller.LoginUser(userObj);


            //Assert
            var result = actionResult as ObjectResult;
            Assert.Equal(expectedStatusCode, result.StatusCode);
        }

        [Fact, Description("Ensure Login API Called with status code 400")]
        public void UserAuthentcation_Returns_Error_Status_400()
        {
            //Arrange
            var userObj = new UserDTO
            {
                EmailAddress = "prasun.guchhait@december.com",
                Password = "I dont Know"
            };
            var expectedStatusCode = 400; //Ok Result

            //Act
            var actionResult = Controller.LoginUser(userObj);


            //Assert
            var result = actionResult as ObjectResult;
            Assert.Equal(expectedStatusCode, result.StatusCode);
        }

        [Fact, Description("Ensure get user portfolio API Called with status code 200")]
        public void GetUserPortfolio_Returns_Success_Status_200()
        {
            //Arrange
            var userId = 20210;
            var expectedStatusCode = 200; //Ok Result

            //Act
            var actionResult = Controller.GetUserPortfolio(userId);


            //Assert
            var result = actionResult as ObjectResult;
            Assert.Equal(expectedStatusCode, result.StatusCode);
        }

        [Fact, Description("Ensure get user portfolio Called with status code 400")]
        public void GetUserPortfolio_Returns_Error_Status_400()
        {
            //Arrange
            var userId = 1;
            var expectedStatusCode = 400; //Ok Result

            //Act
            var actionResult = Controller.GetUserPortfolio(userId);


            //Assert
            var result = actionResult as ObjectResult;
            Assert.Equal(expectedStatusCode, result.StatusCode);
        }
    }
}
