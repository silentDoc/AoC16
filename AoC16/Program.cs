﻿namespace AoC16
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int day = 1;
            int part = 2;
            bool test = false;

            string input = "./Input/day" + day.ToString();
            input += (test) ? "_test.txt" : ".txt";

            Console.WriteLine("AoC 2015 - Day {0} , Part {1} - Test Data {2}", day, part, test);

            string result = day switch
            {
                1 => day1(input, part).ToString(),
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
    }
}