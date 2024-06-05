using CheckoutKata;
using Moq;
using System.Collections;

namespace CheckoutKata_Tests
{
    public class Checkout_Tests
    {
        /// <summary>
        /// Ensure the Checkout defers the calculation of total price to the rules
        /// </summary>
        /// <param name="expected"></param>
        [TestCase(-1)]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        public void Checkout_Returns_Calculated_Value_From_Rules(int expected)
        {
            var rules = new Mock<IRules>();
            rules.Setup(r => r.Calculate(It.IsAny<IEnumerable<string>>())).Returns(expected);
            var checkout = new Checkout(rules.Object);
            Assert.That(checkout.GetTotalPrice(), Is.EqualTo(expected));
        }

        /// <summary>
        /// Ensure all items scanned by the checkout are passed to the price Calulator (IRules)
        /// </summary>
        /// <param name="items"></param>
        [TestCaseSource(nameof(ScannedItems))]
        public void Checkout_Passes_Scanned_Items_To_Rules(IEnumerable<string> items)
        {
            var rules = new Mock<IRules>();
            IEnumerable<string> itemParams = [];
            rules.Setup(r => r.Calculate(It.IsAny<IEnumerable<string>>())).Callback<IEnumerable<string>>(e => itemParams = e);

            var checkout = new Checkout(rules.Object);

            foreach (var item in items)
                checkout.Scan(item);
            
            checkout.GetTotalPrice();

            CollectionAssert.AreEquivalent(items, itemParams);
        }

        private static IEnumerable ScannedItems
        { 
            get 
            { 
                yield return new List<string>(){"A"};
                yield return new List<string>(){"A", "B"};
                yield return new List<string>(){"A", "B", "A", "C"};
                yield return new List<string>(){"A", "A", "A", "B"};
                yield return new List<string>(){"A", "B", "G", "Z", "XXX"};
            }
        }
    }
}