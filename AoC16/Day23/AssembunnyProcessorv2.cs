﻿using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AoC16.Day23
{
    class Instruction_v2
    {
        public string arg1 = "";
        public string arg2 = "";
        public string cmd  = "";

        int index = 0;

        public Instruction_v2(string line, int index)
        {
            this.index = index;
            var groups = line.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            cmd = groups[0];
            arg1 = groups[1];
            if(groups.Count()==3)
                arg2 = groups[2];
        }

        int getVal(string regOrVal, Dictionary<string, int> registers)
            => int.TryParse(regOrVal, out var value) ? value
                : registers.ContainsKey(regOrVal) ? registers[regOrVal]
                : 0;

        void setVal(string regOrVal, int value, Dictionary<string, int> registers)
        {
            int useless = 0;
            if (!int.TryParse(regOrVal, out useless))
                 registers[regOrVal] = value;
        }

        void Toggle()
        {
            cmd = cmd switch
            {
                "cpy" => "jnz",
                "jnz" => "cpy",
                "inc" => "dec",
                "dec" => "inc",
                "tgl" => "inc",
                _ => cmd
            };
        }

        public int Run(Dictionary<string, int> registers, List<Instruction_v2> program)
        {
            int offset = 1;
            switch (cmd)
            {
                case "cpy":
                    setVal(arg2, getVal(arg1, registers), registers);
                    break;
                case "inc":
                    setVal(arg1, getVal(arg1, registers) +1, registers);
                    break;
                case "dec":
                    setVal(arg1, getVal(arg1, registers) -1, registers);
                    break;
                case "jnz":
                    offset = getVal(arg1, registers) != 0 ? getVal(arg2, registers) : 1;
                    break;
                case "tgl":
                    var newIndex = index + getVal(arg1, registers);
                    if (newIndex < 0 || newIndex >= program.Count)
                        break;
                    program[newIndex].Toggle();
                    break;
                default:
                    throw new InvalidOperationException("Unknown command - " + cmd);
            }
            return index + offset;
        }
    }

    internal class AssembunnyProcessorV2
    {
        Dictionary<string, int> registers = new();
        List<Instruction_v2> program = new();

        public void ParseInput(List<string> lines)
        {
            for (int row = 0; row < lines.Count; row++)
                program.Add(new Instruction_v2(lines[row], row));
        }

        int RunProgram(int part = 1)
        {
            registers["a"] = (part == 1) ? 7 : 1;
            registers["b"] = 0;
            registers["c"] = 0;
            registers["d"] = 0;
            int currentIndex = 0;

            while (currentIndex < program.Count)
                currentIndex = program[currentIndex].Run(registers, program);

            return registers["a"];
        }

        public int Solve(int part = 1)
            => RunProgram(part);
    }
}
