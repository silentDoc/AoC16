using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC16.Day03
{
    class HqTriangle
    {
        public int a;
        public int b;
        public int c;

        public HqTriangle(string inputLine)
        { 
            var groups = inputLine.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            a = int.Parse(groups[0].Trim());
            b = int.Parse(groups[1].Trim());
            c = int.Parse(groups[2].Trim());
        }

        public bool Possible
            => (a + b) > c && (a + c) > b && (b + c) > a;
    }

    internal class TriangleChecker
    {
        List<HqTriangle> triangles= new();
        public void ParseInput(List<string> lines)
            => lines.ForEach( line => triangles.Add(new HqTriangle(line)) );

        public int Solve(int part = 1)
            => triangles.Where(x => x.Possible).Count();
            
    }
}
