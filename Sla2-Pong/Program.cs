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
            gameStatus.Run();
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
    // We create a singleton class for the game configuration, this class is used to set the game window size and the cursor visibility
    // The reasoning is that we only need one instance of the game configuration, so we use a singleton pattern to ensure that only one instance is created.
    public class GameConfig
    {
        private static GameConfig instance;
        public int Width { get; private set; }
        public int Height { get; private set; }
        public bool CursorVisible { get; private set; }

        // Private constructor to prevent instantiation outside this class
        private GameConfig()
        {
            Width = 90;
            Height = 30;
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.SetWindowSize(Width + 1, Height + 1);
            CursorVisible = false;
            Console.CursorVisible = CursorVisible;
        }

        // Public method to get the instance
        public static GameConfig GetInstance()
        {
            if (instance == null)
            {
                instance = new GameConfig();
            }
            return instance;
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

    // PongBall class for the ball object in the game
    public class PongBall
    { // X and Y properties for the ball's position and directionX and directionY for the ball's movement direction
        public int X { get; private set; }
        public int Y { get; private set; }
        private int directionX = 1;
        private int directionY = 1;
        private readonly int width;
        private readonly int height;

        // Constructor for the PongBall class to initialize the game width and height and reset the ball position
        public PongBall(int gameWidth, int gameHeight)
        {
            width = gameWidth;
            height = gameHeight;
            ResetPosition();
        }

        // Method for moving the ball, we increment the X and Y coordinates by the direction values
        public void Move()
        {
            X += directionX;
            Y += directionY;

            if (Y <= 1 || Y >= height - 1)
            {
                directionY = -directionY;
            }
        }

        // We check the collision of the ball with the paddles, if the ball is at the same position as the paddle and the direction is towards the paddle.
        // We change the direction of the ball
        public void CheckPaddleCollision(int paddleX, int paddleY, int paddleSize)
        {
            if (directionX < 0 && X == paddleX + 1 && Y >= paddleY - paddleSize / 2 && Y <= paddleY + paddleSize / 2)
            {
                directionX = -directionX; 
            }
            if (directionX > 0 && X == paddleX - 1 && Y >= paddleY - paddleSize / 2 && Y <= paddleY + paddleSize / 2)
            {
                directionX = -directionX; 
            }
        }

        // We draw the ball at its current position
        public void Draw()
        {
            Console.SetCursorPosition(X, Y);
            Console.Write("O");
        }

        // We clear the ball at its current position
        public void Clear()
        {
            Console.SetCursorPosition(X, Y);
            Console.Write(" ");
        }

        // We reset the ball position to the center of the game frame
        private void ResetPosition()
        {
            X = width / 2;
            Y = height / 2;
        }

        // We reset the ball position and direction and clear the ball's current position.
        public void ResetBall()
        {
            ResetPosition();
            directionX = -directionX;
            Clear();
        }
    }
    // GameStatus class for the game loop and the game status
    public class GameStatus
    {
        // width and height properties for the game frame and the paddles and the ball.
        private readonly int width;
        private readonly int height;
        private int leftPaddleY;
        private int rightPaddleY;
        private PongBall ball;
        private int leftScore = 0; // Initialized left score
        private int rightScore = 0; // Initialized right score

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
                // We slow down the game loop with a short delay to control the speed of the ball movement.
                // We clear the old position of the ball and redraw it to its new position
                // We also check for the collision of the ball with the paddles.
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
                // We check if either player has reached a score of 5 to end the game.
                if (leftScore >= 5 || rightScore >= 5)
                { // We clear the game frame and display the winner
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
                UpdateScoreDisplay();
            }
        }

        // Method to update the score display on the game frame
        private void UpdateScoreDisplay()
        {
            // Clear previous scores to avoid overwriting issues
            Console.SetCursorPosition(2, 1);
            Console.Write(new string(' ', 5)); // Clear area for left score
            Console.SetCursorPosition(width - 7, 1);
            Console.Write(new string(' ', 5)); // Clear area for right score

            // Display left score at the top left of the frame
            Console.SetCursorPosition(2, 1);
            Console.Write($"L: {leftScore}");

            // Display right score at the top right of the frame
            Console.SetCursorPosition(width - 7, 1);
            Console.Write($"R: {rightScore}");
        }
    }



}
