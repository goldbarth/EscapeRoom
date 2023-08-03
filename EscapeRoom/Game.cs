namespace EscapeFromConsole
{
    public class Game
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

        private bool _isGameWon;

        public void Start()
        {
            DrawPromptCommand();
            var room = GetRoomSize();
            Initialize(room);
        }

        private static void DrawPromptCommand()
        {
            Console.WriteLine("\n\n       ******** ESCAPE ROOM ********");
            Console.WriteLine("\n\n\n   Gib die Höhe und Breite des Raumes ein.");
        }

        private (int width, int height) GetRoomSize()
        {
            const string heightName = "Breite";
            const string widthName = "Höhe";
            
            var width = InputValidation(30, 60, heightName);
            var height = InputValidation(15, 28, widthName);
            
            Console.Clear();

            return (width, height);
        }

        private void Initialize((int width, int height) room)
        {
            _room = new Room(room.width, room.height);
            _key = new Key(_room);
            _exit = new Exit(_room);
            _player = new Player(this, _room, _key);

            _room.Draw(WallChar, FloorChar);
            _key.DrawKeyPosition(KeyChar);
            _exit.DrawExitPosition(ExitChar);
            _player.DrawStartPosition(PlayerChar);

            GameLoop();
            Console.ReadKey();
        }

        private void GameLoop()
        {
            
            while (!_isGameWon)
            {
                _player?.Move(PlayerChar);
                CheckIfExitOpens();
                CheckIfPlayerExited();
            }
        }

        private void CheckIfExitOpens()
        {
            if(HasPlayerKeyLooted())
                OpenExit();
        }

        private void CheckIfPlayerExited()
        {
            if (IsPlayerAtExit() && _player!.KeyIsLooted)
                InitializeGameEnd();
        }

        private void InitializeGameEnd()
        {
            _isGameWon = true;
            
            PlayWinSound();
            PlayAnimation();
            GameEndScreen();
            DrawWinScreen();
        }

        private static void DrawWinScreen()
        {
            new Application().Outro();
        }

        private void GameEndScreen()
        {
            // Set the text compared to the room size in the middle of the room
            Console.SetCursorPosition((_room!.Width / 2) - 12, (_room.Height / 2) - 5);
            Console.WriteLine("YOU ESCAPED THE DARKNESS.");
            Console.SetCursorPosition((_room.Width / 2) - 12, (_room.Height / 2) - 4);
            Console.WriteLine("  NOW YOU´LL ENTER THE");
            Console.SetCursorPosition((_room.Width / 2) - 12, (_room.Height / 2) - 3);
            Console.WriteLine("        UNKNOWN.");
            Console.SetCursorPosition((_room.Width / 2) - 12, (_room.Height / 2));
            Console.WriteLine("     Press any Key...");
            Console.ReadKey();
            Console.Clear();
        }

        private void PlayAnimation()
        {
            Console.SetCursorPosition(_player!.X, _player.Y);
            Console.Write("...");
            Console.Write('Ð');
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        private static void PlayWinSound()
        {
            for (int beepIndex = 0; beepIndex < 3; beepIndex++)
                Console.Beep(600, 350);
        }

        public bool IsPlayerAtExit()
        {
            return _player?.X + 1 == _exit?.X && _player?.Y == _exit?.Y;
        }

        private bool HasPlayerKeyLooted()
        {
            return _player!.LootKey() && _player.KeyIsLooted;
        }

        private void OpenExit()
        {
            Console.Beep(450, 200);
            _player?.DrawPosition(_exit!.X, _exit.Y, FloorChar);
        }

        private static int InputValidation(int minInput, int maxInput, string unit)
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