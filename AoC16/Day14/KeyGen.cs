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
    internal class KeyGen
    {
        private string Salt = "";
        int FindKey(int part = 1)
        {
            List<int> keys = new();
            Dictionary<int, string> candidateSubstrings = new();
            int index = -1;
            bool endScan = false;
            int safeIndex = 0;

            // For performance, let's try to calculate only the hash for the index ONLY ONCE
            while (!endScan)
            { 
                index++;
                string hashkey = Salt + index.ToString();
                var hash = Crypto.CreateMD5(hashkey).ToLower();
                if (part == 2)
                    hash = KeyStretch(hash, 2016);

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
                        if (keys.Count == 64)
                            safeIndex = keys.Max() + 1000;
                    }
                }

                // The exit condition is trickier than I thought. We should keep checking once we have 64 indexes because
                // we may have one lower than the first candidate that matches with a further 5 in a row - The result on part 1 was luck :P :D
                endScan = keys.Count > 64 && index > safeIndex;  
            }
            return keys.OrderBy(x => x).Take(64).Max();
        }

        string KeyStretch(string input, int stretch)
        {
            var hashkey = input;
            for (int i = 0; i < stretch; i++)
                hashkey = Crypto.CreateMD5(hashkey).ToLower();
            return hashkey;
        }

        public void ParseInput(List<string> lines)
            => Salt = lines[0];

        public int Solve(int part = 1)
            => FindKey(part);
    }
}
