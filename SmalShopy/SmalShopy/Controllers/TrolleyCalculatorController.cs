using System;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SmalShopy.Models;
using SmalShopy.Services;

namespace SmalShopy.Controllers
{
    [ApiController]
    [Route("")]
    public class TrolleyCalculatorController : Controller
    {
        private readonly ILogger<TrolleyCalculatorController> _logger;
        private readonly ITrolleyCalculatorService _trolleyCalculatorService;


        public TrolleyCalculatorController(ILogger<TrolleyCalculatorController> logger, ITrolleyCalculatorService trolleyCalculatorService)
        {
            _logger = logger;
            _trolleyCalculatorService = trolleyCalculatorService;
        }

        [HttpPost]
        [Route("/trolleyTotal")]
        public async Task<IActionResult> Get([FromBody] Trolley request)
        {
            var jsonData = JsonSerializer.Serialize(request);
            _logger.LogInformation(1,jsonData);
            _logger.LogInformation(1, $"Calculating min of trolley's products.");
            string totalSum;
            try
            {
                //totalSum = await _trolleyCalculatorService.TrolleyCalculate(request);
                totalSum = _trolleyCalculatorService.TrolleyCalculateLocally(request);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(totalSum);
        }
    }
}
