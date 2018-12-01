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
    }
}
