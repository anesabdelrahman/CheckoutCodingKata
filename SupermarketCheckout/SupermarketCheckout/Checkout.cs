using System;

namespace SupermarketCheckout
{
    public class Checkout : ICheckout
    {
        private int _totalPrice = 0;
        public void Scan(string sku)
        {
            if (sku == "A")
            {
                _totalPrice = 50;
            }
        }

        public int GetTotalPrice()
        {
            return _totalPrice;
        }
    }
}
