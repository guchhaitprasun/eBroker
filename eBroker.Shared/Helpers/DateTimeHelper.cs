using eBroker.Shared.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace eBroker.Shared.Helpers
{
    /// <summary>
    /// Helper to get the datetime
    /// </summary>
    public class DateTimeHelper : IDateTimeHelper
    {
        public DateTime GetDateTimeNow()
        {
            return DateTime.Now;
        }
    }
}
