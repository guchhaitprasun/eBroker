using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace eBroker.Shared.Helpers
{
    /// <summary>
    /// Container to initiate fund related transaction from API
    /// </summary>
    public class Fund
    {

        private decimal amount;
        private decimal processingCharges;

        /// <summary>
        /// DMAT Number for Fund addition
        /// </summary>
        [DefaultValue("1234-5678-9012-3456")] 
        public string DmatNumber { get; set; }

        /// <summary>
        /// Amount with processing charges removed from the actual amount
        /// </summary>
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
                {
                    processingCharges = CalculateProcessingCharges(value);
                    amount = value - processingCharges;

                }
                else
                    amount = value;
            }
        }

        /// <summary>
        /// Readonly value for the calculated processing charges
        /// </summary>
        public decimal ProcessingCharges
        {
            get
            {
                return processingCharges;
            }
        }

        /// <summary>
        /// Static funtion to calculate processing charges
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
        public static decimal CalculateProcessingCharges(decimal amount)
        {
            decimal retunValue = 0;
            if (amount > 100000)
            {
                retunValue = amount * (decimal)0.05;
            }

            return retunValue;
        }
    }
}
