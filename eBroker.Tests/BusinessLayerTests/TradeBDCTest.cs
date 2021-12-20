using eBroker.Business;
using eBroker.Business.Interface;
using eBroker.Data.Database;
using eBroker.Shared.DTOs;
using eBroker.Shared.Enums;
using eBroker.Shared.Helpers;
using eBroker.Shared.Interface;
using eBroker.Tests.InMemoryData;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace eBroker.Tests.BusinessLayerTests
{
    public class TradeBDCTest
    {
        private readonly ITradeBDC tradeBDC;
        private readonly DbContextOptions<EBrokerDbContext> options;
        private readonly InMemoryDBContext inMemoryDBContext;

        public TradeBDCTest()
        {
            tradeBDC = new TradeBDC();
            inMemoryDBContext = new InMemoryDBContext();
            options = inMemoryDBContext.Options;
        }

        [Fact]
        public void GetAllStocks_Returns_List_of_Stocks()
        {
            //Act
            var result = tradeBDC.GetAllStocks();

            //Assert;
            Assert.NotEmpty(result.Data);
        }

        [Fact]
        public void CalculateBrokrage_Returns_Minimum_Brokrage_Amount()
        {
            //Arrange
            var sharesValue = 100.5m;
            var expectedValue = 20m;

            //Act
            var result = tradeBDC.CalculateBrokrage(sharesValue);

            //Assert
            Assert.Equal(expectedValue, result);
        }

        [Fact]
        public void CalculateBrokrage_Returns_Brokrage_Amount()
        {
            //Arrange
            var sharesValue = 1000.5m;
            var expectedValue = 50.025m;

            //Act
            var result = tradeBDC.CalculateBrokrage(sharesValue);

            //Assert
            Assert.Equal(expectedValue, result);
        }

        [Fact]
        public void ValidateAndInitiateTrade_BuyEquity_Outside_Trading_Hours_Returns_Error_Message()
        {
            //Arrange
            var tradeObject = new Trade
            {
                DmatAccountnumber = "1234-5678-9012-3456",
                StockID = 1,
                EquityQuantity = 1
            };
            var expectedMessage = Constants.TradingCloseMessage;
            var mockDateTimeHelper = new Mock<IDateTimeHelper>();
            var fakeDateTime = new DateTime(2021, 12, 16, 16, 30, 30);
            mockDateTimeHelper.Setup(o => o.GetDateTimeNow()).Returns(fakeDateTime);
            var tradeBDCWithMockDateTime = new TradeBDC(mockDateTimeHelper.Object);


            //Act
            var result = tradeBDCWithMockDateTime.ValidateAndInitiateTrade(tradeObject, Constants.TradeType.Buy);

            //Assert
            Assert.Equal(expectedMessage, result.Message);

        }

        [Fact]
        public void ValidateAndInitiateTrade_SellEquity_Outside_Trading_Hours_Returns_Error_Message()
        {
            //Arrange
            var tradeObject = new Trade
            {
                DmatAccountnumber = "1234-5678-9012-3456",
                StockID = 1,
                EquityQuantity = 1
            };
            var expectedMessage = Constants.TradingCloseMessage;
            var mockDateTimeHelper = new Mock<IDateTimeHelper>();
            var fakeDateTime = new DateTime(2021, 12, 16, 16, 30, 30);
            mockDateTimeHelper.Setup(o => o.GetDateTimeNow()).Returns(fakeDateTime);
            var tradeBDCWithMockDateTime = new TradeBDC(mockDateTimeHelper.Object);


            //Act
            var result = tradeBDCWithMockDateTime.ValidateAndInitiateTrade(tradeObject, Constants.TradeType.Sell);

            //Assert
            Assert.Equal(expectedMessage, result.Message);

        }

        [Fact]
        public void ValidateAndInitiateTrade_BuyEquity_Outside_Trading_Days_Returns_Error_Message()
        {
            //Arrange
            var tradeObject = new Trade
            {
                DmatAccountnumber = "1234-5678-9012-3456",
                StockID = 1,
                EquityQuantity = 1
            };
            var expectedMessage = Constants.TradingCloseSatSunMessage;
            var mockDateTimeHelper = new Mock<IDateTimeHelper>();
            var fakeDateTime = new DateTime(2021, 12, 12, 10, 30, 30);
            mockDateTimeHelper.Setup(o => o.GetDateTimeNow()).Returns(fakeDateTime);
            var tradeBDCWithMockDateTime = new TradeBDC(mockDateTimeHelper.Object);


            //Act
            var result = tradeBDCWithMockDateTime.ValidateAndInitiateTrade(tradeObject, Constants.TradeType.Buy);

            //Assert
            Assert.Equal(expectedMessage, result.Message);

        }

        [Fact]
        public void ValidateAndInitiateTrade_SellEquity_Outside_Trading_Days_Returns_Error_Message()
        {
            //Arrange
            var tradeObject = new Trade
            {
                DmatAccountnumber = "1234-5678-9012-3456",
                StockID = 1,
                EquityQuantity = 1
            };
            var expectedMessage = Constants.TradingCloseSatSunMessage;
            var mockDateTimeHelper = new Mock<IDateTimeHelper>();
            var fakeDateTime = new DateTime(2021, 12, 12, 10, 30, 30);
            mockDateTimeHelper.Setup(o => o.GetDateTimeNow()).Returns(fakeDateTime);
            var tradeBDCWithMockDateTime = new TradeBDC(mockDateTimeHelper.Object);


            //Act
            var result = tradeBDCWithMockDateTime.ValidateAndInitiateTrade(tradeObject, Constants.TradeType.Sell);

            //Assert
            Assert.Equal(expectedMessage, result.Message);

        }

        [Fact]
        public void ValidateAndInitiateTrade_InvalidAccount_Error_Message()
        {
            //Arrange
            var tradeObject = new Trade
            {
                DmatAccountnumber = "1234-5678-9012-0000",
                StockID = 1,
                EquityQuantity = 1
            };
            var expectedMessage = Constants.AccountInvalid;
            var mockDateTimeHelper = new Mock<IDateTimeHelper>();
            var fakeDateTime = new DateTime(2021, 12, 20, 10, 30, 30);
            mockDateTimeHelper.Setup(o => o.GetDateTimeNow()).Returns(fakeDateTime);
            var tradeBDCWithMockDateTime = new TradeBDC(mockDateTimeHelper.Object);

            //Act
            var result = tradeBDCWithMockDateTime.ValidateAndInitiateTrade(tradeObject, Constants.TradeType.Sell);

            //Assert
            Assert.Equal(expectedMessage, result.Message);
        }

        [Fact]
        public void ValidateAndInitiateTrade_InvalidStock_Error_Message()
        {
            //Arrange
            var tradeObject = new Trade
            {
                DmatAccountnumber = "1234-5678-9012-3456",
                StockID = 10000,
                EquityQuantity = 1
            };
            var expectedMessage = Constants.StocksInvalid;
            var mockDateTimeHelper = new Mock<IDateTimeHelper>();
            var fakeDateTime = new DateTime(2021, 12, 20, 10, 30, 30);
            mockDateTimeHelper.Setup(o => o.GetDateTimeNow()).Returns(fakeDateTime);
            var tradeBDCWithMockDateTime = new TradeBDC(mockDateTimeHelper.Object);

            //Act
            var result = tradeBDCWithMockDateTime.ValidateAndInitiateTrade(tradeObject, Constants.TradeType.Sell);

            //Assert
            Assert.Equal(expectedMessage, result.Message);
        }

        [Fact]
        public void ValidateAndInititateTrade_Buy_Success()
        {
            //Arrange
            var mockDateTimeHelper = new Mock<IDateTimeHelper>();
            var fakeDateTime = new DateTime(2021, 12, 20, 10, 30, 30);
            mockDateTimeHelper.Setup(o => o.GetDateTimeNow()).Returns(fakeDateTime);
            var tradeBDCWithInmemoryDB = new TradeBDC(mockDateTimeHelper.Object, options);
            var tradeObject = new Trade
            {
                DmatAccountnumber = "1111-2222-3333-4444",
                StockID = 1,
                EquityQuantity = 1
            };
            var expectedMessage = Constants.PurchaseSuccess;

            //Act
            var result = tradeBDCWithInmemoryDB.ValidateAndInitiateTrade(tradeObject, Constants.TradeType.Buy);

            //Assert
            Assert.Equal(expectedMessage, result.Message);
        }

        [Fact]
        public void ValidateAndInititateTrade_Buy_Failed_InsufficientFund()
        {
            //Arrange
            var mockDateTimeHelper = new Mock<IDateTimeHelper>();
            var fakeDateTime = new DateTime(2021, 12, 20, 10, 30, 30);
            mockDateTimeHelper.Setup(o => o.GetDateTimeNow()).Returns(fakeDateTime);
            var tradeBDCWithInmemoryDB = new TradeBDC(mockDateTimeHelper.Object, options);
            var tradeObject = new Trade
            {
                DmatAccountnumber = "1111-2222-3333-4444",
                StockID = 1,
                EquityQuantity = 100
            };
            var expectedMessage = Constants.InsufficientFunds;

            //Act
            var result = tradeBDCWithInmemoryDB.ValidateAndInitiateTrade(tradeObject, Constants.TradeType.Buy);

            //Assert
            Assert.Equal(expectedMessage, result.Message);
        }

        [Fact]
        public void ValidateAndInititateTrade_Sell_Success()
        {
            //Arrange
            var mockDateTimeHelper = new Mock<IDateTimeHelper>();
            var fakeDateTime = new DateTime(2021, 12, 20, 10, 30, 30);
            mockDateTimeHelper.Setup(o => o.GetDateTimeNow()).Returns(fakeDateTime);
            var tradeBDCWithInmemoryDB = new TradeBDC(mockDateTimeHelper.Object, options);
            var tradeObject = new Trade
            {
                DmatAccountnumber = "1111-2222-3333-4444",
                StockID = 1,
                EquityQuantity = 1
            };
            var stockPrice = 709.55m;
            var sellingGain = stockPrice * tradeObject.EquityQuantity;
            var brokerage = tradeBDCWithInmemoryDB.CalculateBrokrage(sellingGain);
            sellingGain = sellingGain - brokerage;
            var expectedMessage = Constants.EquitySoldSuccess.Replace(Constants.SellingGainPlaceholder, sellingGain.ToString()).Replace(Constants.BrokeragePlaceholder, brokerage.ToString());

            //Act
            var result = tradeBDCWithInmemoryDB.ValidateAndInitiateTrade(tradeObject, Constants.TradeType.Sell);

            //Assert
            Assert.Equal(expectedMessage, result.Message);
        }

        ~TradeBDCTest()
        {
            inMemoryDBContext.Dispose();
        }
    }
}
