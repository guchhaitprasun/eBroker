using eBroker.WebAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xunit;

namespace eBroker.Tests.ControllerTests
{
    public class CommonControllerTest
    {
        [Fact, Description("Ensure GetTrade type API can be called")]
        public void GetTradeType_Returns_success()
        {
            //Arrange 
            var controller = new CommonController();
            var expectedValue = 200;

            //Act 
            var actionResult = controller.GetTradeType();
            var result = actionResult as ObjectResult;

            //Assert
            Assert.Equal(expectedValue, result.StatusCode);
        }

        [Fact, Description("Ensure GetTradeHistory API can be called")]
        public void GetTradeHistory_Returns_success()
        {
            //Arrange 
            var controller = new CommonController();
            var userId = 20210;
            var expectedValue = 200;

            //Act 
            var actionResult = controller.GetTradeHistory(userId);
            var result = actionResult as ObjectResult;

            //Assert
            Assert.Equal(expectedValue, result.StatusCode);
        }
    }
}
