using AoC16.Day21;


namespace AoC16Tests
{
    [TestClass]
    public class Day21_Tests_Part1
    {
        [DataTestMethod]
        [DataRow("abcde", 4, 0, "ebcda")]
        [DataRow("abcde", 2, 1, "acbde")]
        public void Should_Swap_Positions(string input, int pos1, int pos2, string expected)
        {
            Scrambler scr = new();
            var result = scr.Swap(input, pos1, pos2);
            Assert.AreEqual(expected, result);
        }

        [DataTestMethod]
        [DataRow("abcde", 'e', 'a', "ebcda")]
        [DataRow("abcde", 'c', 'b', "acbde")]
        public void Should_Swap_Letters(string input, char letter1, char letter2, string expected)
        {
            Scrambler scr = new();
            var result = scr.Swap(input, letter1, letter2);
            Assert.AreEqual(expected, result);
        }

        [DataTestMethod]
        [DataRow("abcde", 0, 4, "edcba")]
        [DataRow("abcde", 1, 3, "adcbe")]
        public void Should_Reverse(string input, int start, int end,  string expected)
        {
            Scrambler scr = new();
            var result = scr.Reverse(input, start, end);
            Assert.AreEqual(expected, result);
        }

        [DataTestMethod]
        [DataRow("abcde", 0, "abcde")]
        [DataRow("abcde", 2, "cdeab")]
        [DataRow("abcde", 4, "eabcd")]
        public void Should_RotateLeft(string input, int count, string expected)
        {
            Scrambler scr = new();
            var result = scr.RotateLeft(input, count);
            Assert.AreEqual(expected, result);
        }

        [DataTestMethod]
        [DataRow("abcde", 0, "abcde")]
        [DataRow("abcde", 2, "deabc")]
        [DataRow("abcde", 4, "bcdea")]
        public void Should_RotateRight(string input, int count, string expected)
        {
            Scrambler scr = new();
            var result = scr.RotateRight(input, count);
            Assert.AreEqual(expected, result);
        }

        [DataTestMethod]
        [DataRow("ecabd", 'd', "decab")]
        [DataRow("abcde", 'a', "eabcd")]
        [DataRow("abcde", 'b', "deabc")]
        public void Should_RotateOnChar(string input, char target, string expected)
        {
            Scrambler scr = new();
            var result = scr.Rotate(input, target);
            Assert.AreEqual(expected, result);
        }

        [DataTestMethod]
        [DataRow("bcdea", 1, 4, "bdeac")]
        [DataRow("bdeac", 3, 0, "abdec")]
        public void Should_Move(string input, int source, int target, string expected)
        {
            Scrambler scr = new();
            var result = scr.Move(input, source, target);
            Assert.AreEqual(expected, result);
        }

    }
}