using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC16.Day08
{
    record struct Instruction
    {
        public string operation;
        public int operandA;
        public int operandB;
    }

    class DisplayCell
    {
        public int x;
        public int y;
        public bool lit;
    }

    internal class KeypadScreen
    {
        List<Instruction> instructions = new();
        List<DisplayCell> display = new();

        private readonly int ROW_WIDTH = 50;
        private readonly int COL_HEIGHT = 6;

        Instruction ParseInstruction(string line)
        {
            if (line.StartsWith("rect "))
            {
                var groups = line.Replace("rect ", "").Split("x", StringSplitOptions.RemoveEmptyEntries);
                return new Instruction() { operation = "rect", operandA = int.Parse(groups[0]), operandB = int.Parse(groups[1]) };
            }
            if (line.StartsWith("rotate row y="))
            {
                var groups = line.Replace("rotate row y=", "").Split(" by ", StringSplitOptions.RemoveEmptyEntries);
                return new Instruction() { operation = "row", operandA = int.Parse(groups[0]), operandB = int.Parse(groups[1]) };
            }
            var groups1 = line.Replace("rotate column x=", "").Split(" by ", StringSplitOptions.RemoveEmptyEntries);
            return new Instruction() { operation = "column", operandA = int.Parse(groups1[0]), operandB = int.Parse(groups1[1]) };
        }

        public void ParseInput(List<string> lines)
            => lines.ForEach(line => instructions.Add(ParseInstruction(line)));

        public void InitPanel()
        {
            for (int yy = 0; yy < COL_HEIGHT; yy++)
                for (int xx = 0; xx < ROW_WIDTH; xx++)
                    display.Add(new DisplayCell() { x = xx, y = yy, lit = false });
        }

        void Rect(int width, int height)
        {
            for (int yy = 0; yy < height; yy++)
                for (int xx = 0; xx < width; xx++)
                {
                    var cell = display.FindIndex(c => c.x == xx && c.y == yy);
                    display[cell].lit = true;
                }
        }

        void Rotate_row(int row, int offset)
        {
            var rowToProcess = display.Where(c => c.y == row).Select(cc => cc.lit).ToList();
            for (int i = 0; i < ROW_WIDTH; i++)
            {
                var cell = display.FindIndex(c => c.x == (offset + i) % ROW_WIDTH && c.y == row);
                display[cell].lit = rowToProcess[i];
            }
        }

        void Rotate_column(int column, int offset)
        {
            var colToProcess = display.Where(c => c.x == column).Select(cc => cc.lit).ToList();
            for (int i = 0; i < COL_HEIGHT; i++)
            {
                var cell = display.FindIndex(c => c.y == (offset + i) % COL_HEIGHT && c.x == column);
                display[cell].lit = colToProcess[i];
            }
        }

        void PreviewDisplay()
        {
            Console.WriteLine("");
            for (int i = 0; i < COL_HEIGHT; i++)
            {
                var values = display.Where(c => c.y == i).Select(v => (v.lit) ? "#" : ".").ToList();
                Console.WriteLine(string.Join("", values));
            }
        }

        int FindLitCells(int part = 1)
        {
            InitPanel();
            foreach (var ins in instructions)
            {
                if (ins.operation == "rect") Rect(ins.operandA, ins.operandB);
                if (ins.operation == "row") Rotate_row(ins.operandA, ins.operandB);
                if (ins.operation == "column") Rotate_column(ins.operandA, ins.operandB);
            }

            if (part == 2)
                PreviewDisplay();

            return display.Count(x => x.lit);
        }

        public int Solve(int part = 1)
            => FindLitCells(part);

    }
}
