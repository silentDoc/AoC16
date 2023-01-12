﻿namespace AoC16
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int day = 4;
            int part = 2;
            bool test = false;

            string input = "./Input/day" + day.ToString();
            input += (test) ? "_test.txt" : ".txt";

            Console.WriteLine("AoC 2015 - Day {0} , Part {1} - Test Data {2}", day, part, test);

            string result = day switch
            {
                1 => day1(input, part).ToString(),
                2 => day2(input, part).ToString(),
                3 => day3(input, part).ToString(),
                4 => day4(input, part).ToString(),
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
    }
}