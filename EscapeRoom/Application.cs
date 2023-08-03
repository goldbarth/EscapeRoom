namespace EscapeFromConsole
{
    public enum Screen
    {
        Default,
        Title,
        Outro,
        HowToPlay
    }
    
    public class Application
    {
        private readonly Game _game = new();

        public void Run()
        {
            MainMenu();
        }

        private void MainMenu()
        {
            DrawTitleScreen();
            DrawGameOptions();
            GameOptions(Screen.Title);
        }
        
        private void QuitGame()
        {
            QuitGamePromptCommand();
            var inputKey = QuitGameValidation();
            QuitGameOptions(inputKey);
        }
        
        private void Tutorial()
        {
            DrawTutorial();
            DrawOptions();
            GameOptions(Screen.Default);
        }
        
        public void Outro()
        {
            DrawOutroScreen();
            DrawOptions();
            GameOptions(Screen.Default);
        }
        
        private void GameOptions(Screen screen)
        {
            var inputKey = GetInputOption(screen);
            switch (inputKey)
            {
                case ConsoleKey.D1:
                    Console.Clear();
                    _game.Start();
                    break;
                case ConsoleKey.D2:
                    Console.Clear();
                    QuitGame();
                    break;
                case ConsoleKey.D3:
                    Console.Clear();
                    Tutorial();
                    break;
            }
        }
        
        private void QuitGameOptions(ConsoleKey inputKey)
        {
            switch (inputKey)
            {
                case ConsoleKey.J:
                    Environment.Exit(0);
                    break;
                case ConsoleKey.N:
                    Console.WriteLine($"\n\n\n   Drücke eine belibige Taste um zum Startmenü zu gelangen.");
                    Console.ReadKey();
                    Console.Clear();
                    MainMenu();
                    break;
            }
        }

        private void DrawGameOptions()
        {
            Console.WriteLine("   1: Spiel starten");
            Console.WriteLine("   2: Spiel beenden");
            Console.WriteLine("   3: Tutorial");
        }

        private void QuitGamePromptCommand()
        {
            Console.WriteLine("\n\n\n   Spiel beenden (J/N)");
        }

        private static void DrawOptions()
        {
            Console.WriteLine("\n\n\n   1: Spiel starten");
            Console.WriteLine("   2: Spiel beenden");
        }

        private ConsoleKey GetInputOption(Screen screen)
        {
            var inputKey = GetInputKey();
            var valid = false;
            switch (screen)
            {
                case Screen.Title:
                    valid = inputKey is ConsoleKey.D1 or ConsoleKey.D2 or ConsoleKey.D3;
                    break;
                case Screen.Default:
                    valid = inputKey is ConsoleKey.D1 or ConsoleKey.D2;
                    break;
                default:
                    Console.WriteLine("Es ist ein Fehler aufgetreten. Bitte starte das Spiel neu.");
                    break;
            }

            return InputValidation(screen, valid, inputKey);
        }

        private ConsoleKey InputValidation(Screen screen, bool valid, ConsoleKey inputKey)
        {
            var validInput = valid;
            var inputOptions = screen == Screen.Title ? "1: Spiel starten, 2: Spiel beenden, 3: Tutorial" : "1: Spiel starten, 2: Spiel beenden";
            while (!validInput)
            {
                Console.WriteLine($"\n   Falsche Eingabe. {inputOptions}");
                inputKey = Console.ReadKey().Key;
                validInput = valid;
            }

            return inputKey;
        }
        
        private ConsoleKey QuitGameValidation()
        {
            var inputKey = GetInputKey();
            var valid = inputKey is ConsoleKey.J or ConsoleKey.N;
            while (!valid)
            {
                Console.WriteLine("\n   Falsche Eingabe: Spiel beenden (J/N)");
                inputKey = Console.ReadKey().Key;
                valid = inputKey is ConsoleKey.J or ConsoleKey.N;
            }

            return inputKey;
        }

        private ConsoleKey GetInputKey()
        {
            return Console.ReadKey().Key;
        }

        private void DrawTitleScreen()
        {
            Console.WriteLine();
            Console.WriteLine("   ██████ ************************** * *  ");
            Console.WriteLine("   ██████                           *     ");
            Console.WriteLine("   ██     ████ ████ ████ ████ ████     *  ");
            Console.WriteLine("   ████   █    █    ██ █ ██ █ █           ");
            Console.WriteLine("   ██     ████ █    ████ ████ ███    *  * ");
            Console.WriteLine("   ██████    █ █    ██ █ ██   █           ");
            Console.WriteLine("   ██████ ████ ████ ██ █ ██   ████     *  ");
            Console.WriteLine("                                    *     ");
            Console.WriteLine("   ████████████  ═══════════════════════ ");
            Console.WriteLine("   █          █   ██████                  ");
            Console.WriteLine("   █  Ð       #   ██   █ ████ ████ ██   █ ");
            Console.WriteLine("   █          █   █████  █  █ █  █ ███ ██ ");
            Console.WriteLine("   █     ¥    █   ██   █ █  █ █  █ ██ █ █ ");
            Console.WriteLine("   █          █   ██   █ █  █ █  █ ██   █ ");
            Console.WriteLine("   ████████████   ██   █ ████ ████ ██   █ ");
            Console.WriteLine();
            Console.WriteLine();
        }

        private void DrawOutroScreen()
        {
            Console.WriteLine();
            Console.WriteLine("   ██████ ******YOU ESCAPED THE***** * *  ");
            Console.WriteLine("   ██████                           *     ");
            Console.WriteLine("   ██     ████ ███ ████ ████ ████     *   ");
            Console.WriteLine("   ████   █    █   ██ █ ██ █ █            ");
            Console.WriteLine("   ██     ████ █   ████ ████ ███    *  *  ");
            Console.WriteLine("   ██████    █ █   ██ █ ██   █            ");
            Console.WriteLine("   ██████ ████ ███ ██ █ ██   ████     *   ");
            Console.WriteLine("                                    *     ");
            Console.WriteLine("   ████████████   ═══════════════════════ ");
            Console.WriteLine("   █          █   ██████                  ");
            Console.WriteLine("   █         ...Ð ██   █ ████ ████ ██   █ ");
            Console.WriteLine("   █          █   █████  █  █ █  █ ███ ██ ");
            Console.WriteLine("   █          █   ██   █ █  █ █  █ ██ █ █ ");
            Console.WriteLine("   █          █   ██   █ █  █ █  █ ██   █ ");
            Console.WriteLine("   ████████████   ██   █ ████ ████ ██   █ ");
            Console.WriteLine();
            Console.WriteLine();
        }

        private void DrawTutorial()
        {
            Console.WriteLine("\n\n       ******** ESCAPE ROOM ********");
            Console.WriteLine("        ──────────────────────────");
            Console.WriteLine("        ******** TUTORIAL ********");
            Console.WriteLine("\n   Zum Start wird die Größe des Escape Room bestimmt.");
            Console.WriteLine("   Höhe und Breite werden per Eingabe festgelegt.");
            Console.WriteLine("\n\n   Die Spielfigur bewegt sich durch betätigen der Pfeiltasten: ^, v, <, >.");
            Console.WriteLine("\n\n   Der Spieler wird an einer zufälligen Position in dem Escape Room ausgesetzt.");
            Console.WriteLine("   Der Raum hat eine verschlossene Tür, die mit einem Schlüssel geöffnet werden kann.");
            Console.WriteLine("   Der Schlüssel ist irgendwo im Raum zu finden.");
            Console.WriteLine("   Wenn er eigesammelt wird, öffnet sich die Tür automatisch.");
            Console.WriteLine("\n\n   Das Spiel ist gewonnen wenn der Spieler durch die Tür geht.");
        }
    }
}