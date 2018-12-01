using System;
using NUnit.Framework;
using SupermarketCheckout;

namespace CheckoutTests
{
    [TestFixture]
    public class CheckoutTest
    {
      Checkout _sut;

        [SetUp]
        public void TestSetUp()
        {
            //Arrange
            _sut = new Checkout();
        }

        [Test]
        [Category("Sinle Product Not Exceeding SpecialPrice Quantity Multiple. e.g. less than 6 of A and less than 4 for B")]
        [TestCase("A", 1, 50)]
        [TestCase("A", 2, 100)]
        [TestCase("A", 3, 130)]
        [TestCase("A", 4, 180)]
        [TestCase("B", 1, 30)]
        [TestCase("B", 2, 45)]
        [TestCase("B", 3, 75)]
        [TestCase("B", 4, 90)]
        [TestCase("C", 1, 20)]
        [TestCase("D", 1, 15)]
        public void WhenScanSingleProducts_ItShouldTotalPriceAndAdjustForSpecialPriceDiscount(string sku, int productQuantity, int expectedPrice)
        {
            //Act
            for (var i = 0; i < productQuantity; i++)
            {
                _sut.Scan(sku);
            }
            
            var result = _sut.GetTotalPrice();

            //Assert
            Assert.AreEqual(result, expectedPrice);

        }

        [Test]
        [Category("Product A & B")]
        [TestCase("A", 1, "B", 1, 80)]
        [TestCase("A", 2, "B", 1, 130)]
        [TestCase("A", 3, "B", 1, 160)]
        [TestCase("A", 4, "B", 1, 210)]
        [TestCase("A", 5, "B", 1, 260)]
        [TestCase("A", 6, "B", 1, 290)]
        [TestCase("A", 1, "B", 2, 95)]
        [TestCase("A", 1, "B", 3, 125)]
        [TestCase("A", 1, "B", 4, 140)]
        [TestCase("A", 1, "B", 5, 170)]
        public void WhenScan_A_and_B_ItShouldTotalPricesAndAdjustForSpecialPricesDiscounts(string productA, int productAQuantity, string productB, int productBQuantity, int expectedPrice)
        {
            //Act
            for (var i = 0; i < productAQuantity; i++)
            {
                _sut.Scan(productA);
            }

            for (var i = 0; i < productBQuantity; i++)
            {
                _sut.Scan(productB);
            }

            var result = _sut.GetTotalPrice();

            //Assert
            Assert.AreEqual(result, expectedPrice);
        }

        [Test]
        [Category("All Products")]
        [TestCase("A", 1, "B", 1, "C", 1, "D", 1, 115)]
        [TestCase("A", 2, "B", 1, "C", 1, "D", 1, 165)]
        [TestCase("A", 3, "B", 1, "C", 1, "D", 1, 195)]
        [TestCase("A", 4, "B", 1, "C", 1, "D", 1, 245)]
        [TestCase("A", 5, "B", 1, "C", 1, "D", 1, 295)]
        [TestCase("A", 6, "B", 1, "C", 1, "D", 1, 325)]
        [TestCase("A", 1, "B", 2, "C", 1, "D", 1, 130)]
        [TestCase("A", 1, "B", 3, "C", 1, "D", 1, 160)]
        [TestCase("A", 1, "B", 4, "C", 1, "D", 1, 175)]
        [TestCase("A", 1, "B", 5, "C", 1, "D", 1, 205)]
        [TestCase("A", 7, "B", 6, "C", 2, "D", 2, 515)]
        [TestCase("A", 20, "B", 40, "C", 35, "D", 66, 3470)]
        public void WhenScan_AllProducts_ItShouldTotalPricesAndAdjustForSpecialPricesDiscounts(string productA, int productAQuantity,
            string productB, int productBQuantity,
            string productC, int productCQuantity,
            string productD, int productDQuantity,
            int expectedPrice)
        {
            //Act
            for (var i = 0; i < productAQuantity; i++)
            {
                _sut.Scan(productA);
            }

            for (var i = 0; i < productBQuantity; i++)
            {
                _sut.Scan(productB);
            }

            for (var i = 0; i < productCQuantity; i++)
            {
                _sut.Scan(productC);
            }

            for (var i = 0; i < productDQuantity; i++)
            {
                _sut.Scan(productD);
            }

            var result = _sut.GetTotalPrice();

            //Assert
            Assert.AreEqual(result, expectedPrice);
        }
    }
}
