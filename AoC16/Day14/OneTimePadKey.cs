using AoC16.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AoC16.Day14
{
    internal class OneTimePadKey
    {
        private string Salt = "";
        int FindKey(int part = 1)
        {
            List<int> keys = new();
            Dictionary<int, string> candidateSubstrings = new();
            int index = -1;
            
            // For performance, let's try to calculate only the hash for the index ONLY ONCE
            while (keys.Count < 64)
            { 
                index++;
                string hashkey = Salt + index.ToString();
                var hash = Crypto.CreateMD5(hashkey).ToLower();

                var tripletInPos = hash.Where( (charInHash, i) => i >= 2 && hash[i - 1] == charInHash && hash[i - 2] == charInHash).ToList();

                if (tripletInPos.Count>0)
                    candidateSubstrings[index]  = new string(tripletInPos.First(), 5);
                else
                    continue;  // If there are no triplets then there are no five in a row

                var min_index = index - 1000;
                foreach (var key in candidateSubstrings.Keys.Where(k => k >= min_index && k != index))
                {
                    var substringToFind = candidateSubstrings[key];
                    if (hash.Contains(substringToFind))
                    {
                        keys.Add(key);
                        candidateSubstrings.Remove(key);
                    }
                }
            }
            return keys.Max();
        }

        public void ParseInput(List<string> lines)
            => Salt = lines[0];

        public int Solve(int part = 1)
            => FindKey(part);
    }
}
