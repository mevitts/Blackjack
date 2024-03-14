using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{
    internal class Transaction
    {
        public int Amount { get; set; }
        public DateTime Date { get; set; }
        public Result GameResult { get; set; }
    }
}
