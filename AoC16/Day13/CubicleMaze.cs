using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using AoC16.Common;

namespace AoC16.Day13
{
    public class Cubicle
    {
        public Coord2D position;
        public int Number = 0;
        public int Steps = 0;

        public Cubicle(int x, int y, int number, int steps =0)
        {
            position = new Coord2D(x, y);
            Number = number;
            Steps = steps;
        }

        public Cubicle(Coord2D pos, int number, int steps =0)
        {
            position = pos;
            Number = number;
            Steps = steps;
        }

        public bool IsOpen()
        {
            (int x, int y) = position;
            var calc = x * x + 3 * x + 2 * x * y + y + y * y;
            calc += Number;
            var str = Convert.ToString(calc, 2);
            var num_ones = str.Count(c => c == '1');
            return num_ones % 2 ==0;
        }

        public List<Cubicle> GetNeighbors()
        {
            var neigh = position.GetNeighbors();
            List<Cubicle> retVal = new();
            foreach (var n in neigh)
            {
                var cub = new Cubicle(n, Number, Steps + 1);
                if(cub.IsOpen())
                    retVal.Add(cub);
            }
            return retVal;
        }

        public int GetHash()
            => position.GetHashCode();
    }

    internal class CubicleMaze
    {
        Cubicle startingCubicle = new(1, 1, 0);
        int inputNumber = 0;

        public void ParseInput(List<string> lines)
            => inputNumber = int.Parse(lines[0]);

        public int Simulate(int part = 1)
        {
            startingCubicle.Number = inputNumber;
            Coord2D finalPosition = new Coord2D(31,39);

            HashSet<int> KnownStates = new();
            Queue<Cubicle> active_Cubicles = new();

            active_Cubicles.Enqueue(startingCubicle);

            while (active_Cubicles.Count > 0)
            {
                var currentCubicle = active_Cubicles.Dequeue();
                
                if (!KnownStates.Add(currentCubicle.GetHash()))
                    continue;

                if (currentCubicle.position == finalPosition)
                    return currentCubicle.Steps;

                var nextCubs = currentCubicle.GetNeighbors();
                foreach (var c in nextCubs)
                    active_Cubicles.Enqueue(c);
            }
            return -1;
        }

        public int Solve(int part = 1)
            => Simulate(part);
    }
}
