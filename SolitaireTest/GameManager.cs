using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolitaireTest
{
    internal class GameManager
    {
        public GameManager(int numGames)
        {
            this.numGames = numGames;
            cards = new List<Card>();

            for (int i = 0; i < Card.NUM_CARDS; i++)
            {
                cards.Add(new Card(i));
            }

            random = new Random();
            Redeal();
        }

        private int numGames;
        private Solitaire fpp;
        private Solitaire lpp;
        private List<Card> cards;
        private Random random;

        private void Redeal()
        {
            List<Card> shuffledCards = cards.OrderBy(x => random.Next()).ToList();

            fpp = new FirstPossiblePlay(shuffledCards);
            lpp = new LastPossiblePlay(shuffledCards);
        }

        public string PlayGames()
        {
            int fppWins = 0;
            int lppWins = 0;

            for (int i = 0; i < numGames; i++)
            {

#if DEV
                Console.WriteLine("\nFirst Play Possible\n");
#endif
                if (fpp.Play() == true)
                {
                    fppWins++;
                }

#if DEV
                Console.WriteLine("\nLast Play Possible\n");
#endif
                if (lpp.Play() == true)
                {
                    lppWins++;
                }

                Redeal();
            }

            return "Played " + numGames + ": \n\nFirst Possible Play: " + fppWins + " wins out of " + numGames + " games (" + ((double)fppWins / (double)numGames * 100.0) + ")\nLast Possible Play: " + lppWins + " wins out of " + numGames + " games (" + ((double)lppWins / (double)numGames * 100.0) + ")";
        }
         

        //private static Random rng = new Random();
        //var shuffledcards = cards.OrderBy(a => rng.Next()).ToList();
    }
}
