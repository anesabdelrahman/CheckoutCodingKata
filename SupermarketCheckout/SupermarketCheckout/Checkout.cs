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
            var grouppedItems = ScannedItems.Where(i => i.SpecialPrice != null).GroupBy(x => x.Sku);

            foreach (var group in grouppedItems)
            {
                var groupCount = group.Count() - group.FirstOrDefault().SpecialPrice.Quantity;
                if (groupCount < 0)
                {
                    _totalPrice += group.Sum(g => g.Price);
                }
                else
                {
                    _totalPrice += group.FirstOrDefault().SpecialPrice.Price;
                    _totalPrice += groupCount * group.FirstOrDefault().Price;
                }
            }

            _totalPrice += ScannedItems.Where(i => i.SpecialPrice == null).Sum(i => i.Price);
            return _totalPrice;
        }
    }
}
