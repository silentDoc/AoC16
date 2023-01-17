using AoC16.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC16.Day17
{
    class State
    {
        public Coord2D position;
        public string hashkey;

        public State(Coord2D position, string hashkey)
        {
            this.position = position;
            this.hashkey = hashkey;
        }

        public List<State> GetNextStates()
        {
            var neighBors = position.GetNeighbors(Coord2D.Arrangement.UpDownLeftRight).ToList();
            var validNeighbors = neighBors.ToList();
            var openDoors = FindOpenDoors();
            char[] dirs = new char[4] { 'U', 'D', 'L', 'R' };
            
            List<State> retVal = new();

            for (int index = 0; index < 4; index++)
            {
                var n = neighBors[index];
                if (n.x < 0 || n.y < 0 || n.x > 3 || n.y > 3 || !openDoors[index])
                    validNeighbors.Remove(n);
                else
                    retVal.Add(new State(n, hashkey + dirs[index].ToString()));
            }

            return retVal;
        }

        private List<bool> FindOpenDoors()
        {
            // up, down, left, and right
            var hashed = Crypto.CreateMD5(hashkey).ToLower().Substring(0,4);
            char[] interest = new char[5] { 'b','c','d','e', 'f'};
            return hashed.Select(c => interest.Contains(c)).ToList();
        }
    }

    internal class VaultNavigator
    {
        public string seedHash = "";

        public void ParseInput(List<string> lines)
            => seedHash = lines[0].Trim();

        // Hashing also means that we will not have any visited state
        public string NavigateVault(int part = 1)
        {
            HashSet<string> KnownStates = new();
            Queue<State> activeStates = new();
            State startingState = new State(new Coord2D(0,0), seedHash);

            Coord2D exit = new Coord2D(3, 3);
            List<string> endingHashes = new();
            
            activeStates.Enqueue(startingState);
            while (activeStates.Count > 0)
            {
                var currentState = activeStates.Dequeue();

                if (currentState.position == exit)
                { 
                    if (part == 1)
                        return currentState.hashkey.Substring(seedHash.Length);
                    else
                    {
                        endingHashes.Add(currentState.hashkey);
                        continue;
                    }
                }

                var nextStates = currentState.GetNextStates();
                foreach (var s in nextStates)
                    activeStates.Enqueue(s);
            }

            var lastHash = endingHashes.Last();
            lastHash = lastHash.Substring(seedHash.Length);

            return lastHash.Length.ToString();
        }

        public string Solve(int part = 1)
            => NavigateVault(part);
    }
}
