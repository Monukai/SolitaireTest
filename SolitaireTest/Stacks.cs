using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolitaireTest
{
    /// <summary>
    /// Stacks represents the four piles dealt out at the beginning of the game each with one card face-up
    /// </summary>
    internal class Stacks
    {
        private const int NUM_SUITS = 4;
        private const int CARDS_PER_STACK = 4;

        public Stacks(Card[] cards)
        {
            System.Diagnostics.Debug.Assert(cards.Length == 16);

            stacks = new List<List<Card>>();

            InitializeStacks(cards);
        }

        private List<List<Card>> stacks;

        private void InitializeStacks(Card[] cards)
        {
            for (int i = 0; i < NUM_SUITS; i++)
            {
                stacks[i] = new List<Card>();
            }

            for (int i = 0; i < CARDS_PER_STACK; i++)
            {
                for (int j = 0; j < NUM_SUITS; j++)
                {
                    stacks[j].Insert(0, cards[(i * NUM_SUITS) + j]);
                }
            }
        }

        public List<Card> GetTopCards()
        {
            List<Card> topCards = new List<Card>();

            foreach (List<Card> cards in stacks)
            {
                if (cards.Count > 1)
                {
                    topCards.Add(cards.First());
                }
            }

            return topCards;
        }

        public void PlayCard(Card card)
        {
            List<Card> stack = stacks.First(x => x.Any(y => y.GetCardValue() == card.GetCardValue()));

            if (stack != null)
            {
                stack.Remove(stack.First());
            }
        }
    }
}
