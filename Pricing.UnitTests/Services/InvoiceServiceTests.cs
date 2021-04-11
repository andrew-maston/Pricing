using Moq;
using NUnit.Framework;
using Pricing.Models;
using Pricing.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pricing.UnitTests.Services
{
    [TestFixture]
    public class InvoiceServiceTests
    {

        private Mock<IProductService> _productService { get; set; }
        private Mock<IOfferService> _offerService { get; set; }
        private InvoiceService _invoiceService { get; set; }

        [SetUp]
        public void SetUp()
        {
            _productService = new Mock<IProductService>();
            _offerService = new Mock<IOfferService>();
            _invoiceService = new InvoiceService(_productService.Object, _offerService.Object);

            _productService.Setup(s => s.GetAll()).ReturnsAsync(new List<Product>
            {
                new Product
                {
                    Name = "Bread",
                    Price = 1.1m
                },
                new Product
                {
                    Name = "Milk",
                    Price = 0.5m
                },
                new Product
                {
                    Name = "Cheese",
                    Price = 0.9m
                },
                new Product
                {
                    Name = "Soup",
                    Price = 0.6m
                },
                new Product
                {
                    Name = "Butter",
                    Price = 1.2m
                }
            }.ToArray());

            _offerService.Setup(s => s.GetAll()).ReturnsAsync(new List<Offer>
            {
                new Offer
                {
                    QualifyingProductName = "Cheese",
                    QuantityRequired = 2,
                    Discount = new OfferModifier
                    {
                        DiscountedProductName = "Cheese",
                        PriceMultiplier = 0
                    }
                },
                new Offer
                {
                    QualifyingProductName = "Soup",
                    QuantityRequired = 1,
                    Discount = new OfferModifier
                    {
                        DiscountedProductName = "Bread",
                        PriceMultiplier = 0.5m
                    }
                },
                new Offer
                {
                    QualifyingProductName = "Butter",
                    QuantityRequired = 1,
                    Discount = new OfferModifier
                    {
                        DiscountedProductName = "Butter",
                        PriceMultiplier = 0.66m
                    }
                }
            }.ToArray());
        }

        [Test]
        public async Task Calculate_BuySoupAndTwoBreads_ExpectedTotals()
        {
            var expectedSubTotal = 2.8m;
            var expectedSaving = 0.55m;
            var expectedTotalWithSavings = 2.25m;
            var orderItems = new List<OrderItem>
            {
                new OrderItem
                {
                    ProductName = "Soup",
                    Quantity = 1
                },
                new OrderItem
                {
                    ProductName = "Bread",
                    Quantity = 2
                }
            };

            var invoice = await _invoiceService.Calculate(orderItems);

            Assert.IsNotNull(invoice);
            Assert.AreEqual(expectedSubTotal.ToString("C"), invoice.SubTotal);
            Assert.AreEqual(expectedSaving.ToString("C"), invoice.TotalSaving);
            Assert.AreEqual(expectedTotalWithSavings.ToString("C"), invoice.TotalWithSavings);
        }

        [Test]
        [TestCase(2.7, 0.9, 1.8, 3)]
        [TestCase(3.6, 1.8, 1.8, 4)]
        public async Task Calculate_BuyCheeses_ExpectedTotals(decimal expectedSubTotal, decimal expectedSaving, decimal expectedTotalWithSavings, int quantity)
        {
            var orderItems = new List<OrderItem>
            {
                new OrderItem
                {
                    ProductName = "Cheese",
                    Quantity = quantity
                }
            };

            var invoice = await _invoiceService.Calculate(orderItems);

            Assert.IsNotNull(invoice);
            Assert.AreEqual(expectedSubTotal.ToString("C"), invoice.SubTotal);
            Assert.AreEqual(expectedSaving.ToString("C"), invoice.TotalSaving);
            Assert.AreEqual(expectedTotalWithSavings.ToString("C"), invoice.TotalWithSavings);
        }

        [Test]
        public async Task Calculate_BuyButter_ExpectedTotals()
        {
            var expectedSubTotal = 1.2m;
            var expectedSaving = 0.41m;
            var expectedTotalWithSavings = 0.79m;
            var orderItems = new List<OrderItem>
            {
                new OrderItem
                {
                    ProductName = "Butter",
                    Quantity = 1
                }
            };

            var invoice = await _invoiceService.Calculate(orderItems);

            Assert.IsNotNull(invoice);
            Assert.AreEqual(expectedSubTotal.ToString("C"), invoice.SubTotal);
            Assert.AreEqual(expectedSaving.ToString("C"), invoice.TotalSaving);
            Assert.AreEqual(expectedTotalWithSavings.ToString("C"), invoice.TotalWithSavings);
        }
    }
}
