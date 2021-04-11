using Pricing.Extensions;
using Pricing.Models;
using Pricing.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pricing.Services
{
    /// <inheritdoc/>
    public class InvoiceService : IInvoiceService
    {
        private readonly IProductService _productService;
        private readonly IOfferService _offerService;

        /// <summary>ctor</summary>
        public InvoiceService(IProductService productService, IOfferService offerService)
        {
            _offerService = offerService;
            _productService = productService;
        }

        /// <inheritdoc/>
        public async Task<Invoice> Calculate(IReadOnlyCollection<OrderItem> orderItems)
        {
            var products = await _productService.GetAll();
            var orderRows = orderItems.GetOrderRows(products);

            var invoice = new Invoice
            {
                Status = orderRows.Any(r => r.Product.Price == 0.0m) 
                ? InvoiceStatus.PriceNotFound 
                : InvoiceStatus.Calculated,
                OrderRows = orderRows
            };

            //return prior to calculating savings
            if (invoice.Status == InvoiceStatus.PriceNotFound)
            {
                return invoice;
            }

            invoice = await CheckAndApplySavings(invoice);

            return invoice;
        }

        private async Task<Invoice> CheckAndApplySavings(Invoice invoice)
        {
            var offers = await _offerService.GetAll();
            
            //perhaps there could be a scenario where there are no offers
            if (offers.Any()) {
                foreach (var row in invoice.OrderRows) {
                    //again we probalby wouldn't use the product name as a key in the real world
                    if (offers.Any(o => o.QualifyingProductName == row.Product.Name && row.Quantity >= o.QuantityRequired && invoice.OrderRows.Any(r => r.Product.Name == o.Discount.DiscountedProductName)))
                    {
                        ApplySavings(invoice, offers, row);
                    }
                } 
            }

            return invoice;
        }

        private void ApplySavings(Invoice invoice, IReadOnlyCollection<Offer> offers, OrderRow row)
        {
            //apply offer
            var offer = offers.FirstOrDefault(o => o.QualifyingProductName == row.Product.Name && row.Quantity >= o.QuantityRequired);
            var discountedQuantity = row.Quantity / offer.QuantityRequired;

            var discountRow = invoice.OrderRows.FirstOrDefault(r => r.Product.Name == offer.Discount.DiscountedProductName);
            var subTotalWithSavings = 0.0m;
            for (var i = 1; i <= discountRow.Quantity; i++)
            {
                if (discountedQuantity >= i)
                {
                    subTotalWithSavings += (discountRow.Product.Price * offer.Discount.PriceMultiplier);
                }
                else
                {
                    subTotalWithSavings += discountRow.Product.Price;
                }
            }

            discountRow.Saving = discountRow.SubTotal - subTotalWithSavings;
        }
    }
}