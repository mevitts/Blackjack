using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{
    internal class Transaction
    {
        public int Amount { get; }
        public DateTime Date { get; }
        public string Notes { get; }

        public Transaction(int amount, DateTime date, string result)
        {
            Amount = amount;
            Date = date;
            Notes = result;
        }
    }
}
