namespace SolitaireTest
{
    class Program
    {
        static void Main(string[] args)
        {
            GameManager gameManager = new GameManager(1000000);

            Console.WriteLine(gameManager.PlayGames());
        }
    }
}