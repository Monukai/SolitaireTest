using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolitaireTest
{
    internal class Card
    {
        public const int NUM_CARDS = 52;

        private int cardNum;
        private Suit cardSuit;
        public Suit Suit { get { return cardSuit; } }

        public Card(int cardNum)
        {
            this.cardNum = cardNum;
            cardSuit = Suit.GetSuit(cardNum);
        }

        public bool IsEqual(Card card)
        {
            return card.GetCardValue() == cardNum;
        }

        public int GetCardValue()
        {
            return cardNum;
        }
        public int GetCardFaceValue()
        {
            return cardNum % Suit.CARDS_PER_SUIT;
        }

        public override string ToString()
        {
            int cardVal = GetCardFaceValue();
            int suitNum = cardNum / Suit.CARDS_PER_SUIT;

            char suit = Suit.GetSymbol(suitNum);
            string val = String.Empty;

            if (cardVal == 0)
            {
                val = "A";
            }
            else if (cardVal < 10)
            {
                val = (cardVal + 1).ToString();
            }
            else if (cardVal == 10)
            {
                val = "J";
            }
            else if (cardVal == 11)
            {
                val = "Q";
            }
            else 
            {
                val = "K";
            }

            return val + suit;
        }
    }
}
