using eBroker.Business;
using eBroker.Shared.DTOs;
using eBroker.Shared.Enums;
using eBroker.Shared.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static eBroker.Shared.Enums.Constants;

namespace eBroker.WebAPI.Controllers
{
    /// <summary>
    /// Trading APIs Implementation
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TradeController : ControllerBase
    {
        /// <summary>
        /// Fetch all the acive and available stocks in the market 
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("Get/allMarketStocks")]
        public IActionResult GetAllStocks()
        {
            TradeBDC tradeBDC = new TradeBDC();
            DataContainer<IList<StockDTO>> allStocks = tradeBDC.GetAllStocks();

            if (allStocks.isValidData && allStocks.Data.Count > 0)
            {
                return Ok(allStocks.Data);
            }
            return BadRequest(Constants.ConnetionIssue);
        }

        /// <summary>
        /// Buy Equity between 9AM to 3PM
        /// </summary>
        /// <param name="stockDetials"></param>
        /// <returns></returns>
        [HttpPost, Route("Post/buyEquity")]
        public IActionResult BuyStocks(Trade stockDetials)
        {
  
            TradeBDC tradeBDC = new TradeBDC();
            var response = tradeBDC.ValidateAndInitiateTrade(stockDetials, TradeType.Buy);
            if (response.isValidData)
            {
                return Ok(response.Message);
            }
            return BadRequest(DMATNumberValidtion);
        }

        /// <summary>
        /// Sell Equity between 9AM to 3PM after deducing brokerage (0.05%, min 20rs)
        /// </summary>
        /// <param name="stockDetails"></param>
        /// <returns></returns>
        [HttpPost, Route("Post/sellEquity")]
        public IActionResult SellStocks(Trade stockDetails)
        {
            TradeBDC tradeBDC = new TradeBDC();
            var response = tradeBDC.ValidateAndInitiateTrade(stockDetails, TradeType.Sell);
            if (response.isValidData)
            {
                return Ok(response.Message);
            }
            return BadRequest(DMATNumberValidtion);
        }
    }
}
