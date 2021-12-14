using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eBroker.WebAPI.Controllers
{
    /// <summary>
    /// Accounts APIs Implementation
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        /// <summary>
        /// Dummy API
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("Get")]
        public IActionResult Get()
        {
            return Ok();
        }
    }
}
