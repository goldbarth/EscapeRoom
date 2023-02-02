namespace EscapeFromConsole
{
    internal class Exit
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        private readonly Room _room;

        public Exit(Room room)
        {
            _room = room;
        }

        // draws the door random on the right wallside
        public void Draw(char exit)
        {
            var rand = new Random();
            var randomPositionY = rand.Next(1, _room.Height - 1);

            X = _room.Width + 1;
            Y = randomPositionY;

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.SetCursorPosition(X, Y);
            Console.Write(exit);
        }
    }
}