using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC16.Day19
{
    class Elf
    {
        public int id;
        public Elf? previous;
        public Elf? next;
    }

    internal class ElvenRing  // Could not resist the naming ;) 
    {
        int numElves = 0;

        public void ParseInput(List<string> lines)
            => numElves = int.Parse(lines[0]);

        int PlayCircle(int part = 1)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            // Double linked list
            var thief = new Elf();
            var goes_out = new Elf();
            thief.id = 1;
            var ptr = thief;

            for (int i = 2; i <= numElves; i++)
            {
                ptr.next = new Elf();
                ptr.next.id = i;
                ptr.next.previous = ptr;
                ptr = ptr.next;
            }
            ptr.next = thief;
            thief.previous = ptr;


            if (part == 1)
            {
                while (goes_out != thief)
                {

                    goes_out = thief.next;
                    goes_out.previous.next = goes_out.next;         // we disconnect the one who goes out from his next and previous
                    goes_out.next.previous = goes_out.previous;
                    thief = thief.next;
                }
            }
            else
            {
                // part 2
                int index_first_goes_out = numElves / 2 + 1;
                goes_out = thief;
                while (goes_out.id != index_first_goes_out)
                    goes_out = goes_out.next;

                int currentElfCount = numElves;
                // As the circle reduces, we should keep track of where is our position (thief) and where is the position of the elf that goes out
                while (goes_out != thief)
                {
                    goes_out.previous.next = goes_out.next;         // we disconnect the one who goes out from his next and previous
                    goes_out.next.previous = goes_out.previous;
                    
                    goes_out = goes_out.next;
                    if(currentElfCount %2 == 1)
                        goes_out = goes_out.next;   // Yet another one when the count is odd
                    
                    currentElfCount--;
                    thief = thief.next;
                }
            }
            return thief.id;
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
        }



        public int Solve(int part = 1)
            => PlayCircle(part);
    }
}
