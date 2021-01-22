using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SmalShopy.Models;
using SmalShopy.Services;

namespace SmalShopy.Controllers
{
    //api/Exercise/exercise2

    [ApiController]
    [Route("")]
    public class ProductsController : Controller
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly IProductService _productService;


        public ProductsController(ILogger<ProductsController> logger, IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        [HttpGet]
        [Route("/sort")]
        public async Task<IActionResult> Get(string sortOption)
        {
            _logger.LogInformation(1, $"Sorting products.");
            List<Product> products = null;
            try
            {
                products = (await _productService.SortProductsByAsync(sortOption)).ToList();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            if (products == null)
            {
                return NotFound();
            }
            return Ok(products);
        }
    }
}
