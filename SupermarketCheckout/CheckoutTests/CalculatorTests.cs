using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using SupermarketCheckout;

namespace CheckoutTests
{
    [TestFixture]
    [Category("Calculators Tests")]
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

        [Test]
        [Category("Addig Bags Tests")]
        [TestCase(1, 5)]
        [TestCase(3, 5)]
        [TestCase(6, 10)]
        [TestCase(10, 10)]
        [TestCase(11, 15)]
        [TestCase(26, 30)]
        public void When_Buying_Items_It_Should_Add_One_Bag_Per_5_Items(int numberOfItems, int expectedCostOfBags)
        {
            var sut = new Checkout();
            var result = sut.GetCostOfBags(numberOfItems);

            Assert.AreEqual(result, expectedCostOfBags);
        }
        #region Private Methods
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
        #endregion Private Methods


    }
}
