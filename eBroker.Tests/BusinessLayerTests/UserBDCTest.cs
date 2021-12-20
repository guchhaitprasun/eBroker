using eBroker.Business;
using eBroker.Business.Interface;
using eBroker.Shared.DTOs;
using eBroker.Shared.Enums;
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
    public class UserBDCTest
    {
        private readonly IUserBDC userBDC;

        public UserBDCTest()
        {
            userBDC = new UserBDC();
        }

        [Fact, Description("Ensure success message is provided on valid user authentication")]
        public void UserAuthentcation_Returns_Success_Message()
        {
            //Arrange
            var userObj = new UserDTO
            {
                EmailAddress = "prasun.guchhait@nagp.com",
                Password = "Passw0rd"
            };
            var expectedStatusMessage = Constants.LoginSuccess;

            //Act
            var result = userBDC.AuthentictaeUser(userObj);


            //Assert
            Assert.Equal(expectedStatusMessage, result.Message);
        }

        [Fact, Description("Ensure error message is provided on invalid user authentication")]
        public void UserAuthentcation_Returns_Error_Message()
        {
            //Arrange
            var userObj = new UserDTO
            {
                EmailAddress = "prasun.guchhait@december.com",
                Password = "I dont Know"
            };
            var expectedStatusMessage = Constants.LoginFailed;

            //Act
            var result = userBDC.AuthentictaeUser(userObj);


            //Assert
            Assert.Equal(expectedStatusMessage, result.Message);
        }

        [Fact, Description("Ensure portfolio is provided for valid user")]
        public void GetUserPortfolio_Returns_Success_PortfolioList_for_ValidUser()
        {
            //Arrange
            var userId = 20210;

            //Act
            var result = userBDC.GetUserPortfolio(userId);

            //Assert
            Assert.NotEmpty(result.Data.UserPortfolioDTOs);
        }

        [Fact, Description("Ensure proper error message is provided on requesting portfoli for valid user")]
        public void GetUserPortfolio_Returns_Error_Message_For_ValidUser()
        {
            //Arrange
            var userId = 20211;
            var expectedStatusMessage = Constants.PortfolioNotExist;

            //Act
            var result = userBDC.GetUserPortfolio(userId);

            //Assert
            Assert.Equal(expectedStatusMessage, result.Message);
        }

        [Fact, Description("Ensure proper error message is provided on requesting portfoli for invalid user")]
        public void GetUserPortfolio_Returns_Error_Message_For_InvalidValidUser()
        {
            //Arrange
            var userId = 211;
            var expectedStatusMessage = Constants.UserNotExist;

            //Act
            var result = userBDC.GetUserPortfolio(userId);

            //Assert
            Assert.Equal(expectedStatusMessage, result.Message);
        }
    }
}
