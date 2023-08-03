namespace EscapeFromConsole
{
    public class Exit
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        private readonly Room _room;

        public Exit(Room room)
        {
            _room = room;
        }
        
        public void DrawExitPosition(char exit)
        {
            SetRandomPosition();
            DrawExit(exit);
        }

        private void DrawExit(char exit)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.SetCursorPosition(X, Y);
            Console.Write(exit);
        }

        private void SetRandomPosition()
        {
            var rand = new Random();
            var randomPositionY = rand.Next(1, _room.Height - 1);

            X = _room.Width + 1;
            Y = randomPositionY;
        }
    }
}