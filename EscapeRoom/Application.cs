namespace EscapeFromConsole
{
    internal class Application
    {
        private readonly Game _game = new();

        internal void Run()
        {
            MainMenu();
        }

        private void MainMenu()
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
            Console.WriteLine("   1: Play Game");
            Console.WriteLine("   2: How to play");
            Console.WriteLine("   3: Quit");

            var inputKey = Console.ReadKey().Key;
            var valid = inputKey is ConsoleKey.D1 or ConsoleKey.D2 or ConsoleKey.D3;
            while (!valid)
            { 
                Console.WriteLine("   Falsche Eingabe. 1: Play Game, 2: How to play, 3: Quit");
                inputKey = Console.ReadKey().Key;
                valid = inputKey is ConsoleKey.D1 or ConsoleKey.D2 or ConsoleKey.D3;
            }

            switch (inputKey)
            {
                case ConsoleKey.D1:
                Console.Clear();
                _game.Start();
                break;
                case ConsoleKey.D2:
                Console.Clear();
                Tutorial();
                break;
                case ConsoleKey.D3:
                Console.Clear();
                Quit();
                break;
            }

        }

        internal void Outro()
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

            SelectOptions();
        }

        private void Tutorial()
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

            SelectOptions();
        }

        private void Quit()
        {
            Console.WriteLine("\n\n\n   Spiel beenden (J/N)");
            var inputKey = Console.ReadKey().Key;
            var valid = inputKey is ConsoleKey.J or ConsoleKey.N;
            while (!valid)
            {
                Console.WriteLine("\n   Falsche Eingabe: Spiel beenden (J/N)");
                inputKey = Console.ReadKey().Key;
                valid = inputKey is ConsoleKey.J or ConsoleKey.N;
            }
            
            if (inputKey == ConsoleKey.J)
            {
                Environment.Exit(0);
            }
            else if (inputKey == ConsoleKey.N)
            {
                Console.WriteLine($"\n\n\n   Drücke eine belibige Taste um zum Startmenü zu gelangen.");
                Console.ReadKey();
                Console.Clear();
                MainMenu();
            }
        }

        private void SelectOptions()
        {
            Console.WriteLine("\n\n\n   1: Main Menu");
            Console.WriteLine("   2: Quit");

            var inputKey = Console.ReadKey().Key;
            var valid = inputKey is ConsoleKey.D1 or ConsoleKey.D2;

            while (!valid)
            {
                Console.WriteLine("   Falsche Eingabe. 1: Main Menu, 2: Quit");
                inputKey = Console.ReadKey().Key;
                valid = inputKey is ConsoleKey.D1 or ConsoleKey.D2;
            }

            switch (inputKey)
            {
                case ConsoleKey.D1:
                Console.Clear();
                MainMenu();
                break;
                case ConsoleKey.D2:
                Console.Clear();
                Quit();
                break;
            }
        }
    }
}