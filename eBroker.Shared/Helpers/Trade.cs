using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace eBroker.Shared.Helpers
{
    public class Trade
    {
        [DefaultValue("1234-5678-9012-3456")]
        public string DmatAccountnumber { get; set; }
        [DefaultValue("1")]
        public int StockID { get; set; }
        [DefaultValue("1")]
        public int EquityQuantity { get; set; }
    }
}
