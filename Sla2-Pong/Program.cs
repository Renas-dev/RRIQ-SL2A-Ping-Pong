using System;
using System.Threading;

namespace Sla2Pong
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ShowMainMenu();
        }
        static void ShowMainMenu()
        {
            Console.WriteLine("Main Menu:");
            Console.WriteLine("1. Start Game");
            Console.WriteLine("2. Exit");
            Console.WriteLine("When game is started press 'q' to exit");
            Console.Write("Select an option: ");

            string userInput = Console.ReadLine();

            switch (userInput)
            {
                case "1":
                    // Gets the singleton instance of GameConfig
                    GameConfig config = GameConfig.GetInstance();

                    // Initialize game status and start the game loop
                    GameController gameController = new GameController(config.Width, config.Height);

                    //Concurrecny pattern, thread per object to run the game loop.
                    Thread gameThread = new Thread(new ThreadStart(gameController.Run));
                    gameThread.Start();
                    break;
                case "2":
                    ExitGame();
                    break;
                default:
                    Console.WriteLine("Invalid selection, please try again.");
                    ShowMainMenu();
                    break;
            }
            Console.Clear();
        }

        static void StartGame()
        {
            // Initialize or load your game here
            Console.WriteLine("Loading game...");
            Console.Clear();
            // Example of game initialization/loading
            InitializeGame();

            // Enter the game loop
            GameLoop();
        }

        static void InitializeGame()
        {
            // Initialize your game's state, load resources, etc.
            //Console.WriteLine("Game initialized.");
        }

        static void GameLoop()
        {
            // Your game's main loop
            bool gameRunning = true;
            while (gameRunning)
            {
                // Update game state, handle user input, etc.

                // Example of exiting the game loop
                //Console.WriteLine("Press 'q' to quit.");
                if (Console.ReadLine() == "q")
                {
                    gameRunning = false;
                }
            }

            // When the loop ends, return to the main menu
            ShowMainMenu();
        }

        static void ExitGame()
        {
            Console.WriteLine("Exiting game...");
            Environment.Exit(0);
        }
    }
}
