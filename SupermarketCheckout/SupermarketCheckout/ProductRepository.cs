﻿using System.Collections.Generic;

namespace SupermarketCheckout
{
    public static class ProductRepository
    {
        public static IList<Product> BuildProducts()
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
    }
}
