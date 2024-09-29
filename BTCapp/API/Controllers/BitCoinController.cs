using Microsoft.AspNetCore.Mvc;
using BTCapp.API.ApiModel;
using BTCapp.Contracts;
using BTCapp.API.Mappings;
using System.ComponentModel.DataAnnotations;
namespace BTC.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BtcController : ControllerBase
    {
        public IApplicationService _appservice;
        public BtcController(IApplicationService appservice)
        {
            _appservice = appservice;
        }
        private static readonly DateTime BitcoinCreationDate = new DateTime(2009, 1, 3);
        private static readonly DateTime OnlineReposLimit = new DateTime(2013,4,1,1,0,0);//2013-04-01T01:00:00
        [HttpGet]
        public async Task<IActionResult> Get(
            [FromQuery, Required] DateTime time,
            CancellationToken cancellationToken)
        {
            if (time==DateTime.MinValue)
                return BadRequest("time cannot be null");

            if(time< BitcoinCreationDate)
                return BadRequest("Bitcoin wasn't invented");

            if (time < OnlineReposLimit)
                return BadRequest("Bitcoin wasn't famous enough for online repo to have price data");

            var query = new GetPriceSingleQuery(time);
            GetPriceSingleQueryResult response = await _appservice.GetPrice(query, cancellationToken);

            return Ok(response.Price.MapToApi());
        }
        
        [HttpGet]
        [Route("Range")]
        public async Task<IActionResult> GetRange(
            [FromQuery, Required] DateTime startDate,
            [FromQuery, Required] DateTime endDate,
            CancellationToken cancellationToken)
        {
            if (startDate < BitcoinCreationDate)
                startDate = BitcoinCreationDate;

            if (startDate > endDate)
                return BadRequest("startDate later than endDate");

            if (startDate < OnlineReposLimit)
                return BadRequest("Bitcoin wasn't famous enough for online repo to have price data");

            var query = new GetPriceMultiQuery(startDate,endDate);
            GetPriceMultiQueryResult response = await _appservice.GetPrices(query, cancellationToken);


            //todo: this with mapping
            return Ok(response.Prices.MapMultiToApi());
        }
    };
}
