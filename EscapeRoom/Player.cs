namespace EscapeFromConsole
{
    internal class Player
    {
        public int X { get; set; }
        public int Y { get; set; }
        public bool KeyIsLooted { get; set; }

        // private const char FLOOR = ' ';
        // private const char WALL = '█';
        // private const char EXIT = '#';

        private const bool CanMove = true;

        private readonly Game _game;
        private readonly Room _room;
        private readonly Key _key;


        public Player(Game game, Room room, Key key)
        {
            _game = game;
            _room = room;
            _key = key;
        }

        public void Draw(char playerChar)
        {
            // prevent drawing player and Key char at same random start position
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
            if (Console.KeyAvailable)
            {
                var inputKey = Console.ReadKey().Key;

                // Overwrites the last position
                Position(X, Y, _game.FloorChar);

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
                        Console.Write(_game.FloorChar);
                        break;
                }
            }

            Position(X, Y, playerChar);
        }


        public bool LootKey()
        {
            if (X == _key?.X && Y == _key?.Y && KeyIsLooted == false)
            {
                KeyIsLooted = true;
                return true;
            }

            return false;
        }

        public void Position(int x, int y, char tile)
        {
            if (x > 0 && y > 0)
            {
                // prevents overwriting right wall with "floor" and draw wall on open Exit
                if (x == _room.Width && !_game.IsPlayerAtExit())
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.SetCursorPosition(_room.Width + 1, y);
                    Console.Write(_game.WallChar);
                }
                // prevents overwriting "Exit" with "wall"
                else if (x == _room.Width && _game.IsPlayerAtExit() && !KeyIsLooted)
                {
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.SetCursorPosition(_room.Width + 1, y);
                    Console.Write(_game.ExitChar);
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
    }
}