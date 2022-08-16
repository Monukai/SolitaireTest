using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

namespace SolitaireTest
{
    internal class GameLogManager
    {
        public GameLogManager(List<SolitaireOptions> options)
        {
            _printOptionManagers = new List<PrintOptionManager>();

            foreach (SolitaireOptions option in options)
            {
                _printOptionManagers.Add(new PrintOptionManager(option));
            }

            _stringBuilder = new StringBuilder();
            _filePath = AppDomain.CurrentDomain.BaseDirectory + "output.txt";

            File.Delete(_filePath);
        }

        private List<PrintOptionManager> _printOptionManagers;
        private StringBuilder _stringBuilder;
        private String _filePath;

        public void Append(String input)
        {
            _stringBuilder.Append(input);
        }

        public void Print(bool isDiff)
        {
            if (IsActive() == false)
            {
                return;
            }
            
            foreach (PrintOptionManager manager in _printOptionManagers)
            {
                if (manager.IsDiffPrint() == false)
                {
                    manager.PrintGame(_stringBuilder, _filePath);
                }
                else
                {
                    if (isDiff == true)
                    {
                        manager.PrintGame(_stringBuilder, _filePath);
                    }
                }                
            }

            if (_printOptionManagers.Any(x => x.IsComplete() == true))
            {
                List<PrintOptionManager> toRemove = _printOptionManagers.Where(x => x.IsComplete() == true).ToList<PrintOptionManager>();

                foreach (PrintOptionManager manager in toRemove)
                {
                    _printOptionManagers.Remove(manager);
                }
            }

            _stringBuilder.Clear();
        }

        /// <summary>
        /// Are there any more games left to log to a file?
        /// </summary>
        /// <returns>A bool value representing whether there are games left to log</returns>
        public bool IsActive()
        {
            return _printOptionManagers.Any();
        }

    }

    internal class PrintOptionManager
    {
        private const string DIFF_MARKER_TEXT = "~============================= DIFF GAME SET ============================~";

        public PrintOptionManager(SolitaireOptions option)
        {
            _printOption = option;
            _gamesToPrint = option.GetPrintNumber();
            _gamesPrinted = 0;
        }

        private int _gamesToPrint;
        private int _gamesPrinted;
        private SolitaireOptions _printOption;

        public void PrintGame(StringBuilder gameToPrint, string filePath)
        {
            _gamesPrinted++;

            string gameNumText = "Game #" + _gamesPrinted + "\n\n";
            if (_printOption.IsDiffPrint())
            {
                gameToPrint.Insert(0, DIFF_MARKER_TEXT + "\n" + gameNumText);
                gameToPrint.Append("\n\n" + DIFF_MARKER_TEXT);
            }
            else
            {
                gameToPrint.Insert(0, gameNumText);
            }

            gameToPrint.Append("\n\n");

            using (StreamWriter streamWriter = new StreamWriter(filePath, append: true))
            {
                streamWriter.Write(gameToPrint);
            }
        }

        public bool IsComplete()
        {
            return _gamesToPrint == _gamesPrinted;
        }

        public bool IsDiffPrint()
        {
            return _printOption.IsDiffPrint();
        }
    }
}
