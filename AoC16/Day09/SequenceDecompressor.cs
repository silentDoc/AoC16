using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC16.Day09
{
    public class SequenceDecompressor
    {
        string input = "";

        public string Decompress(string input)
        {
            var processText = input;
            StringBuilder sb = new();

            while(processText.Length>0)
            {
                var openParenthesis = processText.IndexOf("(");
                var closeParenthesis = processText.IndexOf(")");

                if (openParenthesis == -1)
                {
                    sb.Append(processText);
                    break;
                }

                if (openParenthesis > 0)
                    sb.Append(processText.Substring(0, openParenthesis));

                var marker = processText.Substring(openParenthesis, closeParenthesis-openParenthesis + 1).Replace("(","").Replace(")", "");
                var groups = marker.Split("x");
                int count = int.Parse(groups[0]);
                int times = int.Parse(groups[1]);

                for (int i = 0; i < times; i++)
                    sb.Append(processText.Substring(closeParenthesis + 1, count));

                processText = processText.Substring(closeParenthesis + count+1);
            }

            return sb.ToString();
        }

        public void ParseInput(List<string> lines)
            => input = lines[0];

        public int Solve(int part)
            => (part == 1) ? Decompress(input).Length : 0;
    }
}
