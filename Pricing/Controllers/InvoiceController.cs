using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pricing.Models;
using Pricing.Models.Dtos;
using Pricing.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pricing.Controllers
{
    /// <summary>
    /// Invoice controller, used calculating the bill
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class InvoiceController : ControllerBase
    {
        private readonly ILogger<InvoiceController> _logger;
        private readonly IInvoiceService _invoiceService;

        /// <summary>ctor</summary>
        public InvoiceController(IInvoiceService invoiceService, ILogger<InvoiceController> logger)
        {
            _logger = logger;
            _invoiceService = invoiceService;
        }

        /// <summary>
        /// Calculate an invoice given a request
        /// </summary>
        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType(typeof(Invoice), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Calculate(InvoiceRequestDto invoiceRequest)
        {
            Invoice invoice;

            if (invoiceRequest.Items == null || !invoiceRequest.Items.Any())
            {
                return BadRequest("You must provide the name and quantity of items to calculate an invoice");
            }

            try
            {
                invoice = await _invoiceService.Calculate(invoiceRequest.Items);

                if (invoice.Status == InvoiceStatus.PriceNotFound)
                {
                    var message = @$"Unable to find a price for product(s): {string.Join(", ", invoice.OrderRows.Where(r => r.Product.Price == 0.0m).Select(r => r.Product.Name))}";
                    _logger.LogWarning(message);
                    return NotFound(message);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Something went wrong calculating the invoice", ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return Ok(invoice);
        }
    }
}
