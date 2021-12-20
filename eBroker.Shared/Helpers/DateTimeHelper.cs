using eBroker.Shared.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace eBroker.Shared.Helpers
{
    public class DateTimeHelper : IDateTimeHelper
    {
        public DateTime GetDateTimeNow()
        {
            return DateTime.Now;
        }
    }
}
