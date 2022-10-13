namespace EscapeFromConsole
{
    class Player
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        public bool KeyIsLooted { get; private set; }

        readonly char floor = ' ';

        readonly Room room;
        readonly Key key;
        readonly Exit exit;

        public Player(Room room, Key key, Exit exit)
        {
            this.room = room;
            this.key = key;
            this.exit = exit;
        }

        public void Draw(char playerChar)
        {
            // prevent drawing player and key char at same random start position
            if (!(X == key.X && Y == key.Y))
            {
                Random rand = new Random();
                int randomPositionY = rand.Next(1, room.height);
                int randomPositionX = rand.Next(1, room.width);

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
                ConsoleKey inputKey = Console.ReadKey().Key;

                Position(X, Y, floor); // Overwrites the last player position

                switch (inputKey)
                {
                    case ConsoleKey.UpArrow:
                    if (1 < Y) Y--;
                    else
                        ParallelSound();
                    break;
                    case ConsoleKey.DownArrow:
                    if (Y < room.height) Y++;
                    else
                        ParallelSound();
                    break;
                    case ConsoleKey.LeftArrow:
                    if (1 < X) X--;
                    else
                        ParallelSound();
                    break;
                    case ConsoleKey.RightArrow:
                    if (X < room.width) X++;
                    else
                        ParallelSound();
                    break;
                    default:
                    // prevent printing any other letter/symbol next to the player char if pressed
                    Console.Write(floor);
                    break;
                }
            }

            Position(X, Y, playerChar); // draws the new player position
        }

        private static void Position(int x, int y, char tile)
        {
            if (x > 0 && y > 0)
            {
                Console.SetCursorPosition(x, y);
                Console.WriteLine(tile);
            }
        }

        private static void ParallelSound()
        {
            Thread thread = new Thread(() => Console.Beep(120, 200));
            thread.Start();
        }
        

        #region Bools

        public bool OpenExit()
        {
            if (KeyTrigger() && KeyIsLooted == true)
            {
                Console.Beep(450, 200);
                Position(exit.X, exit.Y, floor);
                return true;
            }
            return false;
        }

        private bool KeyTrigger()
        {
            if (X == key.X && Y == key.Y && KeyIsLooted == false)
            {
                KeyIsLooted = true;
                return true;
            }
            return false;
        }

        #endregion
    }
}