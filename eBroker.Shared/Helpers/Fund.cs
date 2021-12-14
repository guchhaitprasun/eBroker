using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace eBroker.Shared.Helpers
{
    public class Fund
    {

        private decimal amount;
        private decimal processingCharges = 0;

        [DefaultValue("1234-5678-9012-3456")] 
        public string DmatNumber { get; set; }

        [DefaultValue("0")]
        public decimal Amount
        {
            get
            {
                return amount;
            }
            set
            {
                if (value > 100000)
                    amount = value - CalculateProcessingCharges(value);
                else
                    amount = value;
            }
        }
        public decimal ProcessingCharges
        {
            get
            {
                return processingCharges;
            }
        }

        private decimal CalculateProcessingCharges(decimal amount)
        {
            decimal retunValue = 0;
            if (amount > 100000)
            {
                retunValue = amount * (decimal)0.05;
                this.processingCharges = retunValue;
            }

            return retunValue;
        }
    }
}
