namespace EscapeFromConsole
{
    public class Key
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        private readonly Room _room;

        public Key(Room room)
        {
            _room = room;
        }
        
        public void DrawKeyPosition(char key)
        {
            SetRandomPosition();
            DrawPosition(key);
        }

        private void DrawPosition(char key)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.SetCursorPosition(X, Y);
            Console.Write(key);
        }

        private void SetRandomPosition()
        {
            var rand = new Random();
            var randomPositionX = rand.Next(1, _room.Width);
            var randomPositionY = rand.Next(1, _room.Height);

            X = randomPositionX;
            Y = randomPositionY;
        }
    }
}