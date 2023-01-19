using AoC16.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AoC16.Day22
{
    class Node
    {
        public Coord2D position;
        public int size;
        public int used;
        public int available;

        public Node(Coord2D position, int size, int used, int available)
        {
            this.position = position;
            this.size = size;
            this.used = used;
            this.available = available;
        }
    }

    internal class ComputerGrid
    {
        List<Node> nodes = new();

        Node ParseLine(string line)
        {
            var str = line.Replace("/dev/grid/node-", "").Replace("T", "");
            var groups = str.Split(" ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

            var group_coords = groups[0].Split("-");
            Coord2D position = new(int.Parse(group_coords[0].Substring(1)), int.Parse(group_coords[1].Substring(1)));
            int size = int.Parse(groups[1]);
            int used = int.Parse(groups[2]);
            int avail = int.Parse(groups[3]);

            return new Node(position, size, used, avail);
        }

        public void ParseInput(List<string> lines)
            => lines.Skip(2).ToList().ForEach(line => nodes.Add(ParseLine(line)));

        int FindViablePairs()
        {
            HashSet<Coord2D> processedNodes = new();
            int viablePairs = 0;
            foreach (var node in nodes)
            {
                if (node.used == 0)
                    continue;
                foreach (var comp in nodes.Where(x => x != node))
                {
                    if (processedNodes.Contains(comp.position))
                        continue;
                    
                    if (node.used <= comp.available)
                        viablePairs++;
                }
                processedNodes.Add(node.position);
            }

            return viablePairs;
        }

        public int Solve(int part = 1)
            => (part == 1) ? FindViablePairs() : 0;
    }
}
