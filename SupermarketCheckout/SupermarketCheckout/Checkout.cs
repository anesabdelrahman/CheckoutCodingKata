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
            new Product {Sku = "A", Price = 50, SpecialPrice = new SpecialPrice {Price = 130, Quantity = 3} },
            new Product {Sku = "B", Price = 30, SpecialPrice = new SpecialPrice {Price = 45, Quantity = 2} },
            new Product {Sku = "C", Price = 20},
            new Product {Sku = "D", Price = 15},
        };

        private readonly IList<Product> _scannedProducts = new List<Product>();

        private int _totalPrice = 0;
        public void Scan(string sku)
        {
            var currentProduct = _products.FirstOrDefault(p => p.Sku == sku);
            _scannedProducts.Add(currentProduct);

            if (currentProduct?.SpecialPrice != null && _scannedProducts.Count == currentProduct.SpecialPrice.Quantity)
            {
                _totalPrice = currentProduct.SpecialPrice.Price;
            }
            else
            {
                if (currentProduct != null) _totalPrice += currentProduct.Price;
            }
        }

        public int GetTotalPrice()
        {
            return _totalPrice;
        }
    }
}
