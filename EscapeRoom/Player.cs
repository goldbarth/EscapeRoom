namespace EscapeFromConsole
{
    public class Player
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public bool KeyIsLooted { get; private set; }

        private readonly Game _game;
        private readonly Room _room;
        private readonly Key _key;

        public Player(Game game, Room room, Key key)
        {
            _game = game;
            _room = room;
            _key = key;
        }

        public void DrawStartPosition(char playerChar)
        {
            if (X == _key.X && Y == _key.Y) return;
            GetRandomPosition();
            SetPosition(playerChar);
        }

        public void Move(char playerChar)
        {
            if (Console.KeyAvailable)
            {
                var inputKey = Console.ReadKey().Key;

                // Overwrites the last position
                DrawPosition(X, Y, _game.FloorChar);

                switch (inputKey)
                {
                    case ConsoleKey.UpArrow:
                        if (1 < Y) Y--;
                        else PlaySound();
                        break;
                    case ConsoleKey.DownArrow:
                        if (Y < _room.Height) Y++;
                        else PlaySound();
                        break;
                    case ConsoleKey.LeftArrow:
                        if (1 < X) X--;
                        else PlaySound();
                        break;
                    case ConsoleKey.RightArrow:
                        if (X < _room.Width) X++;
                        else PlaySound();
                        break;
                    default:
                        // Prevent printing any other letter/symbol next to the player char if pressed
                        Console.Write(_game.FloorChar);
                        break;
                }
            }

            // Updates the new position
            DrawPosition(X, Y, playerChar);
        }
        
        private void GetRandomPosition()
        {
            var rand = new Random();
            var randomPositionY = rand.Next(1, _room.Height);
            var randomPositionX = rand.Next(1, _room.Width);

            X = randomPositionX;
            Y = randomPositionY;
        }

        private void SetPosition(char playerChar)
        {
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.SetCursorPosition(X, Y);
            Console.Write(playerChar);
        }

        public void DrawPosition(int x, int y, char tile)
        {
            if (x <= 0 || y <= 0) return;
            
            RepaintEnvironment(x, y);
            DrawNewPosition(x, y, tile);
        }

        private static void DrawNewPosition(int x, int y, char tile)
        {
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.SetCursorPosition(x, y);
            Console.Write(tile);
        }

        private void RepaintEnvironment(int x, int y)
        {
            RepaintWall(x, y);
            RepaintExit(x, y);
        }

        private void RepaintExit(int x, int y)
        {
            // prevents overwriting "Exit" with "wall"
            if (x != _room.Width || !_game.IsPlayerAtExit() || KeyIsLooted) return;
            
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.SetCursorPosition(_room.Width + 1, y);
            Console.Write(_game.ExitChar);
        }

        private void RepaintWall(int x, int y)
        {
            // prevents overwriting right wall with "floor" and draw wall on open Exit
            if (x != _room.Width || _game.IsPlayerAtExit()) return;
            
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.SetCursorPosition(_room.Width + 1, y);
            Console.Write(_game.WallChar);
        }
        
        public bool LootKey()
        {
            if (X == _key.X && Y == _key.Y && KeyIsLooted == false)
            {
                KeyIsLooted = true;
                return true;
            }

            return false;
        }

        private static void PlaySound()
        {
            new Thread(() => Console.Beep(120, 200)).Start();
        }
    }
}