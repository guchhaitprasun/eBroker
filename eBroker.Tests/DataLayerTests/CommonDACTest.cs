using eBroker.DAL;
using eBroker.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xunit;

namespace eBroker.Tests.DataLayerTests
{
    public class CommonDACTest
    {
        [Fact, Description("Ensure GetTrade type gives list of trade types from database")]
        public void GetTradeType_Returns_NonEmpty_List()
        {
            //Arrange 
            var commonDAC= new CommonDAC();

            //Act 
            var result = commonDAC.GetTradeType();

            //Assert
            Assert.NotEmpty(result.Data);
        }

        [Fact, Description("Ensure GetTradeHistory gives trade list from database")]
        public void GetTradeHistory_Returns_TradeHistory_List()
        {
            //Arrange 
            var commonDAC = new CommonDAC(); ;
            var userId = 20210;

            //Act 
            var result = commonDAC.GetTradeHistory(userId);

            //Assert
            Assert.True(result.Data is List<TradeHistoryDTO>);
        }
    }
}
