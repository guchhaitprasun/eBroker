using eBroker.DAL;
using eBroker.Data.Database;
using eBroker.Shared.DTOs;
using eBroker.Shared.Helpers;
using eBroker.Shared.Interface;
using eBroker.Tests.InMemoryData;
using Microsoft.EntityFrameworkCore;
using Moq;
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
    public class TradeDACTest
    {
        private DbContextOptions<EBrokerDbContext> options;
        private InMemoryDBContext inMemoryDBContext;
        public TradeDACTest()
        {
            inMemoryDBContext = new InMemoryDBContext();
            options = inMemoryDBContext.Options;
        }

        [Fact, Description("Ensure the list of stocks is provided from the DB")]
        public void GetAllStocks_Returns_List_of_Stocks()
        {
            //Arrange
            var tradeDac = new TradeDAC(options);

            //Act 
            var result = tradeDac.GetAllStocks();

            //Assert
            Assert.NotEmpty(result.Data);
        }

        [Fact, Description("Ensure the stocks details is provided from the DB for valid stock id")]
        public void GetStockByID_Returns_Stocks_for_ValidStockId()
        {
            //Arrange
            var tradeDac = new TradeDAC(options);
            int stockId = 1;

            //Act 
            var result = tradeDac.GetStockByID(stockId);

            //Assert
            Assert.NotNull(result.Data);
        }

        [Fact, Description("Ensure No Stocks is provided for Invalid stock id")]
        public void GetStockByID_Returns_Null_for_InValidStockId()
        {
            //Arrange
            var tradeDac = new TradeDAC(options);
            int stockId = 1000;

            //Act 
            var result = tradeDac.GetStockByID(stockId);

            //Assert
            Assert.Null(result.Data);
        }

        [Fact, Description("Ensure true flag is provided if the stock is availabe in user portfolio")]
        public void IsStockInUserPortfolio_Returns_True_for_Stock_Available_in_Portfolio()
        {
            //Arrange
            var tradeDac = new TradeDAC(options);
            int stockId = 1;
            int userId = 100;

            //Act 
            var result = tradeDac.IsStockInUserPortfolio(userId, stockId);

            //Assert
            Assert.True(result.Data);
        }

        [Fact, Description("Ensure false flag is provided if the stock is not available in user portfolio")]
        public void IsStockInUserPortfolio_Returns_False_for_Stock_NotAvailable_in_Portfolio()
        {
            //Arrange
            var tradeDac = new TradeDAC(options);
            int stockId = 1;
            int userId = 101;

            //Act 
            var result = tradeDac.IsStockInUserPortfolio(userId, stockId);

            //Assert
            Assert.False(result.Data);
        }

        [Fact, Description("Ensure flag true is provided if user has sufficient stocks availabe in portfolio to sell ")]
        public void IsStockQuantitySufficeForSell_Returns_True_for_Stock__Quantity_Available_for_Sell_in_Portfolio()
        {
            //Arrange
            var tradeDac = new TradeDAC(options);
            int stockId = 1;
            int userId = 100;
            int quantityToSell = 10;

            //Act 
            var result = tradeDac.IsStockQuantitySufficeForSell(userId, stockId, quantityToSell);

            //Assert
            Assert.True(result.Data);
        }

        [Fact, Description("Ensure flag false is provided if the user has less stocks then requested for sell")]
        public void IsStockQuantitySufficeForSell_Returns_False_for_Stock__Quantity_NotAvailable_for_Sell_in_Portfolio()
        {
            //Arrange
            var tradeDac = new TradeDAC(options);
            int stockId = 1;
            int userId = 100;
            int quantityToSell = 1000;

            //Act 
            var result = tradeDac.IsStockQuantitySufficeForSell(userId, stockId, quantityToSell);

            //Assert
            Assert.False(result.Data);
        }

        [Fact, Description("Ensure new entry was not made and existing entry was updated in user portfolio")]
        public void ProcessPurchase_Returns_True_For_Stock_Exist_In_Portfolio()
        {
            //Arrange
            var tradeDac = new TradeDAC(options);
            var accountDetails = new AccountDTO
            {
                AccountId = 101,
                UserId = 100,
                DmatAccountNumber = "1111-2222-3333-4444",
                AvailableBalance = 1000,
                IsActive = true
            };
            var tradeDetails = new Trade
            {
                DmatAccountnumber = "1111-2222-3333-4444",
                StockID = 1,
                EquityQuantity = 1
            };

            var stockPrice = tradeDac.GetStockByID(tradeDetails.StockID).Data.Price;
            var purchaseAmount = stockPrice * tradeDetails.EquityQuantity;
            var mockDateTimeHelper = new Mock<IDateTimeHelper>();
            var fakeDateTime = new DateTime(2021, 12, 20, 10, 30, 30);
            mockDateTimeHelper.Setup(o => o.GetDateTimeNow()).Returns(fakeDateTime);
            var mockDateTime = mockDateTimeHelper.Object.GetDateTimeNow();

            //Act 
            var result = tradeDac.ProcessPurchase(accountDetails, tradeDetails, purchaseAmount, mockDateTime);

            //Assert
            Assert.True(result);
        }

        [Fact, Description("Ensure Stock Purchase Executed for valid request where stock does not exist in user portfolio")]
        public void ProcessPurchase_Returns_True()
        {
            //Arrange
            var tradeDac = new TradeDAC(options);
            var accountDetails = new AccountDTO
            {
                AccountId = 101,
                UserId = 100,
                DmatAccountNumber = "1111-2222-3333-4444",
                AvailableBalance = 1000,
                IsActive = true
            };
            var tradeDetails = new Trade
            {
                DmatAccountnumber = "1111-2222-3333-4444",
                StockID = 2,
                EquityQuantity = 1
            };

            var stockPrice = tradeDac.GetStockByID(tradeDetails.StockID).Data.Price;
            var purchaseAmount = stockPrice * tradeDetails.EquityQuantity;
            var mockDateTimeHelper = new Mock<IDateTimeHelper>();
            var fakeDateTime = new DateTime(2021, 12, 20, 10, 30, 30);
            mockDateTimeHelper.Setup(o => o.GetDateTimeNow()).Returns(fakeDateTime);
            var mockDateTime = mockDateTimeHelper.Object.GetDateTimeNow();

            //Act 
            var result = tradeDac.ProcessPurchase(accountDetails, tradeDetails, purchaseAmount, mockDateTime);

            //Assert
            Assert.True(result);
        }

        [Fact, Description("Ensure Stock sold executed for valid request")]
        public void ProcessSelling_Returns_True()
        {
            //Arrange
            var tradeDac = new TradeDAC(options);
            var accountDetails = new AccountDTO
            {
                AccountId = 101,
                UserId = 100,
                DmatAccountNumber = "1111-2222-3333-4444",
                AvailableBalance = 1000,
                IsActive = true
            };
            var tradeDetails = new Trade
            {
                DmatAccountnumber = "1111-2222-3333-4444",
                StockID = 1,
                EquityQuantity = 1
            };

            var stockPrice = tradeDac.GetStockByID(tradeDetails.StockID).Data.Price;
            var sellingGain = stockPrice * tradeDetails.EquityQuantity;
            var brokerage = 20;
            var mockDateTimeHelper = new Mock<IDateTimeHelper>();
            var fakeDateTime = new DateTime(2021, 12, 20, 10, 30, 30);
            mockDateTimeHelper.Setup(o => o.GetDateTimeNow()).Returns(fakeDateTime);
            var mockDateTime = mockDateTimeHelper.Object.GetDateTimeNow();

            //Act 
            var result = tradeDac.ProcessSelling(accountDetails, tradeDetails, sellingGain, brokerage, mockDateTime);

            //Assert
            Assert.True(result);
        }

        ~TradeDACTest()
        {
            inMemoryDBContext.Dispose();
        }
    }

}
