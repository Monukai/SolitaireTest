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
        public Suit Suit { get; }

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

            return cardVal.ToString() + suit;
        }
    }
}
