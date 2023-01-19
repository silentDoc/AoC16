﻿using System.Diagnostics;

namespace AoC16
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int day = 21;
            int part = 1;
            bool test = false;

            string input = "./Input/day" + day.ToString();
            input += (test) ? "_test.txt" : ".txt";

            Console.WriteLine("AoC 2016 - Day {0} , Part {1} - Test Data {2}", day, part, test);
            Stopwatch st = new Stopwatch();
            st.Start();
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
                14 => day14(input, part).ToString(),
                15 => day15(input, part).ToString(),
                16 => day16(input, part).ToString(),
                17 => day17(input, part).ToString(),
                18 => day18(input, part).ToString(),
                19 => day19(input, part).ToString(),
                20 => day20(input, part).ToString(),
                21 => day21(input, part).ToString(),
                _ => throw new ArgumentException("Wrong day number - unimplemented")
            };
            st.Stop();
            Console.WriteLine("Result : {0}", result);
            Console.WriteLine("Ellapsed : {0}", st.Elapsed.TotalSeconds);
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

            return elevator.Solve(part);
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

        static int day14(string input, int part)
        {
            var lines = File.ReadAllLines(input).ToList();
            Day14.KeyGen keygen = new();
            keygen.ParseInput(lines);
            return keygen.Solve(part);
        }

        static int day15(string input, int part)
        {
            var lines = File.ReadAllLines(input).ToList();
            Day15.DiscStack stack = new();
            stack.ParseInput(lines);

            return stack.Solve(part);
        }

        static string day16(string input, int part)
        {
            var lines = File.ReadAllLines(input).ToList();
            Day16.RandomDataGen gen = new();
            gen.ParseInput(lines);

            return gen.Solve(part);
        }

        static string day17(string input, int part)
        {
            var lines = File.ReadAllLines(input).ToList();
            Day17.VaultNavigator navi = new();
            navi.ParseInput(lines);

            return navi.Solve(part);
        }

        static int day18(string input, int part)
        {
            var lines = File.ReadAllLines(input).ToList();
            Day18.TileGenerator tilegen = new();
            tilegen.ParseInput(lines);

            return tilegen.Solve(part);
        }

        static int day19(string input, int part)
        {
            var lines = File.ReadAllLines(input).ToList();
            Day19.ElvenRing ring = new();
            ring.ParseInput(lines);
            
            return ring.Solve(part);
        }

        static long day20(string input, int part)
        {
            var lines = File.ReadAllLines(input).ToList();
            Day20.IPRangeManager manager = new();
            manager.ParseInput(lines);
            return manager.Solve(part);
        }

        static string day21(string input, int part)
        {
            var lines = File.ReadAllLines(input).ToList();
            Day21.Scrambler scr = new();
            scr.ParseInput(lines);
            
            return scr.Solve(part);
        }
    }
}