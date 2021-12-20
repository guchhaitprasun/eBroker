using System;
using System.Collections.Generic;
using System.Text;

namespace eBroker.Shared.Interface
{
    /// <summary>
    /// Interface to mock it from unit test cases
    /// </summary>
    public interface IDateTimeHelper
    {
        DateTime GetDateTimeNow();
    }
}
