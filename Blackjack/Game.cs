using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{
    public class Game
    {
        private HashSet<Card> deck = new HashSet<Card>();
        public HashSet<Card> dilerHand { set; get; }
        public HashSet<Card> playerHand { set; get; }

        private Random random = new Random();

        public Game()
        {
            dilerHand = new HashSet<Card>();
            playerHand = new HashSet<Card>();

            foreach(Card.Suit suit in Enum.GetValues(typeof(Card.Suit)))
            {
                foreach (Card.Rank rank in Enum.GetValues(typeof(Card.Rank)))
                {
                    deck.Add(new Card(suit, rank));
                }
            }
        }

        public void nextRound()
        {
            dilerHand.Clear();
            playerHand.Clear();

            dilerHand.Add(takeFromDeck());

            Card lastCard = takeFromDeck();
            lastCard.isFlippedBack = true;
            dilerHand.Add(lastCard);

            playerHand.Add(takeFromDeck());

        }

        private Card takeFromDeck()
        {
            int cardIndex = random.Next(deck.Count); // [0 .. deck.Count - 1]
            Card card = deck.ElementAt(cardIndex);
            deck.Remove(card);
            return card;
        }

        private int calculateHand(HashSet<Card> hand)
        {
            int total = 0;
            foreach(Card card in hand)
            {
                total = total + card.value;
            }
            return total;
        }

    }
}
