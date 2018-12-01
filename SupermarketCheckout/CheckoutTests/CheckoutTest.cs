﻿using NUnit.Framework;
using SupermarketCheckout;

namespace CheckoutTests
{
    [TestFixture]
    public class CheckoutTest
    {
      Checkout sut;

        [SetUp]
        public void TestSetUp()
        {
            //Arrange
            sut = new Checkout();
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
        [TestCase("C", 1, 20)]
        [TestCase("D", 1, 15)]
        public void WhenScanSingleProducts_ItShouldTotalPriceAndAdjustForSpecialPriceDiscount(string sku, int productQuantity, int expectedPrice)
        {
            //Act
            for (int i = 0; i < productQuantity; i++)
            {
                sut.Scan(sku);
            }
            
            var result = sut.GetTotalPrice();

            //Assert
            Assert.AreEqual(result, expectedPrice);

        }

    }
}
