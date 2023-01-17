using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC16.Day16
{
    public class RandomDataGen
    {
        string initialState = "";
        int diskLength  = 0;

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

        string FindCheckSum(int part)
        {
            if (part == 2)
                diskLength = 35651584;

            var dragon = initialState;
            while (dragon.Length < diskLength)
                dragon = DragonCurve(dragon);
            dragon = dragon.Substring(0, diskLength);
            return Checksum(dragon);
        }

        public void ParseInput(List<string> lines)
        {
            var elements = lines[0].Split(",", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            initialState = elements[0];
            diskLength = int.Parse(elements[1]);
        }

        public string Solve(int part = 1)
            => FindCheckSum(part);
    }
}
