using System.Collections.Generic;
using System.Linq;

namespace SupermarketCheckout
{
    public class Checkout : ICheckout
    {
        private IList<Product> _products = new List<Product>();
        private IList<SpecialPrice> _specialPrices = new List<SpecialPrice>();
        private readonly IList<Product> _scannedItems = new List<Product>();
        private int _totalPrice;

        public void Scan(string sku)
        {
            if (!_products.Any())
            {
                _products = ProductRepository.BuildProducts();
            }

            if (!_specialPrices.Any())
            {
                _specialPrices = SpecialPriceRepository.LoadSpecialPrices();
            }

            var currentProduct = _products.FirstOrDefault(p => p.Sku == sku);
            if (currentProduct != null)
            {
                _scannedItems.Add(_products.FirstOrDefault(p => p.Sku == sku));
            }
        }

        public int GetTotalPrice()
        {
            var grouppedItems = _scannedItems.GroupBy(x => x.Sku);

            foreach (var group in grouppedItems)
            {
                var specialPrice = _specialPrices.FirstOrDefault(sp => sp.SkuId == group.Key);
                if (specialPrice != null)
                {
                    var groupCount = group.Count() - specialPrice.Quantity;

                    if (groupCount < 0)
                    {
                        _totalPrice += group.Sum(g => g.Price);
                    }
                    else if (group.Count() > specialPrice.Quantity)
                    {
                       _totalPrice += CalculateSpecialPrice(group, specialPrice, groupCount);
                    }
                    else
                    {
                        _totalPrice += specialPrice.Price;
                        var firstOrDefault = group.FirstOrDefault();
                        if (firstOrDefault != null) _totalPrice += groupCount * firstOrDefault.Price;
                    }
                }
                else
                {
                    _totalPrice += _scannedItems.Where(si => si.Sku == group.Key).Sum(i => i.Price);
                }
            }
            return _totalPrice;
        }

        private static int CalculateSpecialPrice(IGrouping<string, Product> group, SpecialPrice specialPrice, int groupCount)
        {
            var result = 0;
            var discountedGroups = (int)(groupCount / specialPrice.Quantity);
            var nonDiscountedPrice = groupCount % specialPrice.Quantity;

            if (discountedGroups > 0)
            {
                result += discountedGroups * specialPrice.Price;
            }

            result += specialPrice.Price;

            if (group.FirstOrDefault() != null)
            {
                result += nonDiscountedPrice * group.FirstOrDefault().Price;
            }

            return result;
        }
    }
}
