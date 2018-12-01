using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace SupermarketCheckout
{
    public class Checkout : ICheckout
    {
        private readonly IList<Product> _products = new List<Product>
        {
            new Product {Sku = "A", Price = 50},
            new Product {Sku = "B", Price = 30},
            new Product {Sku = "C", Price = 20},
            new Product {Sku = "D", Price = 15},
        };

        private int _totalPrice = 0;
        public void Scan(string sku)
        {
            var firstOrDefault = _products.FirstOrDefault(p => p.Sku == sku);
            if (firstOrDefault != null)
            {
                _totalPrice += firstOrDefault.Price;
            }
        }

        public int GetTotalPrice()
        {
            return _totalPrice;
        }
    }
}
