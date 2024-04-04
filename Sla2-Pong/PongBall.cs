using System;

namespace Sla2Pong
{
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

        /* We check the collision of the ball with the paddles.
        *  if the ball is at the same position as the paddle and the direction is towards the paddle.
        *  We change the direction of the ball
        */
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
}
