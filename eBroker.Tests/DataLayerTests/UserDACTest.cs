using eBroker.DAL;
using eBroker.Data.Database;
using eBroker.Shared.DTOs;
using eBroker.Shared.Enums;
using eBroker.Tests.InMemoryData;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xunit;

namespace eBroker.Tests.DataLayerTests
{
    /// <summary>
    /// SYSTEM UNDER TEST: eBroker.DAL (Data Layer)
    /// </summary>
    public class UserDACTest
    {
        private DbContextOptions<EBrokerDbContext> options;
        private InMemoryDBContext inMemoryDBContext;
        public UserDACTest()
        {
            inMemoryDBContext = new InMemoryDBContext();
            options = inMemoryDBContext.Options;
        }

        [Fact, Description("Ensure valid data is provided on providing valid credentials")]
        public void UserAuthentcation_Returns_ValidData()
        {
            //Arrange
            var userObj = new UserDTO
            {
                EmailAddress = "visualStudioTest@nagp.com",
                Password = "Passw0rd"
            };
            var userDAC = new UserDAC(options);

            //Act
            var result = userDAC.AuthenticatUser(userObj);


            //Assert
            Assert.True(result.isValidData);

        }

        [Fact, Description("Ensures valid data flag sets to false for invalid credentials")]
        public void UserAuthentcation_Returns_InValidData()
        {
            //Arrange
            var userObj = new UserDTO
            {
                EmailAddress = "invalidUser@nagp.com",
                Password = "Passw0rd"
            };
            var userDAC = new UserDAC(options);

            //Act
            var result = userDAC.AuthenticatUser(userObj);


            //Assert
            Assert.False(result.isValidData);

        }

        [Fact, Description("Ensure portfolio list is provided if exist")]
        public void GetUserPortfolio_ValidUser_NonEmptyPortfolio_List()
        {
            //Arrange
            var userId = 100;
            var userDAC = new UserDAC(options);

            //Act
            var result = userDAC.GetUserPortfolio(userId);

            //Assert
            Assert.NotEmpty(result.Data.UserPortfolioDTOs);
        }

        [Fact, Description("Ensure Empty list provided if portfolio not exist")]
        public void GetUserPortfolio_ValidUser_EmptyPortfolio_List()
        {
            //Arrange
            var userId = 101;
            var userDAC = new UserDAC(options);

            //Act
            var result = userDAC.GetUserPortfolio(userId);

            //Assert
            Assert.Empty(result.Data.UserPortfolioDTOs);
        }

        [Fact, Description("Ensure valid flag set to false for invalid user")]
        public void GetUserPortfolio_InValidUser()
        {
            //Arrange
            var userId = 102;
            var userDAC = new UserDAC(options);

            //Act
            var result = userDAC.GetUserPortfolio(userId);

            //Assert
            Assert.False(result.isValidData);
        }

        ~UserDACTest()
        {
            inMemoryDBContext.Dispose();
        }
    }
}
