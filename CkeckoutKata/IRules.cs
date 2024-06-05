namespace CheckoutKata
{
    public interface IRules
    {
        int Calculate(IEnumerable<string> items);
    }
}
