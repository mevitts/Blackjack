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
    public int DealerCount(ref List<string> dealerHand)
    {
        int cardScore = 0;
        int finalScore = 0;
        int numberOfAces = 0;

        foreach (string card in dealerHand)
        {
            if (Enum.TryParse(card, out Rank rank))
            {   

                while (finalScore < 17)
                {
                    
                    if (rank != Rank.Ace)
                    {
                        cardScore += (int)rank;
                        finalScore = cardScore;
                    }
                    else
                    {
                        numberOfAces++;
                    }
                    int maxScoreWithAces = cardScore + numberOfAces * 11;

                    //logic to decrease ace value if bust until in sweetspot range of 17 to 21
                    while (maxScoreWithAces > cardSupply.MaxHand && numberOfAces > 0 && cardScore >= 17)
                    {
                        maxScoreWithAces -= 10;
                        numberOfAces--;
                        cardScore = maxScoreWithAces;
                    }
                    finalScore = cardScore;
                    cardSupply.Draw(Hand.Dealer);
                    //updates list to continue counting
                    dealerHand = cardSupply.DealerHand;
                }
            }
        }
        return finalScore;
    }
    public bool Bust(List<string> playerHand, Hand hand) 
    {
        if (hand == Hand.Dealer)
        {
            playerHand = cardSupply.DealerHand;
            return (DealerCount(ref playerHand) > 21);
            
        }
        else if (hand == Hand.Player) 
        {
            playerHand = cardSupply.UserHand;
            return (cardSupply.CardTotal(playerHand) > 21);
            
        }
        return false;
    }
    public void DealerProcess(List<string> hand)
    {
        hand = cardSupply.DealerHand;
        while (DealerCount(ref hand) < 17 || Bust(hand, Hand.Dealer) == false)
        {
            hand = cardSupply.DealerHand;
            cardSupply.Draw(Hand.Dealer);
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
                result = (DealerCount(ref dealerHand) > cardSupply.CardTotal(playerHand)) ? Result.DealTotal :
                         (DealerCount(ref dealerHand) < cardSupply.CardTotal(playerHand)) ? Result.PlayTotal :
                         Result.Tie;
            } 
        }
        return result;
    }
    
   
}

 enum Result { DealBust, PlayBust, DealBJ, PlayBJ, DealTotal, PlayTotal, Tie, Next}