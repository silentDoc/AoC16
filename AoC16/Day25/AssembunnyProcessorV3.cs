using AoC16.Day23;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC16.Day25
{
    class Instruction_v3
    {
        public string arg1 = "";
        public string arg2 = "";
        public string cmd = "";

        int index = 0;

        public Instruction_v3(string line, int index)
        {
            this.index = index;
            var groups = line.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            cmd = groups[0];
            arg1 = groups[1];
            if (groups.Count() == 3)
                arg2 = groups[2];
        }

        int getVal(string regOrVal, Dictionary<string, int> registers)
            => registers.ContainsKey(regOrVal) ? registers[regOrVal]
                                               : int.Parse(regOrVal);

        void setVal(string regOrVal, int value, Dictionary<string, int> registers)
        {
            int useless = 0;
            if (!int.TryParse(regOrVal, out useless))
                registers[regOrVal] = value;
        }

        public int Run(Dictionary<string, int> registers, List<Instruction_v3> program, StringBuilder outString)
        {
            int offset = 1;
            switch (cmd)
            {
                case "cpy":
                    setVal(arg2, getVal(arg1, registers), registers);
                    break;
                case "inc":
                    setVal(arg1, getVal(arg1, registers) + 1, registers);
                    break;
                case "dec":
                    setVal(arg1, getVal(arg1, registers) - 1, registers);
                    break;
                case "jnz":
                    offset = getVal(arg1, registers) != 0 ? getVal(arg2, registers) : 1;
                    break;
                case "mul": // No muls in day 25, but may come useful
                    setVal(arg2, getVal(arg1, registers) * getVal(arg2, registers), registers);
                    break;
                case "out":
                    var output = getVal(arg1, registers);
                    outString.Append(output.ToString());
                    if (outString.Length >= 20)
                        offset = 1000;  // ends execution
                    break;
                default:
                    throw new InvalidOperationException("Unknown command - " + cmd);
            }
            return index + offset;
        }
    }

    internal class AssembunnyProcessorV3
    {
        Dictionary<string, int> registers = new();
        List<Instruction_v3> program = new();

        public void ParseInput(List<string> lines)
        {
            for (int row = 0; row < lines.Count; row++)
                program.Add(new Instruction_v3(lines[row], row));
        }

        int RunProgram(int part = 1)
        {
            // Brute force attack
            for (int i = 0; i < 10000; i++)
            {
                StringBuilder sb = new();
                registers["a"] = i;
                registers["b"] = 0;
                registers["c"] = 0;
                registers["d"] = 0;

                int currentIndex = 0;

                while (currentIndex < program.Count)
                    currentIndex = program[currentIndex].Run(registers, program, sb);

                var check = sb.ToString();
                if (check.StartsWith("10101010101010101010") || check.StartsWith("01010101010101010101"))
                    return i;
             
            }
            return registers["a"];
        }

        public int Solve(int part = 1)
            => RunProgram(part);
    }
}
