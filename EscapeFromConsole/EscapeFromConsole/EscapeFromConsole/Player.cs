
namespace EscapeFromConsole
{
    internal class Player
    {
        private int X { get; set; }
        private int Y { get; set; }

        private const char FLOOR = ' ';
        private const char WALL = '░';
        private const char EXIT = '▓';

        private bool _canMove = true;
        private bool _keyIsLooted;

        private readonly Room _room;
        private readonly Key _key;
        private readonly Exit _exit;

        public Player(Room room, Key key, Exit exit)
        {
            _room = room;
            _key = key;
            _exit = exit;
        }

        public void Draw(char playerChar)
        {
            // prevent drawing player and key char at same random start position
            if (!(X == _key.X && Y == _key.Y))
            {
                var rand = new Random();
                var randomPositionY = rand.Next(1, _room.Height);
                var randomPositionX = rand.Next(1, _room.Width);

                X = randomPositionX;
                Y = randomPositionY;

                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.SetCursorPosition(X, Y);
                Console.Write(playerChar);
            }
        }
        
        public void Move(char playerChar)
        {  
            while (_canMove)
            {               
                if (Console.KeyAvailable)
                {
                    var inputKey = Console.ReadKey().Key;;

                    // Overwrites the last position
                    Position(X, Y, FLOOR);

                    switch (inputKey)
                    {
                        case ConsoleKey.UpArrow:
                        if (1 < Y) Y--;
                        else ParallelSound();
                        break;
                        case ConsoleKey.DownArrow:
                        if (Y < _room.Height) Y++;
                        else ParallelSound();
                        break;
                        case ConsoleKey.LeftArrow:
                        if (1 < X) X--;
                        else ParallelSound();
                        break;
                        case ConsoleKey.RightArrow:
                        if (X < _room.Width) X++;
                        else ParallelSound();
                        break;
                        default:
                            // prevent printing any other letter/symbol next to the player char if pressed
                            Console.Write(FLOOR);
                        break;
                    }
                }

                Position(X, Y, playerChar);

                OpenExit();
                WinScreen();
            }
        }

        private void Position(int x, int y, char tile)
        {
            if (x > 0 && y > 0)
            {
                // prevents overwriting right wall with "floor" and draw wall on open exit
                if (x == _room.Width && !IsExit()) 
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.SetCursorPosition(_room.Width + 1, y);
                    Console.Write(WALL);
                }
                // prevents overwriting "exit" with "wall"
                else if (x == _room.Width && IsExit() && !_keyIsLooted)
                {
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.SetCursorPosition(_room.Width + 1, y);
                    Console.Write(EXIT);
                }
                
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.SetCursorPosition(x, y);
                Console.Write(tile);
            }
        }
        
        private static void ParallelSound()
        {
            var thread = new Thread(() => Console.Beep(120, 200));
            thread.Start();
        }
        
        private void WinScreen()
        {
            if (IsExit()&& _keyIsLooted)
            {
                _canMove = false;
                var app = new Application();
                for (int i = 0; i < 3; i++)
                {
                    Console.Beep(600, 350);
                }
                Console.SetCursorPosition(X, Y);
                Console.Write("...");
                Console.Write('Ð');
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.SetCursorPosition((_room.Width / 2) - 12, (_room.Height / 2) - 5);
                Console.WriteLine("YOU ESCAPED THE DARKNESS.");
                Console.SetCursorPosition((_room.Width / 2) - 12, (_room.Height / 2) - 4);
                Console.WriteLine("  NOW YOU´LL ENTER THE");
                Console.SetCursorPosition((_room.Width / 2) - 12, (_room.Height / 2) - 3);
                Console.WriteLine("        UNKNOWN.");
                Console.SetCursorPosition((_room.Width / 2) - 12, (_room.Height / 2));
                Console.WriteLine("     Press any key...");
                Console.ReadKey();
                Console.Clear();
                app.Outro();
            }
        }

        #region Bools

        private bool IsExit()
        {
            return X + 1 == _exit.X && Y == _exit.Y;
        }
        
        private void OpenExit()
        {
            if (KeyTrigger() && _keyIsLooted == true)
            {
                Console.Beep(450, 200);
                Position(_exit.X, _exit.Y, FLOOR);
            }
        }

        private bool KeyTrigger()
        {
            if (X == _key.X && Y == _key.Y && _keyIsLooted == false)
            {
                _keyIsLooted = true;
                return true;
            }
            return false;
        }

        #endregion
    }
}