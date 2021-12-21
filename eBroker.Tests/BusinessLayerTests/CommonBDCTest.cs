using eBroker.Business;
using eBroker.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xunit;

namespace eBroker.Tests.BusinessLayerTests
{
    public class CommonBDCTest
    {
        [Fact, Description("Ensure GetTrade type gives list of trade types")]
        public void GetTradeType_Returns_NonEmpty_List()
        {
            //Arrange 
            var commonBDC = new CommonBDC();

            //Act 
            var result = commonBDC.GetTradeType();

            //Assert
            Assert.NotEmpty(result.Data);
        }

        [Fact, Description("Ensure GetTradeHistory gives trade list")]
        public void GetTradeHistory_Returns_TradeHistory_List()
        {
            //Arrange 
            var commonBDC = new CommonBDC();
            var userId = 20210;

            //Act 
            var result = commonBDC.GetTradeHistory(userId);

            //Assert
            Assert.True(result.Data is List<TradeHistoryDTO>);
        }
    }
}
