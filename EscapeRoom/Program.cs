namespace EscapeFromConsole
{
    internal static class Program
    {
        private static void Main()
        {
            Console.CursorVisible = false;
            var app = new Application();
            app.Run();
        }
    }
}