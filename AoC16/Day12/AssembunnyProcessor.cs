using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC16.Day12
{
    // Borrowing my own approach to AOC15 - Day 23 :)
    class Instruction
    {
        string command = "";
        string reg = "";
        string reg_target = "";
        int offset;
        int index;

        public (int newIndex, int newValue) Run(Dictionary<string, int> registers)
            => command switch
            {
                "cpy" => (index + 1, registers[reg_target] = offset),
                "cpy_r" => (index + 1, registers[reg_target] = registers[reg]),
                "inc" => (index + 1, registers[reg] = registers[reg] + 1),
                "dec" => (index + 1, registers[reg] = registers[reg] - 1),
                "jnz" => ((registers[reg] !=0) ? index + offset : index + 1, 0),
                "jnz_v" => (int.Parse(reg) != 0 ? index + offset : index + 1, 0),
                _ => throw new Exception("Invalid command")
            };

        public Instruction(string line, int index)
        {
            this.index = index;
            var groups = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            command = groups[0].Trim();

            if (command == "cpy")
            {
                if (!int.TryParse(groups[1], out offset))
                    command = "cpy_r";
                reg_target = groups[2];
            }

            reg = groups[1];
            if (command == "jnz")
            {
                int value = 0;
                if (int.TryParse(groups[1], out value))
                    command = "jnz_v";
                offset = int.Parse(groups[2]);
            }
        }
    }

    internal class AssembunnyProcessor
    {
        Dictionary<string, int> registers = new();
        List<Instruction> program = new();

        public void ParseInput(List<string> lines)
        {
            for (int row = 0; row < lines.Count; row++)
                program.Add(new Instruction(lines[row], row));
        }

        int RunProgram(int part = 1)
        {
            registers["a"] = 0;
            registers["b"] = 0;
            registers["c"] = 0;
            registers["d"] = 0;
            int currentIndex = 0;
            int value = 0;

            while (currentIndex < program.Count)
                (currentIndex, value) = program[currentIndex].Run(registers);

            return registers["a"];
        }

        public int Solve(int part = 1)
            => RunProgram(part);
    }
}
