using System.Collections.Generic;
using System.Linq;

namespace SupermarketCheckout
{
    public interface ICalculator 
    {
        int CalculateSpecialPrice(SpecialPrice specialPrice, IGrouping<string, Product> grouping);
        int CalculateStandardPrice(IGrouping<string, Product> group, IList<Product> items);
    
    }
}