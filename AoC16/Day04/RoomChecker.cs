using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace AoC16.Day04
{
    class Room
    {
        public string code;
        public int id;
        public string checksum;

        private readonly string letters = "abcdefghijklmnopqrstuvwxyz";

        public Room(string inputLine)
        {
            Regex regex = new Regex(@"([^0-9]+)([0-9]+)\[([a-z]+)\]");

            var groups = regex.Match(inputLine).Groups;
            code = groups[1].Value;
            id = int.Parse(groups[2].Value);
            checksum = groups[3].Value;
        }

        public bool IsReal()
        {
            Dictionary<char, int> occurrences = new();
            var letters = code.Replace("-", "");
            letters.ToList().ForEach(x => occurrences[x] = 0);

            foreach (var ch in letters)
                occurrences[ch]++;
            
            var counts = occurrences.Values.Distinct().OrderByDescending(x => x).ToList();
            StringBuilder check = new();

            foreach (var count in counts)
            {
                StringBuilder group_of_letters = new();
                var letters_count = occurrences.Keys.Where(x => occurrences[x] == count).ToList();
                letters_count = letters_count.OrderBy(x =>x).ToList();
                letters_count.ForEach(letter => group_of_letters.Append(letter));
                check.Append(group_of_letters.ToString());
            }

            return check.ToString().Substring(0,5) == checksum;
        }

        public string Decrypt()
        {
            string decrypt = code;
            decrypt = decrypt.Replace("-", " ");
            StringBuilder decryptedName = new();

            for (int i = 0; i < decrypt.Length; i++)
                decryptedName.Append(DecryptChar(decrypt[i], id));

            return decryptedName.ToString();
        }

        char DecryptChar(char source, int rotation)
            => (!char.IsLetter(source)) ? source : letters[(letters.IndexOf(source) + rotation) % letters.Length];
    }

    internal class RoomChecker
    {
        List<Room> rooms = new();
        public void ParseInput(List<string> lines)
            => lines.ForEach(line => rooms.Add(new Room(line)));
        
        public int Solve(int part = 1)
            => (part == 1) ? rooms.Where(x => x.IsReal()).Select(r => r.id).Sum()
                           : rooms.Where(x => x.IsReal() && x.Decrypt().Contains("northpole object storage")).Select(r => r.id).First();
    }
}
