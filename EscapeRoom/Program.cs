namespace EscapeFromConsole
{
    public static class Program
    {
        private static void Main()
        {
            Console.CursorVisible = false;
            new Application().Run();
        }
    }
}