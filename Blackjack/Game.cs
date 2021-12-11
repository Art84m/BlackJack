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

        public int playerTotalScore { set; get; }
        public int dealerTotalScore { set; get; }

        public bool playerIsStand { set; get; }

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

            playerTotalScore = 0;
            dealerTotalScore = 0;

            playerIsStand = false;

            dilerHand.Add(takeFromDeck());

            Card lastCard = takeFromDeck();
            lastCard.isFlippedBack = true;
            dilerHand.Add(lastCard);

            playerHand.Add(takeFromDeck());

        }

        public void playerHit()
        {
            playerHand.Add(takeFromDeck());
            playerTotalScore = calculateHand(playerHand);
        }

        public void dealerHit()
        {
            dealerTotalScore = calculateHand(dilerHand);
            while(dealerTotalScore < 17)
            {
                dilerHand.Add(takeFromDeck());
                dealerTotalScore = calculateHand(dilerHand);
                if (dealerTotalScore > playerTotalScore)
                {
                    break;
                }
            }
            openFlippedCard(dilerHand);
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

        private void openFlippedCard(HashSet<Card> hand)
        {
            foreach (Card card in hand)
            {
                card.isFlippedBack = false;
            }
        }

    }
}
