
namespace EscapeFromConsole
{
    internal class Key
    {
        public int X { get; set; }
        public int Y { get; set; }

        Room room;

        public Key(Room room)
        {
            this.room = room;
        }
        
        // draws the key char random inside the rectangle as start position
        public void Draw(char key)
        {
            Random rand = new Random();
            int randomPositionX = rand.Next(1, room.width);
            int randomPositionY = rand.Next(1, room.height);

            X = randomPositionX;
            Y = randomPositionY;

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.SetCursorPosition(X, Y);
            Console.Write(key);
        }
    }
}