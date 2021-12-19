using eBroker.DAL;
using eBroker.Data.Database;
using eBroker.Tests.InMemoryData;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace eBroker.Tests.DataLayerTests
{
    public class AccountDACTest
    {
        private DbContextOptions<EBrokerDbContext> options;
        public AccountDACTest()
        {
            options = new InMemoryDBContext().Options;
        }


        [Fact]
        public void GetAccountDetails_ByDematID_Returns_AccountDetails_Success()
        {
            using (var context = new EBrokerDbContext(options))
            {
                //Arrange
                var dmatNumber = "1111-2222-3333-4444";
                var accountDAC = new AccountDAC(context);
                var expectedAccountId = 101;

                //Act
                var result = accountDAC.GetAccountDetailsByDematID(dmatNumber);

                //Assert
                Assert.Equal(expectedAccountId, result.Data.AccountId);
            }
        }

        [Fact]
        public void AddFunds_ValidAccount_Fund_Addition_Success()
        {
            using (var context = new EBrokerDbContext(options))
            {
                //Arrange
                var dmatNumber = "1111-2222-3333-4444";
                var accountDAC = new AccountDAC(context);
                var additionAmount = 99;
                var expectedAmount = 501 + additionAmount;

                //Act
                accountDAC.AddFunds(dmatNumber, additionAmount);
                var currentAmount = accountDAC.GetAccountDetailsByDematID(dmatNumber);

                //Assert
                Assert.Equal(expectedAmount, currentAmount.Data.AvailableBalance);
            }
        }

        [Fact]
        public void AddFunds_InValidAccount_Fund_Addition_Failure()
        {
            using (var context = new EBrokerDbContext(options))
            {
                //Arrange
                var dmatNumber = "1111222233334444";
                var accountDAC = new AccountDAC(context);

                //Act
                var result = accountDAC.AddFunds(dmatNumber, 100);

                //Assert
                Assert.False(result.Data);
            }
        }
    }
}
