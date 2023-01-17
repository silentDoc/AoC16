using AoC16.Day18;

namespace AoC16Tests
{
    [TestClass]
    public class Day18_Tests_Part1
    {
        [DataTestMethod]
        [DataRow("..^^.", ".^^^^")]
        [DataRow(".^^^^", "^^..^")]
        [DataRow(".^^.^.^^^^", "^^^...^..^")]
        [DataRow("^^^...^..^", "^.^^.^.^^.")]
        [DataRow("^..^^^^.^^", ".^^^..^.^^")]
        public void Should_Generate_Next_Row(string input, string expected)
        {
            TileGenerator gen = new();
            var result = gen.GenerateRow(input);
            Assert.AreEqual(expected, result);
        }
    }
}