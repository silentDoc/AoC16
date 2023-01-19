using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC16.Day20
{
    public class IPRange
    {
        public long start;
        public long end;

        public IPRange(long start, long end)
        {
            this.start = start;
            this.end = end;
        }

        public bool FullyContains(IPRange other)
            => start <= other.start && end >= other.end;
        public bool IsFullyContainedBy(IPRange other)
            => other.start <= start && other.end >= end;

        public bool Overlaps(IPRange other)
            => OverlapsLow(other) || OverlapsHigh(other);

        public bool OverlapsLow(IPRange other)
            => start < other.start && end < other.end && end >=other.start;
        public bool OverlapsHigh(IPRange other)
            => start > other.start && end > other.end && start <= other.end;

        public bool Touches(IPRange other)
            => FullyContains(other) || Overlaps(other) || IsFullyContainedBy(other);

        public bool TryMergeIpRange(IPRange other, out IPRange resultingRange)
        {
            resultingRange = new(0, 0);
            if (!Touches(other))
                return false;

            resultingRange.start = (FullyContains(other) || OverlapsLow(other)) ? start : other.start;
            resultingRange.end = (FullyContains(other) || OverlapsHigh(other)) ? end : other.end;
            return true;
        }
    }

    public class IPRangeManager
    {
        List<IPRange> allRanges = new();

        IPRange ParseLine(string line)
        {
            var g = line.Split("-", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            return new IPRange(long.Parse(g[0]), long.Parse(g[1]));
        }

        public void ParseInput(List<string> lines)
            => lines.ForEach(line => allRanges.Add(ParseLine(line)));

        public long FindMinWhiteListed()
        {
            List<IPRange> mergedRanges = allRanges.OrderBy(x => x.start).ToList();

            int current = 0;
            int count = allRanges.Count;

            for (current = 0; current < count; current++)
            {
                var rng = mergedRanges[current];
                var toMerge = mergedRanges.Where(x => x.Touches(rng) && x != rng).FirstOrDefault();

                if (toMerge != null)
                {
                    IPRange merged = new(0, 0);
                    rng.TryMergeIpRange(toMerge, out merged);
                    mergedRanges.Remove(rng);
                    mergedRanges.Remove(toMerge);
                    mergedRanges.Add(merged);
                    current = 0;
                    count = mergedRanges.Count;
                }
            }

            // Find the first range that leaves any address between him and the following
            mergedRanges = mergedRanges.OrderBy(x => x.start).ToList();
            int i = 0;
            for (i = 0; i < mergedRanges.Count-1; i++)
                if (mergedRanges[i].end < mergedRanges[i + 1].start - 1)
                    break;

            return mergedRanges[i].end + 1;
        }

        public long Solve(int part = 1)
            => (part == 1) ? FindMinWhiteListed() : 0;
    }
}
