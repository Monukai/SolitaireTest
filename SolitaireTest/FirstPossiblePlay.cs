using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolitaireTest
{
    /// <summary>
    /// FirstPossiblePlay is the solitaire mode played most intuitively, cards are played as soon as they are legally allowed to be played
    /// </summary>
    internal class FirstPossiblePlay : Solitaire
    {
        public FirstPossiblePlay(List<Card> shuffledDeck) : base(shuffledDeck) { }

        protected override void PlayRound()
        {
            PlayPile();
            do
            {
                if (pile.HasFlip())
                {
                    pile.Flip();
                }
                PlayPile();
            } while (pile.HasFlip());

            base.PlayRound();
        }

        protected override void PlayPile()
        {
            // cards on the stacks can effect whether cards on the current flip are playable and vice versa so use a while loop until neither have playable cards
            while (HasPossiblePlay())
            {
                PlayAllOffStack();
                PlayOffPile();
                roundsSinceLastPlay = 0;
            }
        }
    }
}
