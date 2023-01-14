using AoC16.Day09;

namespace AoC16Tests
{
    [TestClass]
    public class Day09_Tests
    {
        SequenceDecompressor _decompressor = new();

        [DataTestMethod]
        [DataRow("ADVENT","ADVENT")]
        [DataRow("PARAPA", "PARAPA")]
        [DataRow("BOO", "BOO")]
        public void NoMarkers_ShouldDecompressToItself(string input, string expected)
        {
            var result = _decompressor.Decompress(input);
            Assert.AreEqual(expected, result);
        }

        [DataTestMethod]
        [DataRow("A(1x5)BC", "ABBBBBC")]
        [DataRow("(3x3)XYZ", "XYZXYZXYZ")]
        [DataRow("AA(3x3)XYZBB", "AAXYZXYZXYZBB")]
        public void ShouldProcessSingleMarkers(string input, string expected)
        {
            
            var result = _decompressor.Decompress(input);
            Assert.AreEqual(expected, result);
        }

        [DataTestMethod]
        [DataRow("A(2x2)BCD(2x2)EFG", "ABCBCDEFEFG")]
        [DataRow("A(1x5)BC(3x3)XYZ", "ABBBBBCXYZXYZXYZ")]
        public void ShouldProcessManySingleMarkers(string input, string expected)
        {
            var result = _decompressor.Decompress(input);
            Assert.AreEqual(expected, result);
        }

        [DataTestMethod]
        [DataRow("X(8x2)(3x3)ABCY", "X(3x3)ABC(3x3)ABCY")]
        [DataRow("(6x1)(1x3)A", "(1x3)A")]
        public void ShouldMarkersInSequence(string input, string expected)
        {
            var result = _decompressor.Decompress(input);
            Assert.AreEqual(expected, result);
        }

    }
}