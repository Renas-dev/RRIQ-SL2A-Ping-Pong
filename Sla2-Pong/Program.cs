using System;
using System.Threading;

namespace Sla2Pong
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Basic game setup: width, height, encoding, and cursor visibility
            int width = 90;
            int height = 30;
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.SetWindowSize(width + 1, height + 1);
            Console.CursorVisible = false;

            // Initial position of the left paddle
            int leftPaddleY = height / 2;

            // Draw the initial game frame and paddle
            GameFrame.Frame(width, height); // Draw the frame
            GameFrame.DrawPaddle(1, leftPaddleY, '|', 5); // Draw the paddle

            // Game loop for handling paddle movement
            bool gameRunning = true;
            while (gameRunning)
            {
                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(true).Key;
                    switch (key)
                    {
                        case ConsoleKey.W: // Move paddle up
                            if (leftPaddleY - 2 > 1)
                            {
                                GameFrame.DrawPaddle(1, leftPaddleY + 2, ' ', 1); // Clear the bottom row of the paddle
                                leftPaddleY--;
                            }
                            break;
                        case ConsoleKey.S: // Move paddle down
                            if (leftPaddleY + 2 < height - 1)
                            {
                                GameFrame.DrawPaddle(1, leftPaddleY - 2, ' ', 1); // Clear the top row of the paddle
                                leftPaddleY++;
                            }
                            break;
                        case ConsoleKey.Escape: // Exit game loop
                            gameRunning = false;
                            continue;
                    }

                    // Redraw the paddle at its new position
                    GameFrame.DrawPaddle(1, leftPaddleY, '|', 5);

                    // Redraw the affected part of the frame if needed
                    GameFrame.Frame(width, height, true);
                }
            }
        }
    }

    public static class GameFrame
    {
        // Method for drawing the game frame
        public static void Frame(int width, int height, bool clearOnly = false)
        {
            if (!clearOnly)
            {
                Console.Clear(); // Clear the console when drawing the full frame

                // Drawing the top and bottom borders
                for (int i = 0; i <= width; i++)
                {
                    Console.SetCursorPosition(i, 0);
                    Console.Write("\u2501");
                    Console.SetCursorPosition(i, height);
                    Console.Write("\u2501");
                }

                // Drawing the left and right borders
                for (int i = 1; i < height; i++)
                {
                    Console.SetCursorPosition(0, i);
                    Console.Write("\u2503");
                    Console.SetCursorPosition(width, i);
                    Console.Write("\u2503");
                }

                // Drawing the corners of the frame
                Console.SetCursorPosition(0, 0);
                Console.Write("\u250F");
                Console.SetCursorPosition(width, 0);
                Console.Write("\u2513");
                Console.SetCursorPosition(0, height);
                Console.Write("\u2517");
                Console.SetCursorPosition(width, height);
                Console.Write("\u251B");
            }
            else
            {
                // Redraw only the left border affected by the paddle movement
                for (int i = 1; i < height; i++)
                {
                    Console.SetCursorPosition(0, i);
                    Console.Write("\u2503");
                }
            }
        }

        // Method for drawing and clearing the paddle
        public static void DrawPaddle(int x, int y, char symbol, int size)
        {
            int startY = y - (size / 2);
            int endY = y + (size / 2);

            for (int i = startY; i <= endY; i++)
            {
                if (i > 0 && i < Console.WindowHeight) // Ensure within console window
                {
                    Console.SetCursorPosition(x, i);
                    Console.Write(symbol);
                }
            }
        }
    }
}
