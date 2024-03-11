using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{
    internal class helpers
    {
        internal static int Increment(int current, int amount)
        {
            int[] validIncs = { 1, 5, 10, 20, 50, 100 };
            if (!validIncs.Contains(amount))
            {
                Console.WriteLine("Can only increase by 1, 5, 10, 20, 50 or 100");
                int currentDec = current - 1;
                return currentDec;
            }
            else
            {
                current += amount;
                return current;
            } 
        }
    }
}
