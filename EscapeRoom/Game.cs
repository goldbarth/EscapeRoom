namespace EscapeFromConsole
{
    internal class Game
    {
        private Player? _player;
        private Room? _room;
        private Exit? _exit;
        private Key? _key;

        private const char KeyChar = '¥';
        private const char PlayerChar = 'Ð';

        public readonly char FloorChar = ' ';
        public readonly char WallChar = '█';
        public readonly char ExitChar = '█';

        private bool _isWon = false;
        private bool _keyIsLooted = false;


        public void Start()
        {
            DrawPromptCommand();
            var (width, height) = GetRoomSize();

            Console.Clear();
            Init(width, height);
        }

        private static void DrawPromptCommand()
        {
            Console.WriteLine("\n\n       ******** ESCAPE ROOM ********");
            Console.WriteLine("\n\n\n   Gib die Höhe und Breite des Raumes ein.");
        }

        private (int width, int height) GetRoomSize()
        {
            var heightName = "Breite";
            var widthName = "Höhe";

            // min/max input for width           
            var width = InputCheck(30, 60, heightName);
            // min/max input for height           
            var height = InputCheck(15, 28, widthName);

            return (width, height);

        }

        private void Init(int width, int height)
        {
            _room = new Room(width, height);

            _key = new Key(_room);
            _exit = new Exit(_room);
            _player = new Player(this, _room, _key);

            _room.Draw(WallChar, FloorChar);
            _key.Draw(KeyChar);
            _exit.Draw(ExitChar);
            _player.Draw(PlayerChar);

            GameLoop();
        }

        private void GameLoop()
        {
            
            while (!_isWon)
            {
                _player?.Move(PlayerChar);
                CheckIfExitCanOpen();
                CheckIfPlayerExited();
            }
        }

        private void CheckIfPlayerExited()
        {
            if (IsPlayerAtExit() && _player.KeyIsLooted)
                ShowWinScreen();
        }

        private void ShowWinScreen()
        {
            //_canMove = false;
            var app = new Application();
            for (int beepIndex = 0; beepIndex < 3; beepIndex++)
                Console.Beep(600, 350);
            

            Console.SetCursorPosition(_player.X, _player.Y);
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
            Console.WriteLine("     Press any Key...");
            Console.ReadKey();
            Console.Clear();
            app.Outro();
        }

        #region Bools

        public bool IsPlayerAtExit()
        {
            return _player?.X + 1 == _exit.X && _player?.Y == _exit.Y;
        }

        private void CheckIfExitCanOpen()
        {
            if (_player.LootKey() && _player.KeyIsLooted == true)
                OpenExit();
        }

        private void OpenExit()
        {
            Console.Beep(450, 200);
            _player?.Position(_exit.X, _exit.Y, FloorChar);
        }

        #endregion

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
        
        private IEnumerable<bool> Infinite()  
        {  
            while (true)  
            {  
                yield return true;  
            }  
        }  
    }
}