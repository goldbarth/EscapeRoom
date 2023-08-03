namespace EscapeFromConsole
{
    public class Room
    {
        public readonly int Width;
        public readonly int Height;
        private readonly int[,] _room;
        
        public Room(int width, int height)
        {
            Width = width;
            Height = height;

            _room = new int[width, height];
        }

        // draws a rectangle, first part wall, second part floor
        public void Draw(char wallChar, char floorChar)
        {
            for (int y = -1; y < Height + 1; y++)
            {
                for (int x = -1; x < Width + 1; x++)
                {
                    // first
                    if (y == -1 || x == -1 || y == Height || x == Width)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write(wallChar);
                    }
                    else 
                    {
                        // second
                        var current = _room[x, y];
                        if (current == 0)
                        {
                            Console.Write(floorChar);
                        }
                    }
                }
                Console.WriteLine();
            }
        }
    }
}