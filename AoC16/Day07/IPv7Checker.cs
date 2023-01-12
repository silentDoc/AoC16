using AoC16.Day04;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AoC16.Day07
{
    class IPv7Address
    {
        List<string> hypernet = new();
        List<string> supernet = new();

        public IPv7Address(string inputLine)
        {
            Regex regex = new Regex(@"(([a-z]+)|(\[[a-z]+\]))");
            var matches = regex.Matches(inputLine);

            foreach(var match in matches.ToList()) 
            {
                var str = match.Groups[0].Value;
                if (str.StartsWith("["))
                    hypernet.Add(str);
                else
                    supernet.Add(str);
            }
        }

        bool hasABBA(string code)
            => code.Select((val, Index) => (Index<=code.Length-4) ? code[Index] == code[Index + 3] && code[Index + 1] == code[Index + 2] && code[Index] != code[Index + 1] : false).Any(x => x);

        public bool SupportsTLS
            => supernet.Select(x => hasABBA(x)).Any(x => x) && !hypernet.Select(x => hasABBA(x)).Any(x => x);
    }

     internal class IPv7Checker
     {
        List<IPv7Address> addresses = new();
        public void ParseInput(List<string> lines)
            => lines.ForEach(line => addresses.Add(new IPv7Address(line)));
       
        public int Solve(int part = 1)
            => (part == 1) ? addresses.Count(x => x.SupportsTLS) : 0;
    }
}
