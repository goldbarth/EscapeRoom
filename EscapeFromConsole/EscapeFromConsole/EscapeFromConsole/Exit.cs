﻿
namespace EscapeFromConsole
{
    internal class Exit
    {
        public int X { get; set; }
        public int Y { get; set; }

        Room room;

        public Exit(Room room)
        {
            this.room = room;
        }

        // draws the door random on the right wallside
        public void Draw(char exit)
        {
            Random rand = new Random();
            int randomPositionY = rand.Next(1, room.height - 1);

            X = room.width + 1;
            Y = randomPositionY;

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.SetCursorPosition(X, Y);
            Console.Write(exit);
        }
    }
}