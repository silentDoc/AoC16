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

        Coord Move(char direction, Coord coord)     // 0,0 is top left corner
            => direction switch
            {
                'U' => coord with { y = coord.y == 0 ? 0 : coord.y - 1 },
                'D' => coord with { y = coord.y == 2 ? 2 : coord.y + 1 },
                'L' => coord with { x = coord.x == 0 ? 0 : coord.x - 1 },
                'R' => coord with { x = coord.x == 2 ? 2 : coord.x + 1 },
                _ => throw new Exception("Invalid direction")
            };

        Coord Move_P2(char direction, Coord coord)  // 0,0 is center
            => direction switch
            {
                'U' => coord with { y = coord.y == -2 ? -2 : Math.Max( coord.y - 1, -1 * (2-Math.Abs(coord.x)) ) },
                'D' => coord with { y = coord.y == 2 ? 2   : Math.Min(coord.y + 1, 2 - Math.Abs(coord.x)) },
                'L' => coord with { x = coord.x == -2 ? -2 : Math.Max(coord.x - 1, -1 * (2 - Math.Abs(coord.y))) },
                'R' => coord with { x = coord.x == 2 ? 2   : Math.Min(coord.x + 1, 2 - Math.Abs(coord.y)) },
                _ => throw new Exception("Invalid direction")
            };

        string Digit(Coord position)
            => (position.y * 3 + position.x + 1).ToString();

        string Digit_P2(Coord position)
        =>  (position.y, position.x) switch
            {
                (-2, 0) => "1",
                (-1, -1) => "2",
                (-1, 0) => "3",
                (-1, 1) => "4",
                (0, -2) => "5",
                (0, -1) => "6",
                (0, 0) => "7",
                (0, 1) => "8",
                (0, 2) => "9",
                (1, -1) => "A",
                (1, 0) => "B",
                (1, 1) => "C",
                (2, 0) => "D",
                (_, _) => throw new Exception("Invalid direction"),
            };

        (string number, Coord finalPosition) FindDigit(Coord startPosition, string instructions, int part =1)
        {
            Coord current = startPosition;
            foreach (char step in instructions)
                current = (part ==1) ? Move(step, current) : Move_P2(step, current);

            return (part ==1 ? Digit(current) : Digit_P2(current), current);
        }

        string FindPin(int part = 1)
        {
            var current = (part == 1) ? new Coord() { x = 1, y = 1 }    // Part 1 - Pinboard considerers 0,0 top left , start at 5 (center)
                                      : new Coord() { x = -2, y = 0 };  // Part 2 - Pinboard considerers 0,0 the center, start at 5 (central row, leftmost)
            StringBuilder sb = new();
            
            foreach (var line in instructions)
            {
                (string digit, current) = FindDigit(current, line, part);
                sb.Append(digit);
            }
            return sb.ToString();
        }

        public string Solve(int part = 1)
            => FindPin(part);
    }
}
