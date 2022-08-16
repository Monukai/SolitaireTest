using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolitaireTest
{
    internal class GameManager
    {
        public GameManager(int numGames, GameLogManager gameLogger)
        {
            this.numGames = numGames;
            cards = new List<Card>();

            for (int i = 0; i < Card.NUM_CARDS; i++)
            {
                cards.Add(new Card(i));
            }

            random = new Random();
            _gameLogger = gameLogger;
            Redeal();
        }

        private int numGames;
        private Solitaire fpp;
        private Solitaire lpp;
        private List<Card> cards;
        private Random random;
        private GameLogManager _gameLogger;

        private void Redeal()
        {
            List<Card> shuffledCards = cards.OrderBy(x => random.Next()).ToList();

            fpp = new FirstPossiblePlay(shuffledCards, _gameLogger);
            lpp = new LastPossiblePlay(shuffledCards, _gameLogger);
        }

        public string PlayGames()
        {
            int fppWins = 0;
            int lppWins = 0;

            bool fppWin = false;
            bool lppWin = false;

            for (int i = 0; i < numGames; i++)
            {
                if (_gameLogger.IsActive())
                {
                    _gameLogger.Append("\n\nFirst Play Possible\n\n");
                }

                fppWin = fpp.Play();

                if (fppWin == true)
                {
                    fppWins++;
                }

                if (_gameLogger.IsActive())
                {
                    _gameLogger.Append("\n\nLast Play Possible\n\n");
                }

                lppWin = lpp.Play(); 

                if (lppWin == true)
                {
                    lppWins++;
                }

                if (_gameLogger.IsActive())
                {
                    _gameLogger.Print(lppWin != fppWin);
                }

                Redeal();
            }

            String output = ComputeOutputText(lppWins, fppWins, numGames);
            return output;            
        }

        private string ComputeOutputText(int lppWins, int fppWins, int numGames)
        {
            double lppWinPercent = ((double)lppWins / (double)numGames * 100.0);
            double fppWinPercent = ((double)fppWins / (double)numGames * 100.0);
            string lppWinPercentText = String.Format("({0:F2}% games won)", lppWinPercent);
            string fppWinPercentText = String.Format("({0:F2}% games won)", fppWinPercent);

            return "Played " + numGames + ": \n\nFirst Possible Play: " + fppWins + " wins out of " + numGames + " games " + fppWinPercentText + "\nLast Possible Play: " + lppWins + " wins out of " + numGames + " games " + lppWinPercentText;
        }
    }
}
