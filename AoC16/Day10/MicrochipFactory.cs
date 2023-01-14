using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AoC16.Day10
{
    enum InstructionType
    { 
        ValueAssignation = 0, 
        BotDecision = 1
    }

    enum TargetType
    {
        Bot = 0,
        Output = 1, 
    }

    class Bot
    {
        public int BotNumber;
        public List<int> values;

        public int LowTarget;
        public TargetType LowTargetType;
        public int HighTarget;
        public TargetType HighTargetType;

        public Bot(int botNumber, int lowTarget, TargetType lowTargetType, int highTarget, TargetType highTargetType)
        {
            this.BotNumber = botNumber;
            this.LowTarget = lowTarget;
            this.LowTargetType = lowTargetType;
            this.HighTarget = highTarget;
            this.HighTargetType = highTargetType;
            values = new();
        }

        public void Receive(int value)
            => values.Add(value);

        public bool CanOperate
           => values.Count == 2;

        void MoveValue(List<Bot> botsList, Dictionary<int, List<int>> bins, TargetType type, int id, int value)
        {
            if (type == TargetType.Bot)
                botsList.Where(x => x.BotNumber == id).First().Receive(value);
            else
            {
                if (!bins.ContainsKey(id))
                    bins[id] = new();
                bins[id].Add(value);
            }
        }

        public bool CheckValues(int low, int high)
        {
            if (values.Count < 2)
                return false;
            var myLow = values.Min();
            var myHigh = values.Max();
            return myLow == low && myHigh == high;
        }

        public bool Operate(List<Bot> botsList, Dictionary<int, List<int>> outputList)
        {
            if (!CanOperate)
                return false;

            var low = values.Min();
            var high = values.Max();
            MoveValue(botsList, outputList, LowTargetType, LowTarget, low);
            MoveValue(botsList, outputList, HighTargetType, HighTarget, high);
            values.Clear();
            return true;
        }
    }

    internal class MicrochipFactory
    {
        Dictionary<int, List<int>> startingAssignations = new();
        Dictionary<int, List<int>> outputBins = new();
        List<Bot> bots = new();

        void ParseInstruction(string line)
        {
            if (line.StartsWith("value "))
            {
                var strValues = line.Replace("value ", "").Replace(" goes to bot ", ",").Split(",", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                var value = int.Parse(strValues[0]);
                var botNum = int.Parse(strValues[1]);
                if (!startingAssignations.ContainsKey(botNum))
                    startingAssignations[botNum] = new();
                startingAssignations[botNum].Add(value);
                return;
            }
            Regex regex = new Regex(@"bot (\d+) gives low to (output|bot) (\d+) and high to (output|bot) (\d+)");
            var groups = regex.Match(line).Groups;

            int botNumber = int.Parse(groups[1].Value);
            TargetType lowTargetType = (groups[2].Value == "bot") ? TargetType.Bot : TargetType.Output;
            int lowTarget = int.Parse(groups[3].Value);
            TargetType highTargetType = (groups[4].Value == "bot") ? TargetType.Bot : TargetType.Output;
            int highTarget = int.Parse(groups[5].Value);

            bots.Add(new Bot(botNumber, lowTarget, lowTargetType, highTarget, highTargetType));
        }

        public void ParseInput(List<string> lines)
            => lines.ForEach(line => ParseInstruction(line));

        private int RunFactory(int low, int high, int part = 1)
        {
            foreach (var key in startingAssignations.Keys)
            {
                foreach (var value in startingAssignations[key])
                    bots.Where(x => x.BotNumber == key).First().Receive(value);
            }
            
            bool canContinue = true;

            while (canContinue)
            {
                var somethingHappened = false;
                foreach (var bot in bots)
                {
                    if (bot.CheckValues(low, high) && part == 1)
                        return bot.BotNumber;
                    somethingHappened |= bot.Operate(bots, outputBins);
                }
                canContinue &= somethingHappened;
            }

            return outputBins[0].First() * outputBins[1].First() * outputBins[2].First();
        }

        public int Solve(int low, int high, int part = 1)
            => RunFactory(low, high, part);
    }
}
