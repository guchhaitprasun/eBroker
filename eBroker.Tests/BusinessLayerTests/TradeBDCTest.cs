using eBroker.Business;
using eBroker.Business.Interface;
using eBroker.Shared.DTOs;
using eBroker.Shared.Enums;
using eBroker.Shared.Helpers;
using eBroker.Shared.Interface;
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

        public TradeBDCTest()
        {
            tradeBDC = new TradeBDC();
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
    }
}
