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
        private const int NUM_STACKS = 4;
        private const int CARDS_PER_STACK = 4;
        public const int INITIAL_TOTAL_STACK_CARDS = NUM_STACKS * CARDS_PER_STACK;

        public Stacks(List<Card> cards)
        {
            System.Diagnostics.Debug.Assert(cards.Count == 16);

            stacks = new List<List<Card>>();

            InitializeStacks(cards);
        }

        private List<List<Card>> stacks;

        private void InitializeStacks(List<Card> cards)
        {
            for (int i = 0; i < NUM_STACKS; i++)
            {
                stacks.Add(new List<Card>());
            }

            // deal the cards into the stacks as you would physically
            // (deal one card face down into each stack, repeat this two more times, and then once more with cards face up instead)
            // the last card dealt is thus on the top, this is represented by being the First element of the list
            for (int i = 0; i < CARDS_PER_STACK; i++)
            {
                for (int j = 0; j < NUM_STACKS; j++)
                {
                    stacks[j].Insert(0, cards[(i * NUM_STACKS) + j]);
                }
            }
        }

        public List<Card> GetTopCards()
        {
            List<Card> topCards = new List<Card>();

            foreach (List<Card> cards in stacks)
            {
                if (cards.Count > 0)
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

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            foreach (List<Card> cards in stacks)
            {
                foreach (Card card in cards)
                {
                    stringBuilder.Append(card.ToString());
                    stringBuilder.Append(" ");
                }
                stringBuilder.Append("\n");
            }

            return stringBuilder.ToString();
        }
    }
}
