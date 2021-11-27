using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{
    public class Card
    {
        public Suit suit { set; get; }
        public Rank rank { set; get; }

        public int value { get; }

        public bool isFlippedBack { set; get; }

        public Card(Suit suit, Rank rank)
        {
            this.suit = suit;
            this.rank = rank;

            value = (int)rank;
            if(value > 10)
            {
                value = 10;
            }
        }
        

        public enum Suit
        {
            HEART, DIAMOND, CLUB, SPADE
        }

        public enum Rank
        {
            ACE=1, TWO=2, THREE=3, FOUR=4, FIVE=5, SIX=6, SEVEN=7, EIGHT=8, NINE=9, TEN=10, JACKET=11, QUEEN=12, KING=13
        }

    }
}
