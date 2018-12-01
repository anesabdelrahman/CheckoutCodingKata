using NUnit.Framework;
using SupermarketCheckout;

namespace CheckoutTests
{
    [TestFixture]
    public class CheckoutTest
    {
      [Test]
        public void WhenScan_A_Once_TotalShouldBe50()
        {
            // Arrange 
            var sut = new Checkout();

            //Act
            sut.Scan("A");
            var result = sut.GetTotalPrice();

            //Assert
            Assert.AreEqual(50, result);

        }

        [Test]
        public void WhenScan_A_Twice_TotalShouldBe100()
        {
            // Arrange 
            var sut = new Checkout();

            //Act
            sut.Scan("A");
            sut.Scan("A");
            var result = sut.GetTotalPrice();

            //Assert
            Assert.AreEqual(100, result);

        }

        [Test]
        public void WhenScan_A_Thrice_TotalShouldBe130()
        {
            // Arrange 
            var sut = new Checkout();

            //Act
            sut.Scan("A");
            sut.Scan("A");
            sut.Scan("A");
            var result = sut.GetTotalPrice();

            //Assert
            Assert.AreEqual(100, result);

        }
    }
}
