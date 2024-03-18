using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack;

    internal class Dealer
    {
    private CardSupply cardSupply;

    public Dealer(CardSupply cardSupply) => this.cardSupply = cardSupply;

    //reference parameter so it can update throughout.
    public int DealerCount(List<string> dealerHand)
    {
        int cardScore = 0;
        int numberOfAces = 0;

        foreach (string card in dealerHand)
        {
            if (Enum.TryParse(card, out Rank rank))
            {
                if (rank != Rank.Ace)
                {
                    cardScore += (int)rank;
                }
                else
                {
                    numberOfAces++;
                }
            }
        }

        // Calculate final score with Ace logic
        int finalScore = cardScore;
        for (int i = 0; i < numberOfAces; i++)
        {
            if (finalScore + 11 <= cardSupply.MaxHand)
            {
                finalScore += 11;
            }
            else
            {
                finalScore += 1;
            }
        }

        return finalScore;
    }

    public bool Bust(List<string> playerHand, Hand hand) 
    {
        int finalScore = 0;
        if (hand == Hand.Dealer)
        {
            playerHand = cardSupply.DealerHand;
            return (DealerCount(playerHand) > 21);
            
        }
        else if (hand == Hand.Player) 
        {
            playerHand = cardSupply.UserHand;
            return (cardSupply.CardTotal(playerHand) > 21); 
        }
        return false;
    }
    public void DealerProcess()
    {
        List<String> hand = cardSupply.DealerHand;
        while (DealerCount(hand) < 17)
        {
            Console.WriteLine("Dealer's turn.");
            cardSupply.Draw(Hand.Dealer);
            Console.WriteLine($"Dealer total is {DealerCount(hand)}");
            if (Bust(hand, Hand.Dealer))
            {
                break;
            }
            Thread.Sleep(1500);
        }
    }
    public Result WinDecider(List<string> dealerHand, List<string> playerHand) 
    {
        //different winning situations
        dealerHand = cardSupply.DealerHand;
        playerHand = cardSupply.UserHand;
        Result result;
        //true will be if user wins, false will return for dealer

        result = (cardSupply.UserBlackJack) ? Result.PlayBJ :
                 (cardSupply.DealerBlackJack) ? Result.DealBJ :
                 (cardSupply.UserBlackJack && cardSupply.DealerBlackJack) ? Result.Tie :
                 Result.Next;

        if (result == Result.Next)
        {
            //bust 
            if (Bust(cardSupply.DealerHand, Hand.Dealer) == true)
            {
                result = Result.DealBust;
            }
            else if (Bust(cardSupply.UserHand, Hand.Player) == true)
            {
                result = Result.PlayBust;
            }
            // no bust
            else
            {
                //condensed instead of 3 if statements (ternary conditional)
                result = (DealerCount(dealerHand) > cardSupply.CardTotal(playerHand)) ? Result.DealTotal :
                         (DealerCount(dealerHand) < cardSupply.CardTotal(playerHand)) ? Result.PlayTotal :
                         Result.Tie;
            } 
        }
        return result;
    }
    
   
}

 enum Result { DealBust, PlayBust, DealBJ, PlayBJ, DealTotal, PlayTotal, Tie, Next}