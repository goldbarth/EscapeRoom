
namespace EscapeFromConsole
{
    internal class Game
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
            Init(width, height);
        }

        private static void Init(int width, int height)
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
            player.Move(playerChar);
            Console.ReadKey();
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