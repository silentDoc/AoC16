using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC16.Day21
{

    public enum Instruction
    { 
        SwapPos =0,
        SwapLetter =1,
        RotateLeft =2,
        RotateRight = 3,
        RotatePosLetter = 4,
        Move = 5, 
        Reverse = 6
    }

    public class ScrambleStep
    {
        public Instruction what;
        public int pos1 = -1;
        public int pos2 = -1;
        public char c1 = '.';
        public char c2 = '.';

        public ScrambleStep(Instruction what, int pos1, int pos2)
        {
            this.what = what;
            this.pos1 = pos1;
            this.pos2 = pos2;
        }

        public ScrambleStep(Instruction what, int pos1)
        {
            this.what = what;
            this.pos1 = pos1;
        }

        public ScrambleStep(Instruction what, char c1, char c2)
        {
            this.what = what;
            this.c1 = c1;
            this.c2 = c2;
        }

        public ScrambleStep(Instruction what, char c1)
        {
            this.what = what;
            this.c1 = c1;
        }

    }

    public class Scrambler
    {
        string startingString = string.Empty;
        List<ScrambleStep> steps = new();

        public string Swap(string input, int pos1, int pos2)
        {
            var retVal = input.ToCharArray();
            retVal[pos1] = input[pos2];
            retVal[pos2] = input[pos1];
            return new string(retVal);
        }

        public string Swap(string input, char a, char b)
            => Swap(input, input.IndexOf(a), input.IndexOf(b));


        public string Reverse(string input, int start, int end)
        {
            string strStart = (start > 0) ? input.Substring(0, start) : "";
            string strEnd = (end < input.Length - 1) ? input.Substring(end + 1) : "";

            var toReverse = input.Substring(start, end - start + 1).ToCharArray();
            Array.Reverse(toReverse);
            string strReverse = new string(toReverse);
            
            return strStart + strReverse + strEnd;
        }


        public string Move(string input, int source, int destination)
        {
            var ch = input[source];
            var str = input;
            str = str.Remove(source, 1);
            return str.Insert(destination, ch.ToString());
        }
            

        public string Rotate(string input, char target)
        { 
            int index = input.IndexOf(target);
            var str = RotateRight(input, 1);
            str = RotateRight(str, index);
            if(index>=4)
                str = RotateRight(str, 1);
            return str;
        }


        public string RotateLeft(string input, int count)
            => string.Concat(input.AsSpan(count % input.Length), input.AsSpan(0, count % input.Length));
        
        
        public string RotateRight(string input, int count)
            => string.Concat(input.Substring((input.Length-count) % input.Length), input.Substring(0, (input.Length - count) % input.Length));


        void ParseInstruction(string line)
        {
            // Preprocess a little bit
            var str = line.Replace("swap position", "swPos,").Replace("swap letter", "swLetter,").Replace("rotate left", "rLeft,").Replace("rotate right", "rRight,");
            str = str.Replace("rotate based on position of letter", "rLetter,").Replace("move position", "move,").Replace("reverse positions", "rev,");
            str = str.Replace("with position", ",").Replace("steps", ",").Replace("with letter ", ",").Replace("to position", ",").Replace("through ", ",");
            str = str.Replace("step", "").Replace("steps", "");

            var groups = str.Split(",", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

            ScrambleStep newStep = groups[0] switch
            {
                "swPos" => new ScrambleStep(Instruction.SwapPos, int.Parse(groups[1]), int.Parse(groups[2])),
                "swLetter" => new ScrambleStep(Instruction.SwapLetter, char.Parse(groups[1]), char.Parse(groups[2])),
                "rLeft" => new ScrambleStep(Instruction.RotateLeft, int.Parse(groups[1])),
                "rRight" => new ScrambleStep(Instruction.RotateRight, int.Parse(groups[1])),
                "rLetter" => new ScrambleStep(Instruction.RotatePosLetter, char.Parse(groups[1])),
                "move" => new ScrambleStep(Instruction.Move, int.Parse(groups[1]), int.Parse(groups[2])),
                "rev" => new ScrambleStep(Instruction.Reverse, int.Parse(groups[1]), int.Parse(groups[2])),
                _ => throw new InvalidDataException("Unknonwn scramble step")
            };

            steps.Add(newStep);
        }

        public void ParseInput(List<string> lines)
        {
            startingString = lines[0];
            steps.Clear();
            foreach (var line in lines.Skip(1))
                ParseInstruction(line);
        }

        string Scramble(int part = 1)
        {
            var currentString = startingString;

            foreach (var step in steps)
            {
                Trace.WriteLine("In : " + currentString + " : " + " Op : " + step.what.ToString());

                currentString = step.what switch
                {
                    Instruction.SwapPos => Swap(currentString, step.pos1, step.pos2),
                    Instruction.SwapLetter => Swap(currentString, step.c1, step.c2),
                    Instruction.RotateLeft => RotateLeft(currentString, step.pos1),
                    Instruction.RotateRight => RotateRight(currentString, step.pos1),
                    Instruction.RotatePosLetter => Rotate(currentString, step.c1),
                    Instruction.Move => Move(currentString, step.pos1, step.pos2),
                    Instruction.Reverse => Reverse(currentString, step.pos1, step.pos2),
                    _ => throw new InvalidOperationException("Unknonwn scramble insruction")
                };
                Trace.WriteLine("out : " + currentString );
                Trace.WriteLine("------------------------------");
            }

            return currentString;
        }

        public string Solve(int part = 1)
            => Scramble(part);
    }
}
