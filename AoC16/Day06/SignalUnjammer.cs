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
        List<Dictionary<char, int>> signal = new();

        void ParseLine(string line)
        {
            for(int pos=0; pos<line.Length; pos++) 
            {
                if (signal[pos].ContainsKey(line[pos]))
                    signal[pos][line[pos]]++;
                else
                    signal[pos][line[pos]] = 1;
            }
        }

        public void ParseInput(List<string> lines)
        {
            for (int i = 0; i < lines[0].Length; i++)
                signal.Add(new Dictionary<char, int>());

            lines.ForEach(line => ParseLine(line));
        }

        string FindWord(int part = 1)
        {
            StringBuilder word = new();
            foreach( var dict in signal) 
            {
                var count = (part ==1) ? dict.Values.Max() : dict.Values.Min();
                char letter = dict.Keys.Where(x => dict[x] == count).First();
                word.Append(letter);
            }
            return word.ToString();
        }

        public string Solve(int part = 1)
            => FindWord(part);
    }
}
