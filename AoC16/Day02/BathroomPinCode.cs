using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AoC16.Day02
{
    record struct Coord
    {
        public int x;
        public int y;
    }

    internal class BathroomPinCode
    {
        List<string> instructions = new();

        public void ParseInput(List<string> lines)
            => lines.ForEach(x => instructions.Add(x));

        Coord Move(char direction, Coord coord)
            => direction switch
            {
                'U' => coord with { y = coord.y == 0 ? 0 : coord.y - 1 },
                'D' => coord with { y = coord.y == 2 ? 2 : coord.y + 1 },
                'L' => coord with { x = coord.x == 0 ? 0 : coord.x - 1 },
                'R' => coord with { x = coord.x == 2 ? 2 : coord.x + 1 },
                _ => throw new Exception("Invalid direction")
            };

        string Digit(Coord position)
            => (position.y * 3 + position.x + 1).ToString();

        (string number, Coord finalPosition) FindDigit(Coord startPosition, string instructions)
        {
            Coord current = startPosition;
            foreach (char step in instructions)
                current = Move(step, current);

            return (Digit(current), current);
        }

        string FindPin(int part = 1)
        {
            var current = new Coord() { x = 1, y = 1 };
            StringBuilder sb = new();
            
            foreach (var line in instructions)
            {
                (string digit, current) = FindDigit(current, line);
                sb.Append(digit);
            }
            return sb.ToString();
        }

        public string Solve(int part = 1)
            => FindPin(part);
    }
}
