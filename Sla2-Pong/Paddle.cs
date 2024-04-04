using System;

namespace Sla2Pong
{
    /* Method for drawing the paddles, with the center coordinates and size as parameters
    *  x and y are the center coordinates of the paddle, we then clear the old position and draw the paddle symbol at the new position
    */
    public class Paddle
    {
        public static void DrawPaddle(int x, int y, char symbol, int size)
        {
            int startY = y - (size / 2);
            int endY = y + (size / 2);
            for (int i = startY; i <= endY; i++)
            {
                if (i > 0 && i < Console.WindowHeight)
                {
                    Console.SetCursorPosition(x, i);
                    Console.Write(symbol);
                }
            }
        }
    }
}