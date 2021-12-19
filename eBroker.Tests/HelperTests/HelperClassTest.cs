using eBroker.Shared.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace eBroker.Tests.HelperTests
{
    public class HelperClassTest
    {
        [Fact]
        public void HelperClass_StaticMethod_Validation_Return_0_for_ProcessingCharge_LessOrEqualTo_100000()
        {
            //Arrange
            int _Amount = 100000;
            int expectedAmount = 0;

            //Act
            Fund payload = new Fund
            {
                Amount = _Amount
            };

            //Assert
            Assert.Equal(expectedAmount, payload.ProcessingCharges);
        }

        [Fact]
        public void HelperClass_StaticMethod_Validation_Return_ProcessingCharge_for_Above_100000()
        {
            //Arrange
            int _Amount = 100001;
            decimal expectedAmount = 5000.05m;

            //Act
            Fund payload = new Fund
            {
                Amount = _Amount
            };

            //Assert
            Assert.Equal(expectedAmount, payload.ProcessingCharges);
        }
    }
}
