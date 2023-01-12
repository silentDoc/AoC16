using AoC16.Day04;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

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

        public bool SupportsSSL
            => CheckSSL();

        bool CheckSSL()
        {
            var babs = GetBABs();
            bool supportsSSL = false;
            foreach (var bab in babs)
                supportsSSL |= hypernet.Select(x => x.Contains(bab)).Any(x => x);
            return supportsSSL;
        }

        List<string> GetBABs()
        {
            List<string> abas = new();
            foreach(var code in supernet)
            {
                for (int i = 0; i < code.Length - 2; i++)
                    if (code[i] == code[i + 2] && code[i] != code[i + 1])
                        abas.Add(code.Substring(i, 3));
            }

            abas = abas.Distinct().ToList();
            List<string> babs = new();
            foreach (var aba in abas)
                babs.Add(aba[1].ToString() + aba[0].ToString() + aba[1].ToString());
            return babs;
        }

    }

     internal class IPv7Checker
     {
        List<IPv7Address> addresses = new();
        public void ParseInput(List<string> lines)
            => lines.ForEach(line => addresses.Add(new IPv7Address(line)));
       
        public int Solve(int part = 1)
            => (part == 1) ? addresses.Count(x => x.SupportsTLS) : addresses.Count(x => x.SupportsSSL);
    }
}
