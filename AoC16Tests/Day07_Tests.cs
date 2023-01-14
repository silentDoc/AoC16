using AoC16.Day07;

namespace AoC16Tests
{
    [TestClass]
    public class Day07_Tests_Part1
    {
        [DataTestMethod]
        [DataRow("abba[mnop]qrst", true)]
        [DataRow("abcd[bddb]xyyx", false)]
        [DataRow("aaaa[qwer]tyui", false)]
        [DataRow("ioxxoj[asdfgh]zxcvbn", true)]
        public void TLS_ABBA_InNonBracketSegment(string input, bool expected)
        {
            var addr = new IPv7Address(input);
            var result = addr.SupportsTLS;
            Assert.AreEqual(expected, result);
        }
    }
    
    [TestClass]
    public class Day07_Tests_Part2
    {
        [DataTestMethod]
        [DataRow("aba[bab]xyz", true)]
        [DataRow("xyx[xyx]xyx", false)]
        [DataRow("aaa[kek]eke", true)]
        [DataRow("zazbz[bzb]cdb", true)]
        public void SSL_AtLeast_One_ABA_With_BAB_InBrackets(string input, bool expected)
        {
            var addr = new IPv7Address(input);
            var result = addr.SupportsSSL;
            Assert.AreEqual(expected, result);
        }

    }
}