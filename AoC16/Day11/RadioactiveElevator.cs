using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AoC16.Day11
{
    enum ItemType
    { 
        RTG = 0,
        Microchip = 1
    }

    class Item
    {
        public int Id;     // Id will come handy when representing states, since the item order does not matter
        public string material;
        public ItemType type;

        public Item(string material, ItemType type)
        {
            this.material = material;
            this.type = type;
            var iId = DateTime.Now.Ticks.ToString();
            this.Id = int.Parse(iId.Substring(iId.Length - 8));
        }
    }

    class State
    {
        Dictionary<int, List<Item>> Floors = new();
        public int elevatorFloor = 1;

        public int cost = 0;

        public string StateSignature()
        {
            StringBuilder sb = new();
            sb.Append("E:" + elevatorFloor.ToString());
            for (int i = 1; i <= 4; i++)
            {
                var str = string.Join(",", Floors[i].Select(x => x.Id.ToString()).OrderBy(x => x).ToList());
                sb.Append(";F" + i.ToString() + ":" + str);
            }
            return sb.ToString();
        }

        // With the first state representation method, part 2 takes 10 minutes to solve. But here's the idea, the materials
        // are SWAPPABLE. That means that we do not care about the material, only about the count of each think by floor. 
        // This should speedup the process a lot (Part1 : 13,57s vs 0,5s -- And Part2 :  608s with v1 vs 2s !!!)
        public string StateSignature2()
        {
            StringBuilder sb = new();
            sb.Append("E:" + elevatorFloor.ToString());
            for (int i = 1; i <= 4; i++)
            {
                var num_rtg = Floors[i].Count(x => x.type == ItemType.RTG);
                var num_chip = Floors[i].Count(x => x.type == ItemType.Microchip);
                var str = "G:" + num_rtg.ToString() +",M:" + num_chip.ToString();
                sb.Append(";F" + i.ToString() + ":" + str);
            }
            return sb.ToString();
        }

        public State(int elevatorFloor = 1)
        {
            Floors = new();
            foreach (var i in Enumerable.Range(1, 4))
                Floors[i] = new();
            this.elevatorFloor = elevatorFloor;
        }

        public State(State source)
        {
            Floors = new();
            foreach (var i in Enumerable.Range(1, 4))
            {
                Floors[i] = new();
                foreach (var item in source.Floors[i])
                    Floors[i].Add(item);
            }
            elevatorFloor = source.elevatorFloor;
        }

        public void AddItem(Item item, int floor)
            => Floors[floor].Add(item);

        public void AddItems(List<Item> items, int floor)
           => items.ForEach(x => Floors[floor].Add(x));

        public void RemoveItems(List<Item> items, int floor)
            => items.ForEach(x => Floors[floor].Remove(x));

        public bool ValidState()
        {
            foreach (var i in Enumerable.Range(1, 4))
            {
                if (Floors[i].Count == 0)
                    continue;
                
                var generators = Floors[i].Where(x => x.type == ItemType.RTG).Select(x => x.material).ToList();
                if (generators.Count() == 0)
                    continue;
                
                var chips = Floors[i].Where(x => x.type == ItemType.Microchip).Select(x => x.material).ToList();
                if (chips.Count() == 0)
                    continue;
                
                foreach (var chip in chips)
                    if (!generators.Contains(chip))
                        return false;
            }
            return true;
        }

        public bool AllItemsInTopFloor
            => Floors[1].Count == 0 && Floors[2].Count == 0 && Floors[3].Count == 0 && Floors[4].Count != 0;

        public List<State> NextStates()
        {
            List<State> validNextStates = new();

            var currentFloor = Floors[elevatorFloor];
            var combinations = Common.ListUtils.GetCombinations(currentFloor, 2).Concat(Common.ListUtils.GetCombinations(currentFloor, 1)).ToList();
            var possibleDirections = (elevatorFloor == 1) ? new List<int>() { 1 } : (elevatorFloor == 4) ? new List<int>() { -1 } : new List<int>() { 1, -1 };

            foreach (var direction in possibleDirections)
                foreach (var combination in combinations)
                {
                    var newElevatorFloor = elevatorFloor + direction;
                    var stateToConsider = new State(this);
                    stateToConsider.cost = cost+1;
                    stateToConsider.RemoveItems(combination, elevatorFloor);
                    stateToConsider.AddItems(combination, newElevatorFloor);
                    stateToConsider.elevatorFloor = newElevatorFloor;

                    if(stateToConsider.ValidState())
                        validNextStates.Add(stateToConsider);
                }

            return validNextStates;
        }
    }

    internal class RadioactiveElevator
    {
        State startingState = new();

        void ParseLine(string line)
        {
            if (line.Contains("nothing relevant"))
                return;

            var str = line;
            // Replace and split are more practical than regex in this case
            str = str.Replace("The", "").Replace(" floor contains a ", ",").Replace(".", "");
            str = str.Replace(", a ", ",").Replace(", and a ", ",").Replace(" and a ", ",");
            Trace.WriteLine(str);
            var groups = str.Split(",", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            var floor = groups[0] switch
            {
                "first" => 1,
                "second" => 2,
                "third" => 3,
                "fourth" => 4,
                _ => throw new ArgumentException("Unknown floor")
            };

            var items = groups.Skip(1).ToList();
            foreach (var item in items)
            {
                bool isChip = item.IndexOf("-compatible microchip") != -1;
                ItemType type = isChip ? ItemType.Microchip : ItemType.RTG;
                string materialName = isChip ? item.Replace("-compatible microchip", "").Trim() : item.Replace(" generator", "").Trim();
                var itemToAdd = new Item(materialName, type);
                startingState.AddItem(itemToAdd, floor);
                
            }

            startingState.cost = 0;
            startingState.elevatorFloor = 1;
            Trace.WriteLine(startingState.StateSignature());
        }

        public void ParseInput(List<string> lines)
            => lines.ForEach(line => ParseLine(line));


        public int Simulate(int part = 1)
        {
            HashSet<string> KnownStates = new();
            Queue<State> activeStates = new();

            if (part == 2)
            {
                startingState.AddItem(new Item("elerium", ItemType.RTG), 1);
                startingState.AddItem(new Item("elerium", ItemType.Microchip), 1);
                startingState.AddItem(new Item("dilithium", ItemType.RTG), 1);
                startingState.AddItem(new Item("dilithium", ItemType.Microchip), 1);
            }

            activeStates.Enqueue(startingState);

            while (activeStates.Count > 0)
            { 
                var currentState = activeStates.Dequeue();
                if (!KnownStates.Add(currentState.StateSignature2()))
                    continue;

                if (currentState.AllItemsInTopFloor)
                    return currentState.cost;

                var nextStates = currentState.NextStates();
                foreach (var s in nextStates)
                    activeStates.Enqueue(s);
            }

            return -1;
        }

        public int Solve(int part = 1)
            => Simulate(part);
    }
}
