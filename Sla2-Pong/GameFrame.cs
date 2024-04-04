using System;

namespace Sla2Pong
{
    public static class GameFrame
    {
        // Method for drawing the game frame
        public static void Frame(int width, int height, bool clearOnly = false)
        {   // Clear the console when drawing the full frame
            if (!clearOnly)
            {
                Console.Clear();
                // Draws the top and bottom borders
                for (int i = 0; i <= width; i++)
                {
                    Console.SetCursorPosition(i, 0);
                    Console.Write("\u2501"); 
                    Console.SetCursorPosition(i, height);
                    Console.Write("\u2501"); 
                }
                // Draws the left and right borders
                for (int i = 1; i < height; i++)
                {
                    Console.SetCursorPosition(0, i);
                    Console.Write("\u2503"); 
                    Console.SetCursorPosition(width, i);
                    Console.Write("\u2503");
                }
                // Draws the corners of the frame based on the width and height.
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
                // Redraws only the left border if it gets affected by the paddle movement
                for (int i = 1; i < height; i++)
                {
                    Console.SetCursorPosition(0, i);
                    Console.Write("\u2503"); 
                }
            }
        }
    }
}
