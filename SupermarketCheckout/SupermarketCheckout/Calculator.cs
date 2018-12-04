using System.Collections.Generic;
using System.Linq;

namespace SupermarketCheckout
{
    public class Calculator : ICalculator
    {
        public int CalculateSpecialPrice(SpecialPrice specialPrice, IGrouping<string, Product> grouping)
        {
            var result = 0;
            var groupCount = grouping.Count() - specialPrice.Quantity;

            //1- No Special price, just add standard price
            if (groupCount < 0)
            {
                result += grouping.Sum(g => g.Price);
                return result;
            }

            //2- Scanned items exactly equals special price quantity.
            if ( grouping != null && grouping.Count() == groupCount)
            {
                result += specialPrice.Price;
                var group = grouping.FirstOrDefault();
                if (group != null) result += groupCount * group.Price;
                return result;
            }

            //3- Scanned items > special price quantity & < twice the special price quantity
            result += specialPrice.Price;

            if (grouping != null && !grouping.Any())
            {
                return result;
            }

            //4- Scanned items > twice the special price quantity
            var discountedGroups = groupCount / specialPrice.Quantity;
            var nonDiscountedPrice = groupCount % specialPrice.Quantity;

            if (discountedGroups > 0)
            {
                result += discountedGroups * specialPrice.Price;
            }

            var product = grouping?.FirstOrDefault();
            if (product != null)
            {
                result += nonDiscountedPrice * product.Price;
            }

            return result;
        }

        public int CalculateStandardPrice(IGrouping<string, Product> group, IList<Product> items )
        {
            return items.Where(si => si.Sku == group.Key).Sum(i => i.Price);
        }
    }
}