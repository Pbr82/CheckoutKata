namespace CheckoutKata
{
    public class Checkout : ICheckout
    {
        private readonly IRules rules;

        private readonly List<string> scannedItems = [];

        public Checkout(IRules rules)
        {
            this.rules = rules ?? throw new ArgumentNullException(nameof(rules));
        }

        public int GetTotalPrice() => rules.Calculate(scannedItems);

        public void Scan(string item) => scannedItems.Add(item);
    }
}
