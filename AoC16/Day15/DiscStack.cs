using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AoC16.Day15
{
    class Disc
    {
        public int id = 0;
        public int num_positions = 0;
        public int startingPosition = 0;

        public Disc(int id, int positions, int startingPosition)
        {
            this.id = id;
            this.num_positions = positions;
            this.startingPosition = startingPosition;
        }

        public int GetPosition(int time)
            => (startingPosition + time) % num_positions;
    }

    internal class DiscStack
    {
        List<Disc> discStack = new List<Disc>();

        Disc ParseLine(string line)
        {
            Regex inputRegex = new Regex(@"Disc #(\d) has (\d+) positions; at time=(\d), it is at position (\d+)");
            var groups = inputRegex.Match(line).Groups;
            return new Disc(int.Parse(groups[1].Value), int.Parse(groups[2].Value), int.Parse(groups[4].Value));
        }

        public void ParseInput(List<string> lines)
            => lines.ForEach(line => discStack.Add(ParseLine(line)));

        int FindTime(int part = 1)
        {
            List<int> targetPositions = new();
            var diff = -1;
            foreach(var disc in discStack) 
            {
                targetPositions.Add(module(diff, disc.num_positions));
                diff--;
            }
            
            bool found = false;
            int time = 0;
            while (!found)
            { 
                time++;
                var currentPositions = discStack.Select(x => x.GetPosition(time)).ToList();
                found = currentPositions.SequenceEqual(targetPositions);
            }

            return time;
        }

        int module(int number, int mod)
            => (number > 0) ? number % mod : number + mod;

        public int Solve(int part = 1)
            => FindTime(part);
    }
}
