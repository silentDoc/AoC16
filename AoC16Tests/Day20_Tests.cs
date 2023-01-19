using AoC16.Day20;

namespace AoC16Tests
{
    [TestClass]
    public class Day20_Tests_Part1
    {
        [DataTestMethod]
        [DataRow(new[] { 3,8}, new[] { 4, 7 }, true)]
        [DataRow(new[] { 3, 8 }, new[] { 4, 9 }, false)]
        [DataRow(new[] { 2, 5 }, new[] { 4, 7 }, false)]
        [DataRow(new[] { -3, 18 }, new[] { 4, 7 }, true)]
        [DataRow(new[] { 2, 10 }, new[] { 14, 17 }, false)]
        public void Should_Check_FullyContains(int[] a, int[] b, bool expected)
        {
            IPRange ip_a = new IPRange(a[0], a[1]);
            IPRange ip_b = new IPRange(b[0], b[1]);

            var result = ip_a.FullyContains(ip_b);
            Assert.AreEqual(expected, result);
        }

        [DataTestMethod]
        [DataRow(new[] { 3, 8 }, new[] { 0, 17 }, true)]
        [DataRow(new[] { 3, 8 }, new[] { 4, 9 }, false)]
        [DataRow(new[] { 2, 5 }, new[] { 4, 7 }, false)]
        [DataRow(new[] { 4, 7}, new[] { -3, 18 }, true)]
        [DataRow(new[] { 2, 10 }, new[] { 14, 17 }, false)]
        public void Should_Check_IsFullyContained(int[] a, int[] b, bool expected)
        {
            IPRange ip_a = new IPRange(a[0], a[1]);
            IPRange ip_b = new IPRange(b[0], b[1]);

            var result = ip_a.IsFullyContainedBy(ip_b);
            Assert.AreEqual(expected, result);
        }

        [DataTestMethod]
        [DataRow(new[] { 3, 8 }, new[] { 0, 17 }, false)]
        [DataRow(new[] { 3, 8 }, new[] { 4, 9 }, true)]
        [DataRow(new[] { 2, 5 }, new[] { 4, 7 }, true)]
        [DataRow(new[] { 6, 15 }, new[] { 4, 7 }, false)]
        [DataRow(new[] { 4, 7 }, new[] { -3, 18 }, false)]
        [DataRow(new[] { 2, 10 }, new[] { 14, 17 }, false)]
        public void Should_Check_OverlapsLow(int[] a, int[] b, bool expected)
        {
            IPRange ip_a = new IPRange(a[0], a[1]);
            IPRange ip_b = new IPRange(b[0], b[1]);

            var result = ip_a.OverlapsLow(ip_b);
            Assert.AreEqual(expected, result);
        }

        [DataTestMethod]
        [DataRow(new[] { 3, 8 }, new[] { 0, 17 }, false)]
        [DataRow(new[] { 3, 8 }, new[] { 4, 9 }, false)]
        [DataRow(new[] { 2, 5 }, new[] { 4, 7 }, false)]
        [DataRow(new[] { 6, 15 }, new[] { 4, 7 }, true)]
        [DataRow(new[] { 4, 7 }, new[] { -3, 18 }, false)]
        [DataRow(new[] { 2, 10 }, new[] { 14, 17 }, false)]
        public void Should_Check_OverlapsHigh(int[] a, int[] b, bool expected)
        {
            IPRange ip_a = new IPRange(a[0], a[1]);
            IPRange ip_b = new IPRange(b[0], b[1]);

            var result = ip_a.OverlapsHigh(ip_b);
            Assert.AreEqual(expected, result);
        }

        [DataTestMethod]
        [DataRow(new[] { 3, 8 }, new[] { 0, 17 }, false)]
        [DataRow(new[] { 3, 8 }, new[] { 4, 9 }, true)]
        [DataRow(new[] { 2, 5 }, new[] { 4, 7 }, true)]
        [DataRow(new[] { 6, 15 }, new[] { 4, 7 }, true)]
        [DataRow(new[] { 4, 7 }, new[] { -3, 18 }, false)]
        [DataRow(new[] { 2, 10 }, new[] { 14, 17 }, false)]
        public void Should_Check_Overlaps(int[] a, int[] b, bool expected)
        {
            IPRange ip_a = new IPRange(a[0], a[1]);
            IPRange ip_b = new IPRange(b[0], b[1]);

            var result = ip_a.Overlaps(ip_b);
            Assert.AreEqual(expected, result);
        }

        [DataTestMethod]
        [DataRow(new[] { 3, 8 }, new[] { 0, 17 }, true)]
        [DataRow(new[] { 3, 8 }, new[] { 4, 9 }, true)]
        [DataRow(new[] { 2, 5 }, new[] { 4, 7 }, true)]
        [DataRow(new[] { 6, 15 }, new[] { 4, 7 }, true)]
        [DataRow(new[] { 4, 7 }, new[] { -3, 18 }, true)]
        [DataRow(new[] { 2, 10 }, new[] { 14, 17 }, false)]
        public void Should_Check_Touches(int[] a, int[] b, bool expected)
        {
            IPRange ip_a = new IPRange(a[0], a[1]);
            IPRange ip_b = new IPRange(b[0], b[1]);

            var result = ip_a.Touches(ip_b);
            Assert.AreEqual(expected, result);
        }

    }
}