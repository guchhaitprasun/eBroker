using eBroker.Shared.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eBroker.Business;
using eBroker.Shared.Helpers;

namespace eBroker.WebAPI.Controllers
{
    /// <summary>
    /// User APIs Implementation
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        /// <summary>
        /// User Authentication (pass email and password only in payload)
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("Post/login")]
        public IActionResult LoginUser(UserDTO user)
        {
            UserBDC userBDC = new UserBDC();
            DataContainer<UserDTO> authenticatedUser = new DataContainer<UserDTO>();

            authenticatedUser = userBDC.AuthentictaeUser(user);

            if (authenticatedUser.isValidData && authenticatedUser.Data.UserId > 0)
            {
                return Ok(authenticatedUser.Data);
            }
            else
            {
                return BadRequest(authenticatedUser.Message);
            }
        }

        /// <summary>
        /// Get List of stocks acquired by user
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("Get/getUserPortfolioList/{userID}")]
        public IActionResult GetUserPortfolio(int userID)
        {
            UserBDC userBDC = new UserBDC();
            DataContainer<UserDTO> authenticatedUser = new DataContainer<UserDTO>();
            if (userID > 0)
            {
                authenticatedUser = userBDC.GetUserPortfolio(userID);
                if (authenticatedUser.isValidData && authenticatedUser.Data.UserId > 0 && authenticatedUser.Data.UserPortfolioDTOs.Count > 0)
                {
                    return Ok(authenticatedUser.Data.UserPortfolioDTOs);
                }

                return BadRequest("User Portforlio Does not Exist");
            }

            return BadRequest("User does not exist");
        }
    }
}
