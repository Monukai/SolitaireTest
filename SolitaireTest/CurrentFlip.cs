﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolitaireTest
{
    internal class CurrentFlip
    {
        private List<Card> cards;

        public CurrentFlip(List<Card> cards)
        {
            this.cards = cards;
        }

        public bool HasTopCard()
        {   
            return cards.Count > 0;
        }

        public Card GetTopCard()
        {
            return cards.First();
        }

        public void RemoveTopCard()
        {
            cards.Remove(cards.First());
        }
    }
}
