
namespace Blackjack
{
    class Program
    {
        private const int TOTAL_CARDS = 312;
        private const int MAX_HAND = 21;
        private List<String> dealerHand;
        private List<String> userHand;
        private List<String> cards;
        


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

        public List<String> DrawCards(int players)
        {
            List<String> initialCards = new List<String>(); 
            var rand = new Random();
            int requiredCards = 2 + (2 * players);
            for (int i = 0; i < requiredCards; i++)
            {
                var randomCardIndex = rand.Next(cards.Count) + 1;
                var randomCard = cards[randomCardIndex];
                initialCards.Add(randomCard);
                cards.RemoveAt(randomCardIndex);
                
            }
            return initialCards;
        }
        
        //draft 4 cards (2 for dealer and 2 for player) formatted for multiple players. and split based on how it should be on order.
        public void Deal(List<String> initialCards)
        {
            //this part will have to be looked at more when more possible players are allowed.
            //it is in preparation for this, but have not looked into it deep enough. 
            int players = ((initialCards.Count - 2)/ 2) ;
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

        }

        
    }

    enum Rank { Ace1, Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Jack = 10, Queen = 10, King = 10, Ace2 = 11, Ace = Ace1 | Ace2 }

}