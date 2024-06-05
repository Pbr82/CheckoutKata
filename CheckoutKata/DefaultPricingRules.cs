using System.Diagnostics.CodeAnalysis;

namespace CheckoutKata
{
    public class DefaultPricingRules : IRules
    {
        private sealed class MultibuyDiscount(int per, int discount)
        {
            public int Per => per;
            public int Discount => discount;
        }

        private record PricedItem(string Barcode, int Price, MultibuyDiscount? Discount)
        {
            private static PricedItem A { get; } = new("A", 50, new MultibuyDiscount(3, 20));
            private static PricedItem B { get; } = new("B", 30, new MultibuyDiscount(2, 15));
            private static PricedItem C { get; } = new("C", 20, null);
            private static PricedItem D { get; } = new("D", 15, null);

            private static readonly Dictionary<string, PricedItem> items = new()
            {
                {A.Barcode, A },
                {B.Barcode, B },
                {C.Barcode, C },
                {D.Barcode, D },
            };
            public static bool TryGet(string barcode, [MaybeNullWhen(false)] out PricedItem price) => items.TryGetValue(barcode, out price);
        }

        public int Calculate(IEnumerable<string> items)
        {
            var basket = items.GroupBy(x => x).ToDictionary(g => g.Key, g => g.Count());

            var total = 0;
            foreach (var (barcode, quantity) in basket)
            {
                if (PricedItem.TryGet(barcode, out var price))
                {
                    total += price.Price * quantity;

                    if (price.Discount != null)
                        total -= quantity / price.Discount.Per * price.Discount.Discount;
                }
            }

            return total;
        }
    }
}
