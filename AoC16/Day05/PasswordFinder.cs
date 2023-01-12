using System;
using System.Collections.Generic;
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

        public string Solve(int part =1)
            => (part == 1) ? FindPass(5) : "";
    }
}
