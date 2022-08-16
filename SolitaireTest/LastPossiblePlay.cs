using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolitaireTest
{
    /// <summary>
    /// LastPossiblePlay represents the less inuitive style of play where you go through the entire pile and only play the last possible card that is a legal play
    /// Always check the stacks for possible plays after a card is played
    /// </summary>
    internal class LastPossiblePlay : Solitaire
    {
        public LastPossiblePlay(List<Card> shuffledDeck, GameLogManager gameLogger) : base(shuffledDeck, gameLogger) { }

        private Card? lastPossiblePlay;

        protected override void PlayRound()
        {
            while (played.IsPossiblePlay(stacks.GetTopCards()))
            {
                PlayAllOffStack();
            }

            CheckPossiblePlayOffPile();

            do
            {
                if (pile.HasFlip())
                {
                    pile.Flip();
                }
                CheckPossiblePlayOffPile();
            } while (pile.HasFlip());

            AttemptPlayOffPile();
            base.PlayRound();
        }

        private void AttemptPlayOffPile()
        {
            if (lastPossiblePlay != null)
            {
                played.PlayCard(lastPossiblePlay, pile);
                roundsSinceLastPlay = 0;
                lastPossiblePlay = null;
            }
        }

        private void CheckPossiblePlayOffPile()
        {
            if (pile.HasTopCard())
            {
                if (played.IsPossiblePlay(pile.GetTopCard()))
                {
                    lastPossiblePlay = pile.GetTopCard();
                }
            }
        }
    }
}
