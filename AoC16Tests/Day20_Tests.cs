using AoC16.Day20;
using System.Security.Cryptography;

namespace AoC16Tests
{
    [TestClass]
    public class Day20_Tests_Part1
    {
        [DataTestMethod]
        [DataRow( 3, 8,  4, 7 , true)]
        [DataRow( 3, 8 ,  4, 9 , false)]
        [DataRow( 2, 5 ,  4, 7 , false)]
        [DataRow( -3, 18 , 4, 7 , true)]
        [DataRow( 2, 10 ,  14, 17 , false)]
        public void Should_Check_FullyContains(long start_a, long end_a, long start_b, long end_b, bool expected)
        {
            IPRange ip_a = new IPRange(start_a, end_a);
            IPRange ip_b = new IPRange(start_b, end_b);

            var result = ip_a.FullyContains(ip_b);
            Assert.AreEqual(expected, result);
        }

        [DataTestMethod]
        [DataRow(3, 8 ,  0, 17 , true)]
        [DataRow(1, 8 ,  4, 9 , false)]
        [DataRow( 2, 5 , 4, 7 , false)]
        [DataRow(4, 7, -3, 18 , true)]
        [DataRow( 2, 10 ,14, 17 , false)]
        public void Should_Check_IsFullyContained(long start_a, long end_a, long start_b, long end_b, bool expected)
        {
            IPRange ip_a = new IPRange(start_a, end_a);
            IPRange ip_b = new IPRange(start_b, end_b);

            var result = ip_a.IsFullyContainedBy(ip_b);
            Assert.AreEqual(expected, result);
        }

        [DataTestMethod]
        [DataRow(3, 8 ,  0, 17 , false)]
        [DataRow( 3, 8 ,  4, 9 , true)]
        [DataRow(2, 5 ,  4, 7 , true)]
        [DataRow( 6, 15 ,  4, 7 , false)]
        [DataRow( 4, 7 ,  -3, 18 , false)]
        [DataRow( 2, 10 ,  14, 17 , false)]
        public void Should_Check_OverlapsLow(long start_a, long end_a, long start_b, long end_b, bool expected)
        {
            IPRange ip_a = new IPRange(start_a, end_a);
            IPRange ip_b = new IPRange(start_b, end_b);

            var result = ip_a.OverlapsLow(ip_b);
            Assert.AreEqual(expected, result);
        }

        [DataTestMethod]
        [DataRow( 3, 8 ,  0, 17 , false)]
        [DataRow( 3, 8 , 4, 9 , false)]
        [DataRow( 2, 5 ,  4, 7 , false)]
        [DataRow( 6, 15 ,  4, 7 , true)]
        [DataRow( 4, 7 ,  -3, 18 , false)]
        [DataRow( 2, 10 , 14, 17 , false)]
        public void Should_Check_OverlapsHigh(long start_a, long end_a, long start_b, long end_b, bool expected)
        {
            IPRange ip_a = new IPRange(start_a, end_a);
            IPRange ip_b = new IPRange(start_b, end_b);

            var result = ip_a.OverlapsHigh(ip_b);
            Assert.AreEqual(expected, result);
        }

        [DataTestMethod]
        [DataRow(3, 8, 0, 17, false)]
        [DataRow(3, 8, 4, 9, true)]
        [DataRow(2, 5, 4, 7, true)]
        [DataRow(6, 15, 4, 7, true)]
        [DataRow(4, 7, -3, 18, false)]
        [DataRow(2, 10, 14, 17, false)]
        public void Should_Check_Overlaps(long start_a, long end_a, long start_b, long end_b, bool expected)
        {
            IPRange ip_a = new IPRange(start_a, end_a);
            IPRange ip_b = new IPRange(start_b, end_b);

            var result = ip_a.Overlaps(ip_b);
            Assert.AreEqual(expected, result);
        }

        [DataTestMethod]
        [DataRow(3, 8, 0, 17, true)]
        [DataRow(3, 8, 4, 9, true)]
        [DataRow(2, 5, 4, 7, true)]
        [DataRow(6, 15, 4, 7, true)]
        [DataRow(4, 7, -3, 18, true)]
        [DataRow(2, 10, 14, 17, false)]
        public void Should_Check_Touches(long start_a, long end_a, long start_b, long end_b, bool expected)
        {
            IPRange ip_a = new IPRange(start_a, end_a);
            IPRange ip_b = new IPRange(start_b, end_b);

            var result = ip_a.Touches(ip_b);
            Assert.AreEqual(expected, result);
        }

    }
}