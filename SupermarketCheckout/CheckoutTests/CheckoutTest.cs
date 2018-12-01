using NUnit.Framework;
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
        [TestCase("B", 4, 90)]
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
            for (int i = 0; i < productAQuantity; i++)
            {
                sut.Scan(productA);
            }

            for (int i = 0; i < productBQuantity; i++)
            {
                sut.Scan(productB);
            }

            var result = sut.GetTotalPrice();

            //Assert
            Assert.AreEqual(result, expectedPrice);

        }

    }
}
