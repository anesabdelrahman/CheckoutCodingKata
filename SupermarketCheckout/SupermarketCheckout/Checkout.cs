using System.Collections.Generic;
using System.Linq;

namespace SupermarketCheckout
{
    public class Checkout : ICheckout, IBagCostCalculator
    {
        private IList<Product> _products = new List<Product>();
        private IList<SpecialPrice> _specialPrices = new List<SpecialPrice>();
        private readonly IList<Product> _scannedItems = new List<Product>();
        private int _totalPrice;
        private readonly ICalculator _calculator;

        public Checkout(ICalculator calculator)
        {
            _calculator = calculator;
        }

        public Checkout() { }

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
                        _totalPrice += _calculator.CalculateSpecialPrice(specialPrice, group);
                }
                else
                {
                    _totalPrice += _calculator.CalculateStandardPrice(group, _scannedItems);
                }
            }
            return _totalPrice;
        }

        public int GetCostOfBags(int numberOfItems)
        {
            var costOfBags = 0;
            var numberOfBags = numberOfItems / 5;
            numberOfBags += numberOfItems % 5;

            if (numberOfItems <= 5)
            {
                costOfBags += 5;
            }
            else
            {
                costOfBags = numberOfBags * 5;
            }

            return costOfBags;
        }
    }
}
