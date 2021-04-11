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
    /// Offer controller, used for retrieving offers
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class OfferController : ControllerBase
    {
        private readonly ILogger<OfferController> _logger;
        private readonly IOfferService _OfferService;

        /// <summary>ctor</summary>
        public OfferController(IOfferService OfferService, ILogger<OfferController> logger)
        {
            _logger = logger;
            _OfferService = OfferService;
        }

        /// <summary>
        /// Get all available offers
        /// </summary>
        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(IReadOnlyCollection<Offer>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            IReadOnlyCollection<Offer> offers;

            try
            {
                offers = await _OfferService.GetAll();
            }
            catch (Exception ex)
            {
                _logger.LogError("Something went wrong retrieving offers", ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return Ok(offers);
        }
    }
}
