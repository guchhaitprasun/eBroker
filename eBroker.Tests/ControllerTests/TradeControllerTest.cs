﻿using eBroker.Shared.DTOs;
using eBroker.Shared.Helpers;
using eBroker.Shared.Interface;
using eBroker.WebAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace eBroker.Tests.ControllerTests
{
    public class TradeControllerTest
    {
        private readonly TradeController Controller;

        public TradeControllerTest()
        {
            Controller = new TradeController();
        }

        [Fact]
        public void GetAllMarketStocks_Returns_Success_200()
        {
            //Arrange
            var expectedStatusCode = 200; //Ok Result

            //Act
            var actionResult = Controller.GetAllStocks();

            //Assert
            var result = actionResult as ObjectResult;
            Assert.Equal(expectedStatusCode, result.StatusCode);
        }

        [Fact]
        public void BuyStocks_Return_StatusCode_Any()
        {
            //Arrange
            var expectedStatusCodeStart = 100;
            var expectedStatusCodeEnd = 599;
            var tradeObject = new Trade
            {
                DmatAccountnumber = "1234567890123456",
                StockID = 1,
                EquityQuantity = 1
            };

            //Act
            var actionResult = Controller.BuyStocks(tradeObject);

            //Assert
            var result = actionResult as ObjectResult;
            Assert.InRange(result.StatusCode.Value, expectedStatusCodeStart, expectedStatusCodeEnd);
        }

        [Fact]
        public void SellStocks_Return_StatusCode_Any()
        {
            //Arrange
            var expectedStatusCodeStart = 100;
            var expectedStatusCodeEnd = 599;
            var tradeObject = new Trade
            {
                DmatAccountnumber = "1234567890123456",
                StockID = 1,
                EquityQuantity = 1
            };

            //Act
            var actionResult = Controller.SellStocks(tradeObject);

            //Assert
            var result = actionResult as ObjectResult;
            Assert.InRange(result.StatusCode.Value, expectedStatusCodeStart, expectedStatusCodeEnd);
        }
    }
}