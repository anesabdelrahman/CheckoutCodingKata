using System.Collections.Generic;

namespace SupermarketCheckout
{
    public static class SpecialPriceRepository
    {
        public static IList<SpecialPrice> LoadSpecialPrices()
        {
            var specialPrices = new List<SpecialPrice>
            {
                 new SpecialPrice { Quantity = 3, Price = 130, SkuId = "A" },
                 new SpecialPrice { Quantity = 2, Price = 45, SkuId = "B" }
            };

            return specialPrices;
        }
    }
}
