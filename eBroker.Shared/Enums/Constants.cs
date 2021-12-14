using System;
using System.Collections.Generic;
using System.Text;

namespace eBroker.Shared.Enums
{
    public static class Constants
    {
        public enum TradeType
        {
            Buy = 1, 
            Sell = 2
        }

        public const string ConnetionIssue = "Database Connection Issue";
        public const string DACException = "Exception Occured on Database Operation \n ";
        public const string BDCException = "Logical Operations Exception on BDC \n ";
        public const string LoginSuccess = "Login Successfully";
        public const string LoginFailed = "Authentication Failed";
        public const string StocksNotAvailabe = "Stock not Avialabe at the moment";
        public const string StocksInvalid = "Stock not Avialabe at the moment";
        public const string AccountInvalid = "Account is Inactive or Not exist";
        public const string TradeFailed = "Tradeing Failed";
        public const string FundValidation = "Min Rs 1 is required for fund addition";
        public const string DMATNumberValidtion = "DMAT number is required";
        public const string TradingCloseSatSunMessage = "Trading sessions are closed on Saturday & Sunday";
        public const string TradingCloseMessage = "Trading sessions are closed now, will be available from 9AM to 3PM for Monday to Friday";
        public const string TradingOpenMessage = "Trading sessions are open now";
        public const string PurchaseSuccess = "Equity Purchased Successfully";
        public const string PurchaseFailed = "Purchase Failed Contact Administrator";
        public const string SellingFailed = "Selling Failed Contact Administrator";
        public const string InavlidOperation = "Invalid Operation Requested";
        public const string InsufficientFunds = "Insufficient Funds";
        public const string LessEquityErrorMessage = "Equity Hold by user is less then Quantity requested to sell";
        public const string EquityNotExist = "Equity Not Exist in User Portfolio";
        public const string EquitySoldSuccess = "Equity Sold Successfully with selling gain Rs.#SELLINGGAIN# and brokerage Rs.#BROKERAGE# (min 20Rs or 0.05% of trade value)";
        public const string FundAddSuccess = "Funds of Rs.#AMOUNT# Added Successfully for DMAT No. #DMATNUMBER# and processing charge of Rs.#PROCESSINGCHARGE# is Applied";
        public const string DMATNotExist = "DMAT No. #DMATNUMBER# is either invalid, in active or Not exist";

        //Placeholders
        public const string SellingGainPlaceholder = "#SELLINGGAIN#";
        public const string BrokeragePlaceholder = "#BROKERAGE#";
        public const string Amount = "#AMOUNT#";
        public const string DMATNumber = "#DMATNUMBER#";
        public const string ProcessingCharges = "#PROCESSINGCHARGE#";

    }
}
