using System;
using System.Threading;

namespace Sla2Pong
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //We set the width and height of the console window.
            int width = 90;
            int height = 30;
            // Sets console encoding to UTF-8, this allows our unicode characters to be displayed correctly.
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            // Sets console window size to the width and height of the box + 1, this ensures that the entire box is visible.
            Console.SetWindowSize(width + 1, height + 1);

            // We call the Frame method from the GameFrame class and pass the width and height as arguments.
            GameFrame.Frame(width, height);

            // We're using a thread to spawn the pong ball, this is so we can see the box and ball at the same time.
            Thread ballThread = new Thread(() => GameFrame.PongBall(width, height));
            ballThread.Start();

            Console.ReadLine(); // Pause to see the box and ball so we can see the render of the box and ball
        }
    }

    public static class GameFrame
    {
        public static void Frame(int width, int height)
        {
            // We draw the top side of the box by using a for loop to draw the horizontal line.
            Console.SetCursorPosition(0, 0);
            for (int i = 0; i < width; i++)
            {
                Console.Write("\u2501");
            }

            //We draw the bottom side of the box by using a for loop to draw the horizontal line.
            Console.SetCursorPosition(0, height);
            for (int i = 0; i < width; i++)
            {
                Console.Write("\u2501");
            }

            // For the vertical lines we do the same. We use SetCursorPosition 0 for the left side and, width for the right side.
            // This ensures that the vertical lines are drawn at the correct position.
            for (int i = 1; i < height; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write("\u2503");
                Console.SetCursorPosition(width, i);
                Console.Write("\u2503");
            }

            // We use the same practice for the corners.
            Console.SetCursorPosition(0, 0);
            Console.Write("\u250F");
            Console.SetCursorPosition(width, 0);
            Console.Write("\u2513");
            Console.SetCursorPosition(0, height);
            Console.Write("\u2517");
            Console.SetCursorPosition(width, height);
            Console.Write("\u251B");
        }

        // Method to spawn the pong ball, we pass the width and height as arguments.
        // For the spawning cordinates we divide the width and height by 2 this makes the ball spawn in the middle of the box.
        public static void PongBall(int width, int height)
        {
            int ballX = width / 2;
            int ballY = height / 2;
 
            Console.SetCursorPosition(ballX, ballY);
            Console.Write("O");
        }
    }
}
