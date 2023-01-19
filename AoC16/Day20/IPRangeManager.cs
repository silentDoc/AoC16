using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC16.Day20
{
    public class IPRange
    {
        public int start;
        public int end;

        public IPRange(int start, int end)
        {
            this.start = start;
            this.end = end;
        }

        public bool FullyContains(IPRange other)
            => throw new NotImplementedException();
        public bool IsFullyContainedBy(IPRange other)
            => throw new NotImplementedException();

        public bool Overlaps(IPRange other)
            => throw new NotImplementedException();

        public bool OverlapsLow(IPRange other)
            => throw new NotImplementedException();
        public bool OverlapsHigh(IPRange other)
            => throw new NotImplementedException();

        public bool Touches(IPRange other)
            => throw new NotImplementedException();

        public bool TryMergeIpRange(IPRange other, out IPRange resultingRange)
            => throw new NotImplementedException();
    }

    public class IPRangeManager
    {
        List<IPRange> allRanges = new();

        IPRange ParseLine(string line)
        {
            var g = line.Split("-", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            return new IPRange(int.Parse(g[0]), int.Parse(g[1]));
        }

        public void ParseInput(List<string> lines)
            => lines.ForEach(line => allRanges.Add(ParseLine(line)));

        public int FindMinWhiteListed()
        {
           
            return 0;
        }

        public int Solve(int part = 1)
            => (part == 1) ? FindMinWhiteListed() : 0;
    }
}
