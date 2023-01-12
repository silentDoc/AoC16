using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC16.Day05
{
    internal class PasswordFinder
    {
        string prefix = "";
        public static string CreateMD5(string input)
        {
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                return Convert.ToHexString(hashBytes);
            }
        }

        public void ParseInput(List<string> lines)
            => prefix = lines[0].Trim();

        public string FindPass(int numZeros)
        {
            StringBuilder password = new("");
            string checkHash = new string("").PadLeft(numZeros, '0');
            long index = 0;

            while (password.Length < 8)
            {
                var hash = CreateMD5(prefix + index.ToString());
                if (hash.StartsWith(checkHash))
                    password.Append(hash[numZeros]);
                index++;
            }
            return password.ToString();
        }

        public string FindPass_v2(int numZeros)
        {
            string checkHash = new string("").PadLeft(numZeros, '0');
            long index = -1;
            Dictionary<int, char> password_v2 = new();

            while (password_v2.Values.Count < 8)
            {
                index++;
                var hash = CreateMD5(prefix + index.ToString());
                if (hash.StartsWith(checkHash))
                {
                    if (!char.IsDigit(hash[numZeros]))
                        continue;
                    int pos = int.Parse(hash[numZeros].ToString());
                    if (pos > 7)
                        continue;
                    if (password_v2.Keys.Contains(pos))
                        continue;
                    password_v2[pos] = hash[numZeros + 1];
                    Trace.WriteLine("Pos : " + pos.ToString() + " ; Value = " + hash[numZeros + 1]);
                }
            }
            StringBuilder password = new();
            for (int i = 0; i < 8; i++)
                password.Append(password_v2[i]);
            
            return password.ToString();
        }

        public string Solve(int part = 1)
            => (part == 1) ? FindPass(5) : FindPass_v2(5);

    }
}
