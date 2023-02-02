namespace EscapeFromConsole
{
    internal class Game
    {
        public static void Start()
        {
            Console.WriteLine("\n\n       ******** ESCAPE ROOM ********");
            Console.WriteLine("\n\n\n   Gib die Höhe und Breite des Raumes ein.");
            // min/max input for width           
            var width = InputCheck(30, 60, "Breite");
            // min/max input for height           
            var height = InputCheck(15, 28, "Höhe");

            Console.Clear();
            Init(width, height);
        }

        private static void Init(int width, int height)
        {
            var room = new Room(width, height);
            var key = new Key(room);
            var exit = new Exit(room);
            var player = new Player(room, key, exit);

            const char wallChar = '░';
            const char floorChar = ' ';
            const char exitChar = '▓';
            const char keyChar = '¥';
            const char playerChar = 'Ð'; 

            room.Draw(wallChar, floorChar);
            key.Draw(keyChar);
            exit.Draw(exitChar);
            player.Draw(playerChar);
            player.Move(playerChar);
            Console.ReadKey();
        }

        private static int InputCheck(int minInput, int maxInput, string unit)
        {
            Console.Write($"\n   {unit}({minInput}-{maxInput}): ");
            var input = Console.ReadLine();
            var success = int.TryParse(input, out var value);
            var valid = success && minInput <= value && value <= maxInput;
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