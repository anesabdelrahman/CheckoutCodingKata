using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using SupermarketCheckout;

namespace CheckoutTests
{
    [TestFixture]
    [Category("Calculator Tests")]
    public class CalculatorTests
    {
        private ICalculator _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new Calculator();
        }

        [Test]
        public void When_Calling_CalculateStandardPrice_Method_It_Should_Only_Add_Items_Prices()
        {
            var items = GetProduct();
            var grouping = items.GroupBy(i => i.Sku);

            var result = grouping.Sum(group => _sut.CalculateStandardPrice(group, items));

            Assert.AreEqual(115, result);
        }

        [Test]
        public void When_Calling_CalculateSpecialPrice_Method_It_Should_Add_Prices_And_Adjust_For_Special_Prices()
        {
            var result = 0;
            var items = GetProduct();
            var specialPrices = GetSpecialPrices();
            var grouping = items.GroupBy(i => i.Sku);

            foreach (var group in grouping)
            {
                var specialPrice = specialPrices.FirstOrDefault(sp => sp.SkuId == group.Key);
                if (specialPrice != null)
                {
                    result += _sut.CalculateSpecialPrice(specialPrice, group);
                }
            }

            Assert.AreEqual(80, result);
        }

        private IList<Product> GetProduct()
        {
            var products = new List<Product>()
            {
                new Product { Sku = "A", Price = 50},
                new Product { Sku = "B", Price = 30},
                new Product { Sku = "C", Price = 20},
                new Product { Sku = "D", Price = 15},
            };

            return products;
        }

        private IList<SpecialPrice> GetSpecialPrices()
        {
            var specialPrices = new List<SpecialPrice>
            {
                 new SpecialPrice { Quantity = 3, Price = 130, SkuId = "A" },
                 new SpecialPrice { Quantity = 2, Price = 45, SkuId = "B" }
            };

            return specialPrices;
        }


    }
}
