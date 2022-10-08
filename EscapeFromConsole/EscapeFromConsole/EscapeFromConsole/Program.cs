
namespace EscapeFromConsole
{
    internal class Program
    {   
        static void Main()
        {
            Console.CursorVisible = false;
            var menu = new Menu();
            menu.MainMenu();
        }
    }
}