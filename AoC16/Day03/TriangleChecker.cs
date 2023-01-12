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

        public HqTriangle(int a, int b, int c)
        {
            this.a = a;
            this.b = b;
            this.c = c;
        }

        public bool Possible
            => (a + b) > c && (a + c) > b && (b + c) > a;
    }

    

    internal class TriangleChecker
    {
        List<HqTriangle> triangles= new();

        public void ParseByColumns(List<string> lines)
        {
            List<int> column1 = new();
            List<int> column2 = new();
            List<int> column3 = new();
            foreach (var line in lines)
            {
                var groups = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                column1.Add(int.Parse(groups[0].Trim()));
                column2.Add(int.Parse(groups[1].Trim()));
                column3.Add(int.Parse(groups[2].Trim()));

                if (column1.Count == 3)
                {
                    triangles.Add(new HqTriangle(column1[0], column1[1], column1[2]));
                    triangles.Add(new HqTriangle(column2[0], column2[1], column2[2]));
                    triangles.Add(new HqTriangle(column3[0], column3[1], column3[2]));
                    column1.Clear();
                    column2.Clear();
                    column3.Clear();
                }
            }
        }

        public void ParseInput(List<string> lines, int part = 1)
        {
            if (part == 1)
                lines.ForEach(line => triangles.Add(new HqTriangle(line)));
            else
                ParseByColumns(lines);
        }
    
        public int Solve()
            => triangles.Where(x => x.Possible).Count();
    }
}
