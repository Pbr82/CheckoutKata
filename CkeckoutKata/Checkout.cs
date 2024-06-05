namespace CheckoutKata
{
    public class Checkout : ICheckout
    {
        private readonly IRules rules;

        public Checkout(IRules rules)
        {
            this.rules = rules ?? throw new ArgumentNullException(nameof(rules));
        }

        public int GetTotalPrice()
        {
            throw new NotImplementedException();
        }

        public void Scan(string item)
        {
            throw new NotImplementedException();
        }
    }
}
