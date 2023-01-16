using System.Diagnostics;

namespace AoC16
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int day = 13;
            int part = 2;
            bool test = false;

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
                7 => day7(input, part).ToString(),
                8 => day8(input, part).ToString(),
                9 => day9(input, part).ToString(),
                10 => day10(input, part, test).ToString(),
                11 => day11(input, part).ToString(),
                12 => day12(input, part).ToString(),
                13 => day13(input, part).ToString(),
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

        static string day6(string input, int part)
        {
            var lines = File.ReadAllLines(input).ToList();
            Day06.SignalUnjammer unjammer = new();
            unjammer.ParseInput(lines);
            return unjammer.Solve(part);
        }

        static int day7(string input, int part)
        {
            var lines = File.ReadAllLines(input).ToList();
            Day07.IPv7Checker checker = new();
            checker.ParseInput(lines);
            return checker.Solve(part);
        }

        static int day8(string input, int part)
        {
            var lines = File.ReadAllLines(input).ToList();
            Day08.KeypadScreen screen = new();
            screen.ParseInput(lines);
            return screen.Solve(part);
        }

        static long day9(string input, int part)
        {
            var lines = File.ReadAllLines(input).ToList();
            Day09.SequenceDecompressor decomp = new();
            decomp.ParseInput(lines);

            return decomp.Solve(part);
        }

        static int day10(string input, int part, bool test)
        {
            var lines = File.ReadAllLines(input).ToList();
            Day10.MicrochipFactory factory = new();
            factory.ParseInput(lines);
            var low = (test) ? 2 : 17;
            var high = (test) ? 5 : 61;

            return factory.Solve(low, high, part);
        }

        static int day11(string input, int part)
        {
            var lines = File.ReadAllLines(input).ToList();
            Day11.RadioactiveElevator elevator = new();
            elevator.ParseInput(lines);
            Stopwatch st = new();
            st.Start();
            var ret = elevator.Solve(part);
            st.Stop();
            Console.WriteLine( st.Elapsed.TotalSeconds.ToString() );
            return ret;
        }

        static int day12(string input, int part)
        {
            var lines = File.ReadAllLines(input).ToList();
            Day12.AssembunnyProcessor asm = new();
            asm.ParseInput(lines);

            return asm.Solve(part);
        }

        static int day13(string input, int part)
        {
            var lines = File.ReadAllLines(input).ToList();
            Day13.CubicleMaze maze = new();
            maze.ParseInput(lines);
            return maze.Solve(part);
        }
    }
}