using System;
using System.Threading;

namespace Sla2Pong
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ShowMenu();
            Console.ReadLine();

            // Gets the singleton instance of GameConfig
            GameConfig config = GameConfig.GetInstance();

            // Initialize game status and start the game loop
            GameStatus gameStatus = new GameStatus(config.Width, config.Height);

            //Concurrecny pattern, thread per object to run the game loop.
            Thread gameThread = new Thread(new ThreadStart(gameStatus.Run));
            gameThread.Start();
        }

        static void ShowMenu()
        {
            // Get console window size
            int width = Console.WindowWidth;
            int height = Console.WindowHeight;
            string greeting = "Welcome to ping pong!";

            // Center the greeting text on the screen
            Console.SetCursorPosition((width / 2) - (greeting.Length / 2), height / 2);
            Console.Write(greeting);

            string[] menuOptions = new string[] { "Play Game", "Exit Game" };
            // Draw boxes around the menu options
            for (int i = 0; i < menuOptions.Length; i++)
            {
                // Set cursor position for the top border
                Console.SetCursorPosition((width / 2) - (menuOptions[i].Length / 2), (height / 2) + i + 1);

                // Draw the top line
                Console.Write("\u250C");
                for (int j = 0; j < menuOptions[i].Length; j++)
                {
                    Console.Write("\u2500");
                }
                Console.Write("\u2510");

                // Draw the menu option text
                Console.SetCursorPosition((width / 2) - (menuOptions[i].Length / 2), (height / 2) + i + 2);
                Console.Write("\u2502" + menuOptions[i] + "\u2502");

                // Draw the bottom line
                Console.SetCursorPosition((width / 2) - (menuOptions[i].Length / 2), (height / 2) + i + 3);
                Console.Write("\u2514");
                for (int j = 0; j < menuOptions[i].Length; j++)
                {
                    Console.Write("\u2500");
                }
            }
        }
    }
}
