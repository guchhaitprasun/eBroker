using eBroker.DAL;
using eBroker.Data.Database;
using eBroker.Tests.InMemoryData;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Xunit;

namespace eBroker.Tests.DataLayerTests
{
    /// <summary>
    /// SYSTEM UNDER TEST: eBroker.DAL (Data Layer)
    /// </summary>
    public class AccountDACTest
    {
        private DbContextOptions<EBrokerDbContext> options;
        private InMemoryDBContext inMemoryDBContext;
        public AccountDACTest()
        {
            inMemoryDBContext = new InMemoryDBContext();
            options = inMemoryDBContext.Options;
        }


        [Fact , Description("Ensure a valid account details on providing a valid dmat id")]
        public void GetAccountDetails_ByDematID_Returns_AccountDetails_Success()
        {
            //Arrange
            var dmatNumber = "1111-2222-3333-4444";
            var accountDAC = new AccountDAC(options);
            var expectedAccountId = 101;

            //Act
            var result = accountDAC.GetAccountDetailsByDematID(dmatNumber);

            //Assert
            Assert.Equal(expectedAccountId, result.Data.AccountId);
        }

        [Fact, Description("Ensure valid data flag set to false if user account does not exist")]
        public void GetAccountDetails_ByDematID_Returns_AccountDetails_Error()
        {
            //Arrange
            var dmatNumber = "1111-2222-3333-5555";
            var accountDAC = new AccountDAC(options);

            //Act
            var result = accountDAC.GetAccountDetailsByDematID(dmatNumber);

            //Assert
            Assert.False(result.isValidData);
        }

        [Fact, Description("Ensure Valid amount added to user wallet")]
        public void AddFunds_ValidAccount_Fund_Addition_Success()
        {
            //Arrange
            var dmatNumber = "1111-2222-3333-4444";
            var accountDAC = new AccountDAC(options);
            var additionAmount = 99;
            var expectedAmount = 1000 + additionAmount;

            //Act
            accountDAC.AddFunds(dmatNumber, additionAmount);
            var currentAmount = accountDAC.GetAccountDetailsByDematID(dmatNumber);

            //Assert
            Assert.Equal(expectedAmount, currentAmount.Data.AvailableBalance);
        }

        [Fact, Description("Ensure a failure if the account does not exist")]
        public void AddFunds_InValidAccount_Fund_Addition_Failure()
        {
            //Arrange
            var dmatNumber = "1111222233334444";
            var accountDAC = new AccountDAC(options);

            //Act
            var result = accountDAC.AddFunds(dmatNumber, 100);

            //Assert
            Assert.False(result.Data);
        }

        ~AccountDACTest()
        {
            inMemoryDBContext.Dispose();
        }
    }
}
