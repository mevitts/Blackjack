using System;
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
        internal void StartGame() {
            Console.WriteLine($@"{user.Balance} Credits.
                                 How much would you like to wager? (Increments of 1, 5, 10, 20, 50, 100)
                                 Or, type e to go back to menu.");
            int current = 0;
            string firstEntry = Console.ReadLine();
            if (firstEntry.ToLower() == "e")
            {
                
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
                    if (current < wager ) 
                    {
                        Console.WriteLine("Invalid wager amount. Try again.");
                        current++;
                    }

                    //seguir means continue, used as word to see if continue wagering because "continue" can't be used
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

                CardSupply.InitialDeal(1);


            }



            }
    }
}
