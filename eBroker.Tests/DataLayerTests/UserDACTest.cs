using eBroker.DAL;
using eBroker.Data.Database;
using eBroker.Shared.DTOs;
using eBroker.Shared.Enums;
using eBroker.Tests.InMemoryData;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace eBroker.Tests.DataLayerTests
{
    public class UserDACTest
    {
        private DbContextOptions<EBrokerDbContext> options;
        public UserDACTest()
        {
            options = new InMemoryDBContext().Options;
        }

        [Fact]
        public void UserAuthentcation_Returns_ValidData()
        {
            using (var context = new EBrokerDbContext(options))
            {
                //Arrange
                var userObj = new UserDTO
                {
                    EmailAddress = "visualStudioTest@nagp.com",
                    Password = "Passw0rd"
                };
                var userDAC = new UserDAC(context);

                //Act
                var result = userDAC.AuthenticatUser(userObj);


                //Assert
                Assert.True(result.isValidData);
            }
           
        }

        [Fact]
        public void UserAuthentcation_Returns_InValidData()
        {
            using (var context = new EBrokerDbContext(options))
            {
                //Arrange
                var userObj = new UserDTO
                {
                    EmailAddress = "invalidUser@nagp.com",
                    Password = "Passw0rd"
                };
                var userDAC = new UserDAC(context);

                //Act
                var result = userDAC.AuthenticatUser(userObj);


                //Assert
                Assert.False(result.isValidData);
            }

        }

    }
}
