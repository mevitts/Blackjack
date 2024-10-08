﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{
    internal class GameProcess
    {
        User user = new User("Matt");

        internal void GameMenu()
        {

            Console.Clear();
            Console.WriteLine("Welcome to Blackjack. Type if you would like to play or check stats (p/c)");
            var response = Console.ReadLine();
            if (response.ToLower() == "p")
            {
                StartGame();
            }
            else
            {
                sqLiteDB.GetAllRecords();
            }
        }
        internal void StartGame() {
            do { 
            Console.Clear();
            Console.WriteLine($@"{user.Balance} Credits.
                                 How much would you like to wager? (Increments of 1, 5, 10, 20, 50, 100)
                                 Or, type e to go back to menu.");
            int current = 0;
            string firstEntry = Console.ReadLine();
            if (firstEntry.ToLower() == "e")
            {
                    GameMenu();
            }
            //need to add other cases, like if increment is -1,, or if the answer/response was not an integer.
            {
                bool response = true;
                while (response == true)
                {
                    int amount = int.Parse(firstEntry);
                    //this creates a comparable variable due to the way increment works. If a non valid value is typed, it decreases current by 1. 
                    //so if it is less than wager after Increment() is run, then it was non valid, and then current is increased back by 1 again.
                    int wager = current;
                    current = helpers.Increment(current, amount);
                    if (current < wager)
                    {
                        Console.WriteLine("Invalid wager amount. Try again.");
                        current++;
                    }

                        //seguir means continue, used as word to see if continue wagering because "continue" can't be used
                        Console.Clear();
                    Console.WriteLine($"{current} wagered. Type w to wager more, type s to start the game, or e to return to menu)");
                    string seguir = Console.ReadLine();
                    if (seguir.ToLower() == "s")
                    {
                        response = false;
                        break;
                    }
                    else if (seguir.ToLower() == "e")
                    {
                        //Menu()
                    }
                    else if (seguir.ToLower() == "w")
                    {
                        response = true;
                    }
                }


                CardSupply cardSupply = new CardSupply();
                Dealer dealer = new(cardSupply);


                cardSupply.CreateCards();
                CardSupply.InitialDeal(1);

                int userTotal = 0;
                int dealerTotal = 0;
                bool continueGame = true;

                List<string> dealerHand = cardSupply.DealerHand;

                var bjResult = (dealer.WinDecider(cardSupply.DealerHand, cardSupply.UserHand));
                    bool continueBJ = false;
                    var result = new Result();
                    if (bjResult == Result.Tie || bjResult == Result.PlayBJ || bjResult == Result.DealBJ)
                {
                    continueGame = false;
                        continueBJ = true;
                        result = bjResult;
                }
                
                while (continueGame)
                {
                    userTotal = cardSupply.CardTotal(cardSupply.UserHand);
                    dealerTotal = dealer.DealerCount(dealerHand);
                        Console.Clear();
                    Console.WriteLine($"Your total is {userTotal}. Your dealer is showing {cardSupply.DealerHand[0]}. Will you hit or stay? (h/s)");
                    string userResponse = Console.ReadLine();
                    while (userResponse.ToLower() != "h" && userResponse.ToLower() != "s")
                    {
                        Console.WriteLine("Type h or s");
                        userResponse = Console.ReadLine();
                    }
                    if (userResponse.ToLower() == "s")
                    {
                        Console.WriteLine($"Dealer reveals {cardSupply.DealerHand[1]}, totalling {dealer.DealerCount(dealerHand)}.");
                        Thread.Sleep(1000);
                        dealer.DealerProcess();
                        continueGame = false;
                    }
                    else
                    {
                        cardSupply.Draw(Hand.Player);
                        Console.WriteLine($"Total is {cardSupply.CardTotal(cardSupply.UserHand)}");
                            Thread.Sleep (2000);

                        if (dealer.Bust(cardSupply.UserHand, Hand.Player))
                        {
                                Console.WriteLine("You busted.");
                                Thread.Sleep(1000); 
                            continueGame = false;
                        }
                    }
                }

                    if (!continueBJ)
                    {
                        

                        if (!dealer.Bust(cardSupply.UserHand, Hand.Player))
                        {
                            result = dealer.WinDecider(cardSupply.DealerHand, cardSupply.UserHand);
                        }
                        else
                        {
                            result = Result.PlayBust;
                        }
                    }
                
                
                switch (result)
                {
                    case Result.PlayBJ:
                        current = (int)(current * Math.Ceiling(1.5));
                        Console.WriteLine($"You win by blackjack. You win {current}");
                        break;
                    case Result.DealBJ:
                        Console.WriteLine($"The dealer wins by blackjack. You lose {current}");
                        current = -current;
                        break;
                    case Result.Tie:
                        Console.WriteLine("The hand is a draw");
                        current = 0;
                        break;
                    case Result.DealBust:
                        Console.WriteLine($"The dealer busts. You win {current}");
                        break;
                    case Result.PlayBust:
                        Console.WriteLine($"You bust, you lose {current}.");
                        current = -current;
                        break;
                    case Result.DealTotal:
                        Console.WriteLine($"The dealer wins this hand. You lose {current}");
                        current = -current;
                        break;
                    case Result.PlayTotal:
                        Console.WriteLine($"You win this hand. You win {current}");
                        break;
                }

                    Transaction transaction = new Transaction() ;
                    transaction.GameResult = result;
                    transaction.Amount = current;
                    transaction.Date = DateTime.Now;

                helpers.AddToTransactions(transaction, user.AllTransactions);
                cardSupply.ReplenishCards();

                
                Console.WriteLine($"\n\n{user.Balance} Credits. Press any key to continue");
                    sqLiteDB.AddHand(transaction);
                Console.ReadLine();
            }
        }while (true);


        } 

    }
    
}
