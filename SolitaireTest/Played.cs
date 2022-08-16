using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolitaireTest
{
    internal class Played
    {
        private const int MAX_PLAYED_STACKS = 4;

        /// <summary>
        /// Played represents the cards you put into play, you start the game with one card (firstCard) in play
        /// </summary>
        /// <param name="firstCard">The first card put into play when the game is dealt</param>
        public Played(Card firstCard, GameLogManager gameLogger)
        {
            cards = new List<List<Card>>();
            cards.Add(new List<Card>() { firstCard });
            suitsInPlay = new List<Suit>() { firstCard.Suit };
            _gameLogger = gameLogger;
        }

        private GameLogManager _gameLogger;
        private List<List<Card>> cards;
        private List<Suit> suitsInPlay;

        public bool IsPossiblePlay(List<Card> cards)
        {
            foreach (Card card in cards)
            {
                if (IsPossiblePlay(card))
                {
                    return true;
                }
            }

            return false;
        }
        public bool IsPossiblePlay(Card? card)
        {
            if (card == null)
            {
                return false;
            }

            // If it's the first suit played, we can play any card from this suit at any time
            if (suitsInPlay.First().SuitNumber == card.Suit.SuitNumber)
            {
                return true;
            }

            // if it's the 'entry' card (has the same face value as the first card played in the game), it can be played at any time
            if (cards.First().First().GetCardFaceValue() == card.GetCardFaceValue())
            {
                return true;
            }

            // if the suit isn't in play, and this isn't the entry card, we cannot play it
            if (suitsInPlay.Contains(card.Suit) == false)
            {
                return false;
            }

            // else we need to check if a card with the same face value has been played in the 'controlling' (preceding) 
            int controllingSuitIndex = GetControllingSuitIndex(card);
            return cards[controllingSuitIndex].Any(x => x.GetCardFaceValue() == card.GetCardFaceValue());
        }

        public void PlayCard(Card card, Pile pile)
        {
            PlayCard(card);
            pile.RemoveCard(card);
        }
        public void PlayCard(Card card, Stacks stacks)
        {
            PlayCard(card);
            stacks.PlayCard(card);
        }

        private void PlayCard(Card card)
        {
            if (suitsInPlay.Contains(card.Suit))
            {
                cards[suitsInPlay.IndexOf(card.Suit)].Add(card);
            }
            else
            {
                suitsInPlay.Add(card.Suit);
                cards.Add(new List<Card>() { card });
            }

            if (_gameLogger.IsActive())
            {
                _gameLogger.Append("{PLAY " + card.ToString() + "} ");
            }
        }

        public bool HasWon()
        {
            return cards.SelectMany(x => x).Count() == Card.NUM_CARDS;
        }

        /// <summary>
        /// Return the index of the card's suit's controlling suit (the preceding suit)
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        private int GetControllingSuitIndex(Card card)
        {
            return suitsInPlay.IndexOf(card.Suit) - 1;
        }

        private bool IsSuitInPlay(Suit suit)
        {
            return suitsInPlay.Contains(suit);
        }
    }
}
