using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pricing.Models;
using Pricing.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pricing.Controllers
{
    /// <summary>
    /// Product controller, used for retrieving products
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductService _productService;

        /// <summary>ctor</summary>
        public ProductController(IProductService productService, ILogger<ProductController> logger)
        {
            _logger = logger;
            _productService = productService;
        }

        /// <summary>
        /// Get all available products
        /// </summary>
        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(IReadOnlyCollection<Product>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            IReadOnlyCollection<Product> products;

            try
            {
                products = await _productService.GetAll();
            }
            catch (Exception ex)
            {
                _logger.LogError("Something went wrong retrieving products", ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return Ok(products);
        }
    }
}
