using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Nagarro.NAGP.EBroker.Business.Services;
using Nagarro.NAGP.EBroker.Shared.Models;
using Nagarro.NAGP.EBroker.Shared.Utilities;

namespace Nagarro.NAGP.EBroker.API.Controllers
{

    [ApiController]
    public class EquityController : ControllerBase
    {
        private readonly IEquityService _equityService;

        public EquityController(IEquityService equityService)
        {
            _equityService = equityService;
        }

        [HttpGet]
        [HttpGet("{id}")]
        [Route("api/[controller]/getUserEquities")]
        public IEnumerable<UserEquity> GetUserEquities([FromQuery] int id)
        {
            return _equityService.GetUserEquities(id);
        }

        [HttpPost]
        [Route("api/[controller]/buyEquity")]
        public IActionResult BuyNewEquity([FromBody] UserEquity userEquity)
        {
            try
            {
                if (EquityUtility.IsValidDate(userEquity.Date))
                {
                    return Ok(_equityService.BuyNewEquity(userEquity));
                }
                else
                {
                    return BadRequest("Invalid Time to Buy Equity");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("api/[controller]/sellEquity")]
        public IActionResult SellEquity([FromBody] UserEquity userEquity)
        {
            try
            {
                if (EquityUtility.IsValidDate(userEquity.Date))
                {
                    return Ok(_equityService.SellEquity(userEquity));
                }
                else
                {
                    return BadRequest("Invalid Time to Sell Equity");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("api/[controller]/addFunds")]
        public IActionResult AddFunds([FromBody] UserFund userFund)
        {
            try
            {
                return Ok(_equityService.AddFunds(userFund.UserId, userFund.Funds));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}