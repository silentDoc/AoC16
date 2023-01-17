using AoC16.Day16;

namespace AoC16Tests
{
    [TestClass]
    public class Day16_Tests_Part1
    {
        [DataTestMethod]
        [DataRow("1", "100")]
        [DataRow("0", "001")]
        [DataRow("11111", "11111000000")]
        [DataRow("10000", "10000011110")]
        [DataRow("10000011110", "10000011110010000111110")]
        [DataRow("111100001010", "1111000010100101011110000")]
        public void Should_DoDragonIteration(string input, string expected)
        {
            RandomDataGen gen = new();
            var result = gen.DragonCurve(input);
            Assert.AreEqual(expected, result);
        }

        [DataTestMethod]
        [DataRow("110010110100", "100")]
        [DataRow("10000011110010000111", "01100")]
        public void Should_DoCheckSum(string input, string expected)
        {
            RandomDataGen gen = new();
            var result = gen.Checksum(input);
            Assert.AreEqual(expected, result);
        }
    }
}