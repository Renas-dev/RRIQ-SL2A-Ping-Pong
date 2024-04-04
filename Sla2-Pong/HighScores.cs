using System;

namespace Sla2Pong
{
    public class HighScores
    {
        public void UpdateScoreDisplay(int width, int leftScore, int rightScore)
        {
            // Clear previous scores to avoid overwriting issues
            Console.SetCursorPosition(2, 1);
            Console.Write(new string(' ', 5));
            Console.SetCursorPosition(width - 7, 1);
            Console.Write(new string(' ', 5));

            // Display left score at the top left of the frame
            Console.SetCursorPosition(2, 1);
            Console.Write($"L: {leftScore}");

            // Display right score at the top right of the frame
            Console.SetCursorPosition(width - 7, 1);
            Console.Write($"R: {rightScore}");
        }
    }
}
