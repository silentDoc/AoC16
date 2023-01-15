using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC16.Common
{
    public static class ListUtils
    {
        private static IEnumerable<IEnumerable<T>> FindCombinations<T>(this IEnumerable<T> elements, int k)
        {
            return k == 0 ? new[] { new T[0] } :
              elements.SelectMany((e, i) =>
                elements.Skip(i + 1).FindCombinations(k - 1).Select(c => (new[] { e }).Concat(c)));
        }

        public static List<List<T>> GetCombinations<T>(List<T> elements, int k)
        {
            var result = FindCombinations<T>(elements, k);
            var retVal = new List<List<T>>();
            foreach (var combination in result)
                retVal.Add(combination.ToList());
            return retVal;
        }
    }
}
