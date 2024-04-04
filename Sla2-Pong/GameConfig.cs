using System;

namespace Sla2Pong
{
    // We create a singleton class for the game configuration, this class is used to set the game window size and the cursor visibility
    // The reasoning is that we only need one instance of the game configuration, so we use a singleton pattern to ensure that only one instance is created.
    public class GameConfig
    {
        private static GameConfig instance;
        public int Width { get; private set; } 
        public int Height { get; private set; }
        public bool CursorVisible { get; private set; }

        // Private constructor to prevent instantiation outside this class
        // We predefined the game window size and set the cursor visibility to false this wont change when the game is active.
        private GameConfig()
        {
            Width = 90;
            Height = 30;
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.SetWindowSize(Width + 1, Height + 1);
            CursorVisible = false;
            Console.CursorVisible = CursorVisible;
        }

        // The GameConig method provides access to the single instance of GameConfig.
        // If the instance doesn't exist, it creates a new one. Doing it like this ensures that there is only one instance of GameConfig.
        public static GameConfig GetInstance()
        {
            if (instance == null)
            {
                instance = new GameConfig();
            }
            return instance;
        }
    }
}
