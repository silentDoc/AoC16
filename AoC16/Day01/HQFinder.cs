using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace AoC16.Day01
{
    record struct Coord
    {
        public int lat;
        public int lon;
    }

    record struct Instruction
    {
        public char turn;
        public int count;
    }

    internal class HQFinder
    {
        List<Instruction> Document = new();
        int Orientation = 0;    // 0 - North, 1 - East , 2 - South , 3 - West

        void TurnRight()
            => Orientation = (Orientation + 1) % 4;

        void TurnLeft()
            => Orientation = (Orientation == 0) ? 3 : Orientation-1;

        int Manhattan(Coord coord)
            => Math.Abs(coord.lat) + Math.Abs(coord.lon);

        Coord Offset(int orientation, int count)
            => orientation switch
            {
                0 => new Coord { lat = count, lon = 0 },
                1 => new Coord { lat = 0, lon = count },
                2 => new Coord { lat = -1 * count, lon = 0 },
                3 => new Coord { lat = 0, lon = -1 * count },
                _ => throw new Exception("Invalid orientation")
            };

        public void ParseInput(List<string> lines)
        {
            Document.Clear();

            var line = lines[0];
            var steps = line.Split(",", StringSplitOptions.RemoveEmptyEntries);
            foreach (var step in steps)
            {
                var trimmed_step = step.Trim();
                var instruction = new Instruction() { turn = trimmed_step[0], 
                                                     count = int.Parse(trimmed_step.Substring(1)) };
                Document.Add(instruction);
            }
        }

        int Walk(int part = 1)
        {
            Coord currentCoord = new Coord() { lat = 0, lon = 0 };
            HashSet<Coord> visitedBlocks = new();
            
            foreach (var instruction in Document)
            {
                if (instruction.turn == 'R') TurnRight(); 
                else TurnLeft();
                
                var lastPosition = currentCoord;

                var movement = Offset(Orientation, instruction.count);
                currentCoord.lat += movement.lat;
                currentCoord.lon += movement.lon;

                if (part == 2)  // Keep all the blocks we go through when advancing
                {
                    IEnumerable<int> range = Orientation switch
                    {
                        0 => Enumerable.Range(lastPosition.lat + 1, instruction.count),
                        1 => Enumerable.Range(lastPosition.lon + 1, instruction.count),
                        2 => Enumerable.Range(currentCoord.lat - 1, instruction.count),
                        3 => Enumerable.Range(currentCoord.lon - 1, instruction.count),
                        _ => throw new Exception("Invalid orientation")
                    };

                    foreach (var i in range)
                    {
                        var block = (Orientation == 0 || Orientation == 2) ? new Coord() { lat = i, lon = currentCoord.lon }
                                                                           : new Coord() { lat = currentCoord.lat, lon = i };
                        if (!visitedBlocks.Add(block))
                                return Manhattan(block);
                    }
                }

                
            }
            return Manhattan(currentCoord);
        }

        public int Solve(int part = 1)
            => Walk(part);
    }
}
