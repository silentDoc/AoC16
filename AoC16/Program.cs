namespace AoC16
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int day = 6;
            int part = 1;
            bool test = true;

            string input = "./Input/day" + day.ToString();
            input += (test) ? "_test.txt" : ".txt";

            Console.WriteLine("AoC 2016 - Day {0} , Part {1} - Test Data {2}", day, part, test);

            string result = day switch
            {
                1 => day1(input, part).ToString(),
                2 => day2(input, part).ToString(),
                3 => day3(input, part).ToString(),
                4 => day4(input, part).ToString(),
                5 => day5(input, part).ToString(),
                6 => day6(input, part).ToString(),
                _ => throw new ArgumentException("Wrong day number - unimplemented")
            };
            Console.WriteLine("Result : {0}", result);
        }

        static int day1(string input, int part)
        {
            var lines = File.ReadAllLines(input).ToList();
            Day01.HQFinder hqfinder = new();
            hqfinder.ParseInput(lines);

            return hqfinder.Solve(part);
        }

        static string day2(string input, int part)
        {
            var lines = File.ReadAllLines(input).ToList();
            Day02.BathroomPinCode pin = new();
            pin.ParseInput(lines);

            return pin.Solve(part);
        }

        static int day3(string input, int part)
        {
            var lines = File.ReadAllLines(input).ToList();
            Day03.TriangleChecker checker = new();
            checker.ParseInput(lines, part);
            return checker.Solve();
        }

        static int day4(string input, int part)
        {
            var lines = File.ReadAllLines(input).ToList();
            Day04.RoomChecker checker = new();
            checker.ParseInput(lines);

            return checker.Solve(part);
        }

        static string day5(string input, int part)
        {
            var lines = File.ReadAllLines(input).ToList();
            Day05.PasswordFinder finder = new();
            finder.ParseInput(lines);
            return finder.Solve(part);
        }

        static int day6(string input, int part)
        {
            var lines = File.ReadAllLines(input).ToList();
            return 0;
        }
    }
}