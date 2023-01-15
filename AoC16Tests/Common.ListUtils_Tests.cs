using AoC16.Common;

namespace AoC16Tests.Common
{
    [TestClass]
    public class Common_ListUtils
    {
        private static IEnumerable<object[]> GetIntTestData()
        {
            #region test data setup
            var sourceList_int = new List<int>() { 1, 2, 3, 4, 5 };
            var expected_k1_int = new List<List<int>>();
            expected_k1_int.Add(new List<int>() { 1 });
            expected_k1_int.Add(new List<int>() { 2 });
            expected_k1_int.Add(new List<int>() { 3 });
            expected_k1_int.Add(new List<int>() { 4 });
            expected_k1_int.Add(new List<int>() { 5 });

            var expected_k2_int = new List<List<int>>();
            expected_k2_int.Add(new List<int>() { 1, 2 });
            expected_k2_int.Add(new List<int>() { 1, 3 });
            expected_k2_int.Add(new List<int>() { 1, 4 });
            expected_k2_int.Add(new List<int>() { 1, 5 });
            expected_k2_int.Add(new List<int>() { 2, 3 });
            expected_k2_int.Add(new List<int>() { 2, 4 });
            expected_k2_int.Add(new List<int>() { 2, 5 });
            expected_k2_int.Add(new List<int>() { 3, 4 });
            expected_k2_int.Add(new List<int>() { 3, 5 });
            expected_k2_int.Add(new List<int>() { 4, 5 });

            var expected_k3_int = new List<List<int>>();
            expected_k3_int.Add(new List<int>() { 1, 2, 3 });
            expected_k3_int.Add(new List<int>() { 1, 2, 4 });
            expected_k3_int.Add(new List<int>() { 1, 2, 5 });
            expected_k3_int.Add(new List<int>() { 1, 3, 4 });
            expected_k3_int.Add(new List<int>() { 1, 3, 5 });
            expected_k3_int.Add(new List<int>() { 1, 4, 5 });
            expected_k3_int.Add(new List<int>() { 2, 3, 4 });
            expected_k3_int.Add(new List<int>() { 2, 3, 5 });
            expected_k3_int.Add(new List<int>() { 2, 4, 5 });
            expected_k3_int.Add(new List<int>() { 3, 4, 5 });

            var expected_k4_int = new List<List<int>>();
            expected_k4_int.Add(new List<int>() { 1, 2, 3, 4 });
            expected_k4_int.Add(new List<int>() { 1, 2, 3, 5 });
            expected_k4_int.Add(new List<int>() { 1, 2, 4, 5 });
            expected_k4_int.Add(new List<int>() { 1, 3, 4, 5 });
            expected_k4_int.Add(new List<int>() { 2, 3, 4, 5 });
            #endregion

            yield return new object[] { sourceList_int, 1, expected_k1_int };
            yield return new object[] { sourceList_int, 2, expected_k2_int };
            yield return new object[] { sourceList_int, 3, expected_k3_int };
            yield return new object[] { sourceList_int, 4, expected_k4_int };
        }

        private static IEnumerable<object[]> GetCharTestData()
        {
            #region test data setup
            var sourceList_char = new List<char>() { 'a', 'b', 'c', 'd', 'e' };
            var expected_k1_char = new List<List<char>>();
            expected_k1_char.Add(new List<char>() { 'a' });
            expected_k1_char.Add(new List<char>() { 'b' });
            expected_k1_char.Add(new List<char>() { 'c' });
            expected_k1_char.Add(new List<char>() { 'd' });
            expected_k1_char.Add(new List<char>() { 'e' });

            var expected_k2_char = new List<List<char>>();
            expected_k2_char.Add(new List<char>() { 'a', 'b' });
            expected_k2_char.Add(new List<char>() { 'a', 'c' });
            expected_k2_char.Add(new List<char>() { 'a', 'd' });
            expected_k2_char.Add(new List<char>() { 'a', 'e' });
            expected_k2_char.Add(new List<char>() { 'b', 'c' });
            expected_k2_char.Add(new List<char>() { 'b', 'd' });
            expected_k2_char.Add(new List<char>() { 'b', 'e' });
            expected_k2_char.Add(new List<char>() { 'c', 'd' });
            expected_k2_char.Add(new List<char>() { 'c', 'e' });
            expected_k2_char.Add(new List<char>() { 'd', 'e' });
            #endregion

            yield return new object[] { sourceList_char, 1, expected_k1_char };
            yield return new object[] { sourceList_char, 2, expected_k2_char };
        }

        [DataTestMethod]
        [DynamicData(nameof(GetIntTestData), DynamicDataSourceType.Method)]
        public void Should_Combine_int_Lists(List<int> input, int k, List<List<int>> expected)
        {
            var result = ListUtils.GetCombinations<int>(input, k);
            bool test = true && (result.Count() == expected.Count());
            foreach (var element in result)
            {
                var found = false;
                foreach (var elementComp in expected)
                    if (elementComp.SequenceEqual(element))
                    { found = true; break; }
                test &= found;
            }



            Assert.IsTrue(test);
        }

        [DataTestMethod]
        [DynamicData(nameof(GetCharTestData), DynamicDataSourceType.Method)]
        public void Should_Combine_char_Lists(List<char> input, int k, List<List<char>> expected)
        {
            var result = ListUtils.GetCombinations<char>(input, k).ToList();
            bool test = true && (result.Count() == expected.Count());
            foreach (var element in result)
            {
                var found = false;
                foreach (var elementComp in expected)
                    if(elementComp.SequenceEqual(element)) 
                        found = true;
                test &= found;
            }

            Assert.IsTrue(test);
        }

        [TestCleanup]
        public void Clean()
        {
        }

    }
}