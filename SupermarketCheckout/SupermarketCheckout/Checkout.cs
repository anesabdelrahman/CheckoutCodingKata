using System.Collections.Generic;
using System.Linq;

namespace SupermarketCheckout
{
    public class Checkout : ICheckout
    {
        private IList<Product> _products = new List<Product>();
        private IList<SpecialPrice> _specialPrices = new List<SpecialPrice>();
        private readonly IList<Product> _scannedItems = new List<Product>();
        private int _totalPrice = 0;
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

            _scannedItems.Add(_products.FirstOrDefault(p => p.Sku == sku));
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
                        var discountedGroups = (int)(groupCount / specialPrice.Quantity);
                        var nonDiscountedPrice = groupCount % specialPrice.Quantity;

                        if (discountedGroups > 0)
                        {
                            _totalPrice += discountedGroups * specialPrice.Price;
                        }

                        _totalPrice += specialPrice.Price;
                        var firstOrDefault = group.FirstOrDefault();
                        if (firstOrDefault != null)
                            _totalPrice += nonDiscountedPrice * firstOrDefault.Price;
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
    }
}
