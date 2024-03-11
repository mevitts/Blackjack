
using System.Threading.Tasks.Sources;

namespace Blackjack
{
    class CardSupply
    {
        private const int TOTAL_CARDS = 312;
        private const int MAX_HAND = 21;
        private static List<String> dealerHand;
        private static List<String> userHand;
        private static List<String> cards;



        public List<String> CreateCards()
        {

            cards = new List<String>();
            //24 loops because 6 decks of 4 suits
            for (int i = 0; i < 24; i++) {
                foreach (Rank rank in Enum.GetValues(typeof(Rank))) {
                    //excluding Ace1 and 2 because i have a combination which is Ace
                    if (rank != Rank.Ace1 && rank != Rank.Ace2)
                    {
                        cards.Add(rank.ToString());
                    }
                }
            }

            return cards;
        }
        public string Draw(Hand hand)
        {
            var rand = new Random();
            var randomCardIndex = rand.Next(cards.Count);
            var card = cards[randomCardIndex];
            
            if (hand == Hand.Player)
            {
                userHand.Add(card);
            }
            else if (hand == Hand.Dealer) {
                dealerHand.Add(card);
            }
            cards.RemoveAt(randomCardIndex);
            return card;
        }

        
        //draft 4 cards (2 for dealer and 2 for player) formatted for multiple players. and split based on how it should be on order.
        public static void InitialDeal(int players)
        {
            List<String> initialCards = new List<String>();
            var rand = new Random();
            int requiredCards = 2 + (2 * players);
            for (int i = 0; i < requiredCards; i++)
            {
                var randomCardIndex = rand.Next(cards.Count);
                var randomCard = cards[randomCardIndex];
                initialCards.Add(randomCard);
                cards.RemoveAt(randomCardIndex);

            }
            //this part will have to be looked at more when more possible players are allowed.
            //it is in preparation for this, but have not looked into it deep enough. 
            userHand = new List<String>();
            dealerHand = new List<String>();

            for (int i = 0; i < players; i++)
            {
                userHand.Add(initialCards[i]);
                dealerHand.Add(initialCards[i+1]);
            }
            
        }
        public void ReplenishCards()
        {
            foreach (string card in userHand)
            {
                cards.Add(card);
            }
            foreach (string card in dealerHand)
            {
                cards.Add(card);
            }
            userHand.Clear();
            dealerHand.Clear();
        }

        public int CardTotal(List<String> player)
        {
            int cardScore = 0;
            //will also need to make logic for dealer? or use this for dealer and if it gets to 16 then stop.
            for (string card in  player)
            {
                //separate logics for if ace is in hand or not
                if (!player.Contains("Ace"))
                {

                }
            }

            return cardScore;
        }

        
    }

    enum Rank { Ace1, Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Jack = 10, Queen = 10, King = 10, Ace2 = 11, Ace = Ace1 | Ace2 }
    enum Hand { Player, Dealer}

}