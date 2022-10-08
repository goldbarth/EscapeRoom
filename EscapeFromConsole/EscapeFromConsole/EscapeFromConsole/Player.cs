
namespace EscapeFromConsole
{
    internal class Player
    {
        private int X { get; set; }
        private int Y { get; set; }

        char floor = ' ';

        bool canMove = true;
        bool keyIsLooted = false;

        Room room;
        Key key;
        Exit exit;

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
            while (canMove)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKey inputKey = Console.ReadKey().Key;

                    // Overwrites the last position
                    Position(X, Y, floor);

                    switch (inputKey)
                    {
                        case ConsoleKey.UpArrow:
                        if (1 < Y)
                            Y--;
                        else
                            ParallelSound();
                        break;
                        case ConsoleKey.DownArrow:
                        if (Y < room.height)
                            Y++;
                        else
                            ParallelSound();
                        break;
                        case ConsoleKey.LeftArrow:
                        if (1 < X)
                            X--;
                        else
                            ParallelSound();
                        break;
                        case ConsoleKey.RightArrow:
                        if (X < room.width)
                            X++;
                        else
                            ParallelSound();
                        break;
                        default:
                            // prevent printing any other letter/symbol next to the player char if pressed
                            Console.Write(floor);
                        break;
                    }      
                }

                Position(X, Y, playerChar);

                OpenExit();
                WinScreen();
            }
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
        private void WinScreen()
        {
            if (X + 1 == exit.X && Y == exit.Y && keyIsLooted == true)
            {
                Menu menu = new Menu();
                canMove = false;
                for (int i = 0; i < 3; i++)
                {
                    Console.Beep(600, 350);
                }
                Console.SetCursorPosition(X, Y);
                Console.Write("...");
                Console.Write('Ð');
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.SetCursorPosition((room.width / 2) - 12, (room.height / 2) - 5);
                Console.WriteLine("YOU ESCAPED THE DARKNESS.");
                Console.SetCursorPosition((room.width / 2) - 12, (room.height / 2) - 4);
                Console.WriteLine("  NOW YOU´LL ENTER THE");
                Console.SetCursorPosition((room.width / 2) - 12, (room.height / 2) - 3);
                Console.WriteLine("        UNKNOWN.");
                Console.ReadKey();
                Console.ReadKey();
                Console.Clear();
                menu.Outro();
            }
        }

        #region Bools

        private void OpenExit()
        {
            if (KeyTrigger() && keyIsLooted == true)
            {
                Console.Beep(450, 200);
                Position(exit.X, exit.Y, floor);
            }
        }

        private bool KeyTrigger()
        {
            if (X == key.X && Y == key.Y && keyIsLooted == false)
            {
                keyIsLooted = true;
                return true;
            }
            return false;
        }

        #endregion
    }
}