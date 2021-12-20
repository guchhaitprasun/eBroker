using eBroker.Shared.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xunit;

namespace eBroker.Tests.HelperTests
{
    /// <summary>
    /// SYSTEM UNDER TEST: eBroker.Shared (Data Layer) - Helper Classes
    /// </summary>
    public class HelperClassTest
    {
        [Fact, Description("Ensure processing charges are always 0 for the amount less than or equal to 100000 ")]
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

        [Fact, Description("Ensure processing charges are always 0.05% of total amount for the amount grater than 100000 ")]
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
