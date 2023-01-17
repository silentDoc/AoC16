using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC16.Day16
{
    public class RandomDataGen
    {
        public string DragonCurve(string input)
        {
            StringBuilder sb = new StringBuilder(input);
            StringBuilder reversed = new();

            var b = input.Reverse();
            b = b.Select(x => (x == '0') ? '1' : '0');
            foreach (char c in b)
                reversed.Append(c);
            sb.Append('0');
            sb.Append(reversed.ToString());
            return sb.ToString();
        }


        public string Checksum(string input)
        {
            StringBuilder checksum = new();
            var groups = input.Chunk(2).Select(x => new string(x)).ToList();
            foreach (var g in groups)
                checksum.Append(g[0] == g[1] ? '1' : '0');

            var check = checksum.ToString();

            return check.Length % 2 == 0 ? Checksum(check) : check;
        }
    }
}
