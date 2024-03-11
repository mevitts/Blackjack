using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack;

    internal class User
    {
        public string Name {  get; set; }
        public int Balance
        {
            get
            {
                int balance = 1000;
                foreach (var item in _allTransactions)
                {
                    balance += item.Amount;
                }
                return balance;
            }
        }

    public List<String> userHand;


    public User(string name)
        {
            Name = name;
        }
            private readonly List<Transaction> _allTransactions = new();
    }




    

