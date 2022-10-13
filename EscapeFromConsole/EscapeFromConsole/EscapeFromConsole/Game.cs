namespace EscapeFromConsole
{
    static class Game
    {
        public static void Start()
        {
            Console.WriteLine("\n\n       ******** ESCAPE ROOM ********");
            Console.WriteLine("\n\n\n   Gib die Höhe und Breite des Raumes ein.");
            // min/max input for width           
            int width = InputCheck(30, 60, "Breite");
            // min/max input for height           
            int height = InputCheck(15, 28, "Höhe");

            Console.Clear();
            Initiate(width, height);
        }

        private static void Initiate(int width, int height)
        {
            Room room = new Room(width, height);
            Key key = new Key(room);
            Exit exit = new Exit(room);
            Player player = new Player(room, key, exit);

            char wallChar = '░';
            char floorchar = ' ';
            char exitChar = '▓';
            char keyChar = '¥';
            char playerChar = 'Ð';

            room.Draw(wallChar, floorchar);
            key.Draw(keyChar);
            exit.Draw(exitChar);
            player.Draw(playerChar);
            Update(player, exit, room, playerChar);
        }

        private static void Update(Player player, Exit exit, Room room, char playerChar)
        {
            while (!IsWinning(player, exit, room))
            {
                player.Move(playerChar);
                player.OpenExit();
            }
        }

        private static bool IsWinning(Player player, Exit exit, Room room)
        {
            if (player.X + 1 == exit.X && player.Y == exit.Y && player.KeyIsLooted)
            {
                GameWon(player, room);
                return true;
            }
            return false;
        }

        private static void GameWon(Player player, Room room)
        {
                for (int i = 0; i < 3; i++)
                {
                    Console.Beep(600, 350);
                }
                Console.SetCursorPosition(player.X, player.Y);
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
                Menu.Outro();
        }

        private static int InputCheck(int minInput, int maxInput, string unit)
        {
            Console.Write($"\n   {unit}({minInput}-{maxInput}): ");
            var input = Console.ReadLine();
            bool success = int.TryParse(input, out int value);
            bool valid = success && minInput <= value && value <= maxInput;
            while (!valid)
            {
                Console.WriteLine($"\n   Falsche Eingabe.\n\n   Es ist eine {unit} von {minInput} bis {maxInput} möglich.");
                input = Console.ReadLine();
                success = int.TryParse(input, out value);
                valid = success && minInput <= value && value <= maxInput;
            }

            return value;
        }
    }
}