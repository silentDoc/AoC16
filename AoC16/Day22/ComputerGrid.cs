using AoC16.Common;
using AoC16.Day13;
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
        Dictionary<Coord2D, char> gridMap = new();

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

        // Display the grid and solve the problem
        int prettyPrint()
        {
            (int x1, int x2) = (nodes.Min(x => x.position.x), nodes.Max(x => x.position.x));
            (int y1, int y2) = (nodes.Min(x => x.position.y), nodes.Max(x => x.position.y));

            Console.SetCursorPosition(0, 0);

            for (int j = y1; j <= y2; j++)
            {
                for (int i = x1; i <= x2; i++)
                {
                    var currentPos = new Coord2D(i, j);
                    var node = nodes.Where(n => n.position == currentPos).First();
                    string symbol = node.used > 100 ? "# " : node.used == 0 ? "_ " : ". ";
                    gridMap[currentPos] = symbol[0];
                    Console.Write(symbol);
                }
                Console.WriteLine("");
            }
            
            var emptyPos = gridMap.Keys.First(c => gridMap[c] == '_');
            var movesToPositionEmptyNodeBeside =  Simulate(emptyPos, new Coord2D(x2 - 1, 0));
            var movesToMoveTheGoalData = 5 * Simulate(new Coord2D(x1, 0), new Coord2D(x2-1, 0)) +1; // 5 moves = 1 for moving data + 4 for moving empty for each step, plus only one for the last one

            return movesToPositionEmptyNodeBeside + movesToMoveTheGoalData;
        }

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

        // Good old BFS that takes into account walls calculated in "PrettyPrint" method
        public int Simulate(Coord2D start, Coord2D target)
        {
            HashSet<Coord2D> visited_nodes = new();
            Queue<(Coord2D pos, int cost)> active_nodes = new();

            (int x1, int x2) = (nodes.Min(x => x.position.x), nodes.Max(x => x.position.x));
            (int y1, int y2) = (nodes.Min(x => x.position.y), nodes.Max(x => x.position.y));

            active_nodes.Enqueue((start,0));

            while (active_nodes.Count > 0)
            {
                var currentNode = active_nodes.Dequeue();

                if (!visited_nodes.Add(currentNode.pos))
                    continue;

                if (currentNode.pos == target)
                    return currentNode.cost;

                var neigh = currentNode.pos.GetNeighbors().Where(c => c.x >= x1 && c.x <= x2 && c.y >= y1 && c.y <= y2).ToList();

                foreach (var c in neigh)
                    if (gridMap[c]!='#')
                        active_nodes.Enqueue((c, currentNode.cost+1));
            }
            return 0;
        }


        public int Solve(int part = 1)
            => (part == 1) ? FindViablePairs() : prettyPrint();
    }
}
