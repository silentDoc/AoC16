using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC16.Day21
{
    public class Scrambler
    {
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

    }
}
