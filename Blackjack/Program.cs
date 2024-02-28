namespace Blackjack
{
    class Program
    {
        private int balance;
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
        
        public 

        public void ReplenishCards()
        {

        }


    }

    enum Rank { Ace1, Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Jack = 10, Queen = 10, King = 10, Ace2 = 11, Ace = Ace1 | Ace2 }

}