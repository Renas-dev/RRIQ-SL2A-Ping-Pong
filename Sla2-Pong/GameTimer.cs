using System;

namespace Sla2Pong
{
    // Class for the game timer, implementing the IDrawableComponent interface
    public class GameTimer : IDrawableComponent
    {   // Variables for the elapsed time, position and start time
        private TimeSpan elapsedTime;
        private readonly int positionX;
        private readonly int positionY;
        private readonly DateTime startTime;
        // Constructor for the GameTimer class, initializing the position and start time
        public GameTimer(int positionX, int positionY)
        {
            this.positionX = positionX;
            this.positionY = positionY;
            startTime = DateTime.Now;
            elapsedTime = TimeSpan.Zero;
        }/* Something interesting, in the past i used a for loop with a thread wait of 1000 miliseconds.
          * But using Start time and elapsed time is a better way to keep track of the time, this seems to be a better approach. since its more accurate. and less code.
          * Through polymorphism, we implement the Draw, Clear and Update method from the IDrawableComponent interface.
          */
        public void Draw()
        {
            Console.SetCursorPosition(positionX, positionY);
            Console.Write($"Time: {elapsedTime.Seconds}s");
        }       
        public void Clear()
        {
            Console.SetCursorPosition(positionX, positionY);
            Console.Write(new string(' ', 10));
        }     
        public void Update()
        {
            elapsedTime = DateTime.Now - startTime;
        }
    }
}

