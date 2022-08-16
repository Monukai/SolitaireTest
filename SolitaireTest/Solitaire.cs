using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace SolitaireTest
{
    internal class Solitaire
    {
        private const int STACK_OFFSET = 0;
        private const int PLAYED_OFFSET = Stacks.INITIAL_TOTAL_STACK_CARDS;
        private const int NUM_INITIALLY_PLAYED = 1;
        private const int PILE_OFFSET = PLAYED_OFFSET + NUM_INITIALLY_PLAYED;
        private const int NUM_IN_PILE = Card.NUM_CARDS - PILE_OFFSET;


        public Solitaire(List<Card> shuffledDeck, GameLogManager gameLogger)
        {
            this.shuffledDeck = shuffledDeck;
            roundsSinceLastPlay = 0;
            _gameLogger = gameLogger;
            Deal();
        }

        protected List<Card> shuffledDeck;
        protected Pile pile;
        protected Stacks stacks;
        protected Played played;
        protected int roundsSinceLastPlay;
        protected GameLogManager _gameLogger;

        [MemberNotNull(nameof(pile), nameof(stacks), nameof(played))]
        public void Deal()
        {
            stacks = new Stacks(shuffledDeck.GetRange(STACK_OFFSET, Stacks.INITIAL_TOTAL_STACK_CARDS));
            if (_gameLogger.IsActive())
            {
                //if (Type.Equals(this, typeof(FirstPossiblePlay)))
                //{
                    _gameLogger.Append(stacks.ToString() + "\n");
                //}
            }

            played = new Played(shuffledDeck[PLAYED_OFFSET], _gameLogger);
            if (_gameLogger.IsActive())
            {
                //if (Type.Equals(this, typeof(FirstPossiblePlay)))
                //{
                    _gameLogger.Append("Start Play: " + shuffledDeck[PLAYED_OFFSET].ToString() + "\n");
                //}
            }

            pile = new Pile(shuffledDeck.GetRange(PILE_OFFSET, NUM_IN_PILE), _gameLogger);
#if DEV
            Console.WriteLine(pile.ToString());
#endif

        }

        /// <summary>
        /// Loops through the game until a win or lose condition is hit
        /// </summary>
        /// <returns>true if the game was won, false if it was lost</returns>
        public bool Play()
        {
            while (IsGameOver() == false)
            {
                PlayRound();
            }

            return HasWon();
        }

        protected bool IsGameOver()
        {
            return played.HasWon() || HasLost();
        }

        protected bool HasWon()
        {
            return played.HasWon();
        }

        protected bool HasLost()
        {
            return roundsSinceLastPlay > 1;
        }

        protected bool HasPossiblePlay()
        {
            // check if any of the top stacks cards are playable cards
            if (played.IsPossiblePlay(stacks.GetTopCards()))
            {
                return true;
            }

            // if the stacks don't have a play, the only place left to check is current pile, if it's empty, can't play
            if (pile.HasTopCard() == false)
            {
                return false;
            }

            // if we do, check if the top card is playable
            return played.IsPossiblePlay(pile.GetTopCard());
        }

        protected void PlayAllOffStack()
        {
            if (played.IsPossiblePlay(stacks.GetTopCards()))
            {
                foreach (Card card in stacks.GetTopCards())
                {
                    if (played.IsPossiblePlay(card))
                    {
                        played.PlayCard(card, stacks);
                        roundsSinceLastPlay = 0;
                    }
                }
            }
        }

        protected void PlayOffPile()
        {
            if (pile.HasTopCard())
            {
                if (played.IsPossiblePlay(pile.GetTopCard()))
                {
                    played.PlayCard(pile.GetTopCard(), pile);
                    roundsSinceLastPlay = 0;
                }
            }
        }

        protected virtual void PlayRound()
        {
            pile.ResetPile();
            roundsSinceLastPlay++;
        }
        protected virtual void PlayPile() { }

        protected void PlayCardFromPile(Card card)
        {
            played.PlayCard(card, pile);
        }

        protected void PlayCardFromStack(Card card)
        {
            played.PlayCard(card, stacks);
        }

        // Wishlist
        // bool IsWinnable - see if the game is lost before it's begun (due to the way the stacks are dealt + suit of the first card)

    }
}
