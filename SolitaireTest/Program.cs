namespace SolitaireTest
{
    class Program
    {
        /// <summary>
        /// Entry point of the solution
        /// </summary>
        /// <param name="args">Valid application parameters are: -p1 -p5 -p10 -p20 -pdiff (print 1-20 games, print first game where
        /// the outcomes of the two modes differ)</param>
        static void Main(string[] args)
        {
            int gamesToPlay = GetNumGamesToPlay();

            List<SolitaireOptions> options = new List<SolitaireOptions>();

            foreach (string arg in args)
            {
#if DEV
                Console.WriteLine(arg);
#endif
                SolitaireOptions? option = default(SolitaireOptions).GetMatchingSolitaireOption(arg.Trim());
                if (option != null)
                {
                    options.Add((SolitaireOptions)option);
                }
            }

            GameLogManager gameLogManager = new GameLogManager(options);

            GameManager gameManager = new GameManager(gamesToPlay, gameLogManager);

            Console.WriteLine("\n");
            Console.WriteLine(gameManager.PlayGames());
            Console.ReadLine();
        }

        private const string INPUT_ERROR_MSG = "Please input a valid positive integer value: ";

        private static int GetNumGamesToPlay()
        {
            int numGames = 0;
            string? input = String.Empty;
            bool succeeded = false;

            Console.Write("Input number of games to simulate: ");

            do
            {
                input = Console.ReadLine();

                if (String.IsNullOrEmpty(input) == false)
                {
                    succeeded = Int32.TryParse(input, out numGames);

                    if (succeeded == false)
                    { 
                        Console.Write(INPUT_ERROR_MSG);
                    }
                }
            } while (succeeded == false);

            return numGames;
        }
    }

    public enum SolitaireOptions
    {
        PrintOne,
        PrintFive,
        PrintTen,
        PrintTwenty,
        PrintDiff        
    }

    static class SolitaireOptionsMethods
    {
        public static int GetPrintNumber(this SolitaireOptions solitaireOptions)
        {
            switch (solitaireOptions)
            {
                case SolitaireOptions.PrintOne:
                    return 1;
                case SolitaireOptions.PrintFive:
                    return 5;
                case SolitaireOptions.PrintTen:
                    return 10;
                case SolitaireOptions.PrintTwenty:
                    return 20;
                case SolitaireOptions.PrintDiff:
                    return 1;
                default:
                    return 0;
            }
        }

        public static bool IsDiffPrint(this SolitaireOptions solitaireOptions)
        {
            switch (solitaireOptions)
            {
                case SolitaireOptions.PrintDiff:
                    return true;
                case SolitaireOptions.PrintOne:
                case SolitaireOptions.PrintFive:
                case SolitaireOptions.PrintTen:
                case SolitaireOptions.PrintTwenty:
                default:
                    return false;
            }
        }

        public static SolitaireOptions? GetMatchingSolitaireOption(this SolitaireOptions solitaireOptions, string parameter)
        {
            if (String.Equals("-p1", parameter) == true)
            {
                return SolitaireOptions.PrintOne;
            }

            if (String.Equals("-p5", parameter) == true)
            {
                return SolitaireOptions.PrintFive;
            }

            if (String.Equals("-p10", parameter) == true)
            {
                return SolitaireOptions.PrintTen;
            }

            if (String.Equals("-p20", parameter) == true)
            {
                return SolitaireOptions.PrintTwenty;
            }

            if (String.Equals("-pdiff", parameter) == true)
            {
                return SolitaireOptions.PrintDiff;
            }

            return null;
        }
    }

}