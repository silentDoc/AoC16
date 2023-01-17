using System.Linq;
using System.Text;

namespace AoC16.Day18
{
    public class TileGenerator
    {
        string startingRow = "";
        int rowCount = 0;

        char GetPrevious(string row, int index)
            => (index < 0) ? '.' : (index == row.Length) ? '.' : row[index];

        char BuildTile((char, char, char) prevRowTriplet)
            => prevRowTriplet switch
            {
                ('^', '^', '.') => '^',
                ('.', '^', '^') => '^',
                ('^', '.', '.') => '^',
                ('.', '.', '^') => '^',
                _ => '.'
            };

        public string GenerateRow(string previousRow)
        {
            var newRow = new StringBuilder();
            
            List<(char, char, char)> triplets = new List<(char, char, char)>();

            for (int i = 0; i < previousRow.Length; i++)
                triplets.Add((GetPrevious(previousRow, i - 1), GetPrevious(previousRow, i), GetPrevious(previousRow, i + 1)));

            triplets.ForEach(x => newRow.Append(BuildTile(x)));
            return newRow.ToString();
        }

        public void ParseInput(List<string> lines)
        {
            var groups = lines[0].Split(",", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            startingRow = groups[0];
            rowCount = int.Parse(groups[1]);
        }

        int CountSafeTiles(int part =1)
        {
            if (part == 2)
                rowCount = 400000;      // Part 2 takes 4,334 secs, quite acceptable but I think that using StringBuilder has made me skip the point of the day

            var currentRow = startingRow;
            StringBuilder sb = new();

            for (int i = 0; i < rowCount; i++)
            {
                sb.Append(currentRow);
                currentRow = GenerateRow(currentRow);
            }

            return sb.ToString().Count(x => x == '.');
        }

        public int Solve(int part = 1)
            => CountSafeTiles(part);
    }
}
