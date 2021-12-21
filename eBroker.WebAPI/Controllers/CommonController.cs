using eBroker.Business;
using eBroker.Business.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eBroker.WebAPI.Controllers
{
    /// <summary>
    /// Common APIs 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CommonController : ControllerBase
    {
        private ICommonBDC commonBDC;

        /// <summary>
        /// Constructor
        /// </summary>
        public CommonController()
        {
            commonBDC = new CommonBDC();
        }

        /// <summary>
        /// Get Trade type for dropdown
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("Get/TradeType")]
        public IActionResult GetTradeType()
        {
            return Ok(commonBDC.GetTradeType().Data);
        }

        /// <summary>
        /// Get Trade history for respective user
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("Get/TradeHistory/{userId}")]
        public IActionResult GetTradeHistory(int userId)
        {
            return Ok(commonBDC.GetTradeHistory(userId).Data);
        }
    }
}
