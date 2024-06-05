using CheckoutKata;
using System.Collections;

namespace CheckoutKata_Tests
{
    internal class DefaultPricingRules_Tests
    {

        [TestCaseSource(nameof(TestData))]
        public int Calculate_Returns_The_Correct_Price(IEnumerable<string> items)
        {
            var rules = new DefaultPricingRules();
            return rules.Calculate(items);
        }

        private static IEnumerable TestData
        { 
            get 
            { 
                // single items
                yield return new TestCaseData(new List<string>(){"A"}).Returns(50);
                yield return new TestCaseData(new List<string>(){"B"}).Returns(30);
                yield return new TestCaseData(new List<string>(){"C"}).Returns(20);
                yield return new TestCaseData(new List<string>(){"D"}).Returns(15);
                
                // 2 items
                yield return new TestCaseData(new List<string>(){"A", "A"}).Returns(100);
                yield return new TestCaseData(new List<string>(){"B", "B"}).Returns(45); // discounted
                yield return new TestCaseData(new List<string>(){"C", "C"}).Returns(40); 
                yield return new TestCaseData(new List<string>(){"D", "D"}).Returns(30);
                
                // 3 items
                yield return new TestCaseData(new List<string>(){"A", "A", "A"}).Returns(130); // discounted
                yield return new TestCaseData(new List<string>(){"B", "B", "B"}).Returns(75); // discounted (1 full price)
                yield return new TestCaseData(new List<string>(){"C", "C", "C"}).Returns(60);
                yield return new TestCaseData(new List<string>(){"D", "D", "D"}).Returns(45);

                // discounted items
                yield return new TestCaseData(new List<string>(){"A", "A", "A", "A", "A", "A"}).Returns(260); // double discounted
                yield return new TestCaseData(new List<string>(){"B", "B", "B", "B"}).Returns(90); // double discounted

                // out of order items
                // 6 * A = 260
                // 3 * B =  75
                // 4 * C =  80
                // 6 * D =  90   
                yield return new TestCaseData(new List<string>()
                    {"A", "D", "B", "A", "C", 
                     "D", "D", "A", "C", "B",
                     "A", "D", "B", "A", "D",
                     "A", "C", "C", "D"}).Returns(505);

                // invalid items
                yield return new TestCaseData(new List<string>(){"X", "Y", "Z"}).Returns(0);

                // invalid items + valid items
                yield return new TestCaseData(new List<string>(){"X", "A", "Y", "A", "Z", "A", "B"}).Returns(160);
            }
        }
    }
}
