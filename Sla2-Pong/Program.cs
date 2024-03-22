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

            // Initialize game status and start the game loop
            GameStatus gameStatus = new GameStatus(width, height);
            gameStatus.Run();
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

                // Draws the corners of the frame
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
                // Redraws only the left border affected by the paddle movement
                for (int i = 1; i < height; i++)
                {
                    Console.SetCursorPosition(0, i);
                    Console.Write("\u2503");
                }
            }
        }

        // Method for drawing the paddles, with the center coordinates and size as parameters 
        // x and y are the center coordinates of the paddle, we then clear the old position and draw the paddle symbol at the new position
        public static void DrawPaddle(int x, int y, char symbol, int size)
        {
            int startY = y - (size / 2);
            int endY = y + (size / 2);
            for (int i = startY; i <= endY; i++)
            {
                if (i > 0 && i < Console.WindowHeight)
                    Console.SetCursorPosition(x, i);
                Console.Write(symbol);

            }
        }
    }
    // PongBall class for the ball object in the game\
    public class PongBall
    { // X and Y properties for the ball's position
        public int X
        {
            get;
            private set;
        }
        public int Y
        {
            get;
            private set;
        } // widht and height properties stored of the game frame, this is used for the collision detection.
        private int directionX = 1;
        private int directionY = 1;
        private readonly int width;
        private readonly int height;

        // Constructor for the PongBall class
        public PongBall(int gameWidth, int gameHeight)
        { // Set the width and height of the game frame and the initial position of the ball.
            width = gameWidth;
            height = gameHeight;
            ResetPosition();
        }
        // Method for moving the ball
        public void Move()
        { // Move the ball by changing its X and Y coordinates
            X += directionX;
            Y += directionY;

            // Bounce off the top and bottom borders
            if (Y <= 1 || Y >= height - 1) directionY = -directionY;

            // Bounce off the left and right borders
            if (X <= 1 || X >= width - 1) directionX = -directionX;
        }
        // Method for checking the collision with the paddles
        public void CheckPaddleCollision(int paddleX, int paddleY, int paddleSize)
        {
            // collision detection with the paddles
            if ((X == paddleX + directionX) && (Y >= paddleY - paddleSize / 2) && (Y <= paddleY + paddleSize / 2))
            { // Reverse the ball's horizontal direction
                directionX = -directionX;
            }
        }
        // Method for drawing the ball
        public void Draw()
        {
            Console.SetCursorPosition(X, Y);
            Console.Write("O");
        }

        public void Clear()
        {
            Console.SetCursorPosition(X, Y);
            Console.Write(" ");
        }
        // Center the ball in the game frame
        private void ResetPosition()
        {
            X = width / 2;
            Y = height / 2;
        }
    }
    // GameStatus class for the game loop and the game status
    public class GameStatus
    { // width and height properties for the game frame and the paddles and the ball.
        private readonly int width;
        private readonly int height;
        private int leftPaddleY;
        private int rightPaddleY;
        private PongBall ball;
        // constructor for the GameStatus class to initialize the game frame and the initial position of the paddles and the ball.
        public GameStatus(int width, int height)
        {
            this.width = width;
            this.height = height;
            leftPaddleY = height / 2;
            rightPaddleY = height / 2;
            ball = new PongBall(width, height);
        }
        // Method for running the game loop
        public void Run()
        { // Draw the game frame
            GameFrame.Frame(width, height);

            bool gameRunning = true;
            while (gameRunning)
            {
                Thread.Sleep(100); // Slows down the loop for visibility
                                   // clears the old position of the ball and moves the ball to its new position and draws the ball at its new position
                ball.Clear();
                ball.Move();
                ball.Draw();

                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(true).Key;

                    // Process the key press
                    switch (key)
                    {
                        case ConsoleKey.W: // Moves left paddle up
                            if (leftPaddleY - 3 > 0)
                            {
                                GameFrame.DrawPaddle(1, leftPaddleY + 2, ' ', 5); // Clears the old position
                                leftPaddleY--;
                            }
                            break;
                        case ConsoleKey.S: // Moves left paddle down
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
                    rightPaddleY++; // Move the right paddle up by incrementing its Y position
                }
                // Check if the ball is below the right paddle and if there's space for the paddle to move down
                else if (ball.Y < rightPaddleY - 2 && rightPaddleY - 3 > 0)
                {
                    // Clear the old position of the right paddle by drawing spaces where the paddle used to be
                    GameFrame.DrawPaddle(width - 2, rightPaddleY + 2, ' ', 5);
                    rightPaddleY--; // Move the right paddle down by decrementing its Y position
                }

                // After potentially moving the right paddle, redraw both paddles in their new positions:
                GameFrame.DrawPaddle(1, leftPaddleY, '|', 5); // Draw the left paddle at its current position
                GameFrame.DrawPaddle(width - 2, rightPaddleY, '|', 5); // Draw the right paddle at its new position

                // Redraw the affected part of the frame if needed to ensure the game frame remains intact
                GameFrame.Frame(width, height, true);
            }
        }
    }
}