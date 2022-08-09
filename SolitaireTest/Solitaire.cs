using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;

namespace SolitaireTest
{
    internal class Solitaire
    {
        private const int STACK_OFFSET = 0;
        private const int PLAYED_OFFSET = Stacks.INITIAL_TOTAL_STACK_CARDS;
        private const int NUM_INITIALLY_PLAYED = 1;
        private const int PILE_OFFSET = PLAYED_OFFSET + NUM_INITIALLY_PLAYED;
        private const int NUM_IN_PILE = Card.NUM_CARDS - PILE_OFFSET;


        public Solitaire(List<Card> shuffledDeck)
        {
            this.shuffledDeck = shuffledDeck;
            roundsSinceLastPlay = 0;
            Deal();
        }

        protected List<Card> shuffledDeck;
        protected Pile pile;
        protected Stacks stacks;
        protected Played played;
        protected int roundsSinceLastPlay;

        [MemberNotNull(nameof(pile), nameof(stacks), nameof(played))]
        public void Deal()
        {
            stacks = new Stacks(shuffledDeck.GetRange(STACK_OFFSET, Stacks.INITIAL_TOTAL_STACK_CARDS));

#if DEV
            Console.WriteLine(stacks.ToString());
#endif
            played = new Played(shuffledDeck[PLAYED_OFFSET]);
#if DEV
            Console.WriteLine("First in play: " + shuffledDeck[PLAYED_OFFSET].ToString());
#endif
            pile = new Pile(shuffledDeck.GetRange(PILE_OFFSET, NUM_IN_PILE));
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
