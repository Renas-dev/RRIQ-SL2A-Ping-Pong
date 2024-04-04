using System;
using System.Threading;

namespace Sla2Pong
{
    // GameStatus class for the game loop and the game status
    public class GameStatus
    {
        // width and height properties for the game frame and the paddles and the ball.
        private readonly int width;
        private readonly int height;
        private int leftPaddleY;
        private int rightPaddleY;
        private PongBall ball;
        private int leftScore = 0;
        private int rightScore = 0;

        // Constructor for the GameStatus class to initialize the game frame and the initial position of the paddles and the ball.
        public GameStatus(int width, int height)
        {
            this.width = width;
            this.height = height;
            leftPaddleY = height / 2;
            rightPaddleY = height / 2;
            ball = new PongBall(width, height);
        }

        // Method for running the game loop.
        public void Run()
        {
            GameFrame.Frame(width, height);

            bool gameRunning = true;
            while (gameRunning)
            {
                /* We slow down the game loop with a short delay to control the speed of the ball movement.
                 * We clear the old position of the ball and redraw it to its new position
                 * We also check for the collision of the ball with the paddles.
                */
                Thread.Sleep(100);
                ball.Clear();
                ball.Move();
                ball.CheckPaddleCollision(1, leftPaddleY, 5);
                ball.CheckPaddleCollision(width - 2, rightPaddleY, 5);

                // We check if the ball passed the left or right edge of the game frame and update the score accordingly.
                if (ball.X <= 1)
                {
                    rightScore++;
                    UpdateScoreDisplay();
                    ball.ResetBall();
                }
                else if (ball.X >= width - 1)
                {
                    leftScore++;
                    UpdateScoreDisplay();
                    ball.ResetBall();
                }
                // We check if either player has reached a score of 5 to end the game, once a player eaches 5 points, the game ends. and displays the winner.
                if (leftScore >= 5 || rightScore >= 5)
                { 
                    gameRunning = false;
                    Console.Clear();
                    string winner = leftScore >= 5 ? "Left Player Wins!" : "Right Player Wins!";
                    Console.SetCursorPosition(width / 2 - winner.Length / 2, height / 2);
                    Console.Write(winner);
                    Console.ReadKey();
                    continue;
                }
                // Moving the draw here seems to fix a weird bug where the ball would someteimes not be drawn or not be cleared properly.
                ball.Draw();
                // We check the user input for moving the paddles.
                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(true).Key;
                    switch (key)
                    {
                        case ConsoleKey.W: // Moves the left paddle up
                            if (leftPaddleY - 3 > 0)
                            {
                                GameFrame.DrawPaddle(1, leftPaddleY + 2, ' ', 5); // Clears the old position
                                leftPaddleY--;
                            }
                            break;
                        case ConsoleKey.S: // Moves the left paddle down
                            if (leftPaddleY + 3 < height)
                            {
                                GameFrame.DrawPaddle(1, leftPaddleY - 2, ' ', 5); // Clears the old position
                                leftPaddleY++;
                            }
                            break;
                        case ConsoleKey.Escape: // Exits game loop
                            gameRunning = false;
                            continue;
                    }

                    // Clears any additional buffered key presses for this frame (to prevent multiple moves)
                    // Ran into an issue where it would remember for how long i had either S or W pressed and move the paddle for that long.
                    while (Console.KeyAvailable)
                    {
                        Console.ReadKey(true);
                    }
                }

     
                // Check if the ball is above the right paddle and if there's space for the paddle to move up
                if (ball.Y > rightPaddleY + 2 && rightPaddleY + 5 < height)
                {
                    // Clear the old position of the right paddle by drawing spaces (' ') where the paddle used to be
                    GameFrame.DrawPaddle(width - 2, rightPaddleY - 2, ' ', 5);
                    rightPaddleY++; 
                }
                // Check if the ball is below the right paddle and if there's space for the paddle to move down
                else if (ball.Y < rightPaddleY - 2 && rightPaddleY - 3 > 0)
                {
                    // Clear the old position of the right paddle by drawing spaces where the paddle used to be
                    GameFrame.DrawPaddle(width - 2, rightPaddleY + 2, ' ', 5);
                    rightPaddleY--; 
                }

                // After potentially moving the right paddle, redraw both paddles in their new positions:
                GameFrame.DrawPaddle(1, leftPaddleY, '|', 5); 
                GameFrame.DrawPaddle(width - 2, rightPaddleY, '|', 5); 

                // Redraw the affected part of the frame if needed to ensure the game frame remains intact
                GameFrame.Frame(width, height, true);
                UpdateScoreDisplay();
            }
        }

        // Method to update the score display on the game frame
        private void UpdateScoreDisplay()
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
