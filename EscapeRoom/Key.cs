namespace EscapeFromConsole
{
    internal class Key
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        private readonly Room _room;

        public Key(Room room)
        {
            _room = room;
        }
        
        // draws the Key char random inside the rectangle as start position
        public void Draw(char key)
        {
            var rand = new Random();
            var randomPositionX = rand.Next(1, _room.Width);
            var randomPositionY = rand.Next(1, _room.Height);

            X = randomPositionX;
            Y = randomPositionY;

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.SetCursorPosition(X, Y);
            Console.Write(key);
        }
    }
}