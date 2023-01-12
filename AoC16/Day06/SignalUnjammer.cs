using AoC16.Day03;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC16.Day06
{
    internal class SignalUnjammer
    {
        List<Dictionary<char, int>> signalInput = new();

        void ParseLine(string line)
        {
            for(int pos=0; pos<line.Length; pos++) 
            {
                if (signalInput[pos].ContainsKey(line[pos]))
                    signalInput[pos][line[pos]]++;
                else
                    signalInput[pos][line[pos]] = 1;
            }
        }

        public void ParseInput(List<string> lines)
        {
            for (int i = 0; i < lines[0].Length; i++)
                signalInput.Add(new Dictionary<char, int>());

            lines.ForEach(line => ParseLine(line));
        }

        string FindWord()
        {
            StringBuilder word = new();
            foreach( var dict in signalInput) 
            {
                char letter = dict.Keys.Where(x => dict[x] == dict.Values.Max()).First();
                word.Append(letter);
            }
            return word.ToString();
        }

        public string Solve(int part = 1)
            => FindWord();
    }
}
