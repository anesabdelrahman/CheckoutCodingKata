using System.Collections.Generic;
using System.Linq;

namespace SupermarketCheckout
{
    public class Checkout : ICheckout
    {
        public IList<Product> Products = new List<Product>();
        public IList<Product> ScannedItems = new List<Product>();

        private int _totalPrice = 0;
        public void Scan(string sku)
        {
            if (!Products.Any())
            {
                Products = ProductRepository.BuildProducts();
            }

            ScannedItems.Add(Products.FirstOrDefault(p => p.Sku == sku));
        }

        public int GetTotalPrice()
        {
            var currentProduct = ScannedItems.Where(i => i.SpecialPrice != null).GroupBy(x => x.Sku);

            foreach (var item in ScannedItems)
            {
                if (item?.SpecialPrice != null && ScannedItems.Count == item.SpecialPrice.Quantity)
                {
                    _totalPrice = item.SpecialPrice.Price;
                }
                else
                {
                    if (item != null) _totalPrice += item.Price;
                }
            }
            
            return _totalPrice;
        }
    }
}
