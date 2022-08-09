using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolitaireTest
{
    internal class Suit
    {
        public const int CARDS_PER_SUIT = 13;

        protected const int SPADES_NUM = 0;
        protected const char SPADES_CHAR = '♤';
        protected const int HEARTS_NUM = 1;
        protected const char HEARTS_CHAR = '♥';
        protected const int DIAMONDS_NUM = 2;
        protected const char DIAMONDS_CHAR = '♦';
        protected const int CLUBS_NUM = 3;
        protected const char CLUBS_CHAR = '♧';

        private static Suit? spadesInstance = null;
        public static Suit Spades
        {
            get
            {
                if (spadesInstance == null)
                {
                    spadesInstance = new Suit(SPADES_CHAR, SPADES_NUM);
                }
                return spadesInstance;
            }
        }
        private static Suit? heartsInstance = null;
        public static Suit Hearts
        {
            get
            {
                if (heartsInstance == null)
                {
                    heartsInstance = new Suit(HEARTS_CHAR, HEARTS_NUM);
                }
                return heartsInstance;
            }
        }
        private static Suit? diamondsInstance = null;
        public static Suit Diamonds
        {
            get
            {
                if (diamondsInstance == null)
                {
                    diamondsInstance = new Suit(DIAMONDS_CHAR, DIAMONDS_NUM);
                }
                return diamondsInstance;
            }
        }
        private static Suit? clubsInstance = null;
        public static Suit Clubs
        {
            get
            {
                if (clubsInstance == null)
                {
                    clubsInstance = new Suit(CLUBS_CHAR, CLUBS_NUM);
                }
                return clubsInstance;
            }
        }

        private Suit() { }
        protected Suit(char suitSymbol, int suitNum)
        {
            this.suitSymbol = suitSymbol;
            this.suitNum = suitNum;
        }

        protected char suitSymbol;
        public char Symbol { get; }
        protected int suitNum;
        public int SuitNumber { get; }

        public int GetSuitNum()
        {
            return suitNum;
        }
        public override string ToString()
        {
            return suitSymbol.ToString();
        }

        public static char GetSymbol(int suitNumber)
        {
            if (suitNumber == Spades.SuitNumber)
            {
                return Spades.suitSymbol;
            }
            else if (suitNumber == Hearts.SuitNumber)
            {
                return Hearts.suitSymbol;
            }
            else if (suitNumber == Diamonds.SuitNumber)
            {
                return Diamonds.suitSymbol;
            }
            else
            {
                return Clubs.suitSymbol;
            }
        }

        /// <summary>
        /// Return instance of corresponding suit
        /// </summary>
        /// <param name="cardNum">A card number ranging from 0 to 51</param>
        /// <returns></returns>
        public static Suit GetSuit(int cardNum)
        {
            int suitNum = cardNum / CARDS_PER_SUIT;

            if (suitNum == SPADES_NUM)
            {
                return Suit.Spades;
            }
            else if (suitNum == HEARTS_NUM)
            {
                return Suit.Hearts;
            }
            else if (suitNum == DIAMONDS_NUM)
            {
                return Suit.Diamonds;
            }
            else
            {
                return Suit.Clubs;
            }
        }
    }
}