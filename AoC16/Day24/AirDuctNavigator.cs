using AoC16.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AoC16.Day24
{
    class Route
    {
        public List<int> sequence = new();
        public int totalCost = 9999999;
    }

    internal class AirDuctNavigator
    {
        Dictionary<Coord2D, char> map = new Dictionary<Coord2D, char>();
        List<Route> routes = new List<Route>();

        void ParseLine(string line, int y)
        {
            for(int i = 0; i < line.Length;i++) 
                map[new Coord2D(i, y)] = line[i];
        }

        public void ParseInput(List<string> lines)
        {
            int y = 0;
            foreach (string line in lines) 
            {
                ParseLine(line, y);
                y++;
            }
        }

        public int BFS(Coord2D start, Coord2D destination)
        {
            HashSet<Coord2D> visited_positions = new();
            Queue<(Coord2D pos, int cost)> active_positions = new();

            active_positions.Enqueue((start, 0));

            while (active_positions.Count > 0)
            {
                var currentNode = active_positions.Dequeue();

                if (!visited_positions.Add(currentNode.pos))
                    continue;

                if (currentNode.pos == destination)
                    return currentNode.cost;

                var neighs = currentNode.pos.GetNeighbors().Where(n => map.ContainsKey(n)).ToList();

                foreach (var n in neighs)
                    if (map[n] != '#')
                        active_positions.Enqueue((n, currentNode.cost + 1));
            }
            return 0;
        }

        void FindCombinations(List<int> visited, List<int> available, Dictionary<(int, int), int> costs ,int currentCost)
        {
            int currentNode = visited.Last();
            if (available.Count == 0)
            {
                Route result = new Route();
                result.totalCost = currentCost;
                result.sequence = visited.ToList();
                routes.Add(result);
                return;
            }

            foreach (var nextNode in available)
            {
                var newAvailable = available.ToList();
                var newVisited = visited.ToList();
                newAvailable.Remove(nextNode);
                newVisited.Add(nextNode);
                FindCombinations(newVisited, newAvailable, costs, currentCost + costs[(currentNode, nextNode)]);
            }
        }

        int FindRouteVisitingNodes()
        {
            int numNodes = map.Values.Count(x => x != '.' && x != '#');
            List<Coord2D> interestingPoints = new();

            for(int c = 0;c<numNodes;c++) 
            {
                char point = char.Parse(c.ToString());
                interestingPoints.Add(map.Keys.Where(p => map[p] == point).First());
            }

            // Calc Costs
            Dictionary<(int, int), int> costs = new();
            for (int i = 0; i < numNodes; i++)
                for (int j = 0; j < numNodes; j++)
                    costs[(i, j)] = 999999;

            for (int i = 0; i < numNodes; i++)
                for (int j = 0; j < numNodes; j++)
                {
                    if (i == j)
                    {
                        costs[(i, j)] = 0;
                        continue;
                    }
                    if (costs[(i, j)] != 999999)
                        continue;

                    var cost = BFS(interestingPoints[i], interestingPoints[j]);
                    costs[(i, j)] = cost;
                    costs[(j, i)] = cost;
                }

            // We have all the costs, now the problem becomes a travelling salesman optimisation
            List<int> visitedNodes = new();
            List<int> availableNodes = new();

            visitedNodes.Add(0);
            for (int i = 1; i < numNodes; i++)
                availableNodes.Add(i);

            FindCombinations(visitedNodes, availableNodes, costs, 0);

            return routes.Min(x => x.totalCost);
        }

        public int Solve(int part = 1)
            => (part == 1) ? FindRouteVisitingNodes() : 0;
    }
}
