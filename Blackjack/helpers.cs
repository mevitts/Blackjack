﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using static System.Net.Mime.MediaTypeNames;

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

        internal static bool BlackJack(List<string> hand)
        {
            return (hand.Contains("Ace") && hand.Any(face => face == "King" || face == "Queen" || face == "Jack"));
        }
        internal static void AddToTransactions(Transaction transaction, List<Transaction> allTransactions)
        {
            //will wait to be added until after windecider decides win or loss and changes current to -, +, *1.5 or0
            allTransactions.Add(transaction);
        }

        internal static string WinOrLoss(Result result)
        {
            switch (result)
            {
                case Result.DealBust:
                case Result.PlayBJ:
                case Result.PlayTotal:
                    return "Win";
                case Result.Tie:
                    return "Tie";
                default:
                    return "Loss";
            }
        }
    }
}
