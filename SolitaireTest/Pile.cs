using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;

namespace SolitaireTest
{
    /// <summary>
    /// Pile represents the cards the player holds, flipping 3 (or as many are left if less are remaining) from the top
    /// </summary>
    internal class Pile
    {
        private const string CARD_TEXT_DELIMITER = " ";
        private const int MAX_FLIP_SIZE = 3;

        public Pile(List<Card> cards, GameLogManager gameLogger)
        {
            this.cards = cards;
            currentIndex = 0;
            _gameLogger = gameLogger;
            Flip();
            //currentFlip = new CurrentFlip(cards.GetRange(0, MAX_FLIP_SIZE));
        }

        private List<Card> cards;
        private int currentIndex;
        private CurrentFlip currentFlip;
        private GameLogManager _gameLogger;

        public bool HasFlip()
        {
            return currentIndex < cards.Count;
        }

        public void ResetPile()
        {
            if (_gameLogger.IsActive())
            {
                _gameLogger.Append("\n");
            }

            currentIndex = 0;
            Flip();
        }

        public Card GetTopCard()
        {
            return currentFlip.GetTopCard();
        }

        public bool HasTopCard()
        {
            return currentFlip.HasTopCard();
        }

        [MemberNotNull(nameof(currentFlip))]
        public void Flip()
        {
            int cardsToFlip = Math.Min(cards.Count - currentIndex, MAX_FLIP_SIZE);

            List<Card> flipInRealOrder = new List<Card>();

            foreach (Card card in cards.GetRange(currentIndex, cardsToFlip))
            {
                flipInRealOrder.Add(card);
            }

            currentFlip = new CurrentFlip(flipInRealOrder, _gameLogger);
            currentIndex += MAX_FLIP_SIZE;
        }

        public void RemoveCard(Card card)
        {
            if (currentFlip.HasTopCard())
            {
                if (currentFlip.GetTopCard().GetCardValue() == card.GetCardValue())
                {
                    currentFlip.RemoveTopCard();
                }
            }

            Card cardToRemove = cards.First(x => x.GetCardValue() == card.GetCardValue());

            currentIndex = cards.IndexOf(cardToRemove) - 1;

            cards.Remove(cards.First(x => x.GetCardValue() == card.GetCardValue()));

            if (currentIndex < 0)
            {
                currentIndex = 0;
            }
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            foreach (Card card in cards)
            {
                stringBuilder.Append(card.ToString());
                stringBuilder.Append(CARD_TEXT_DELIMITER);
            }

            return stringBuilder.ToString();
        }
    }
}
