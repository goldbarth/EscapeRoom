
namespace EscapeFromConsole
{
    internal class Room
    {
        public int width;
        public int height;

        int[,] room;
        
        public Room(int width, int height)
        {
            this.width = width;
            this.height = height;

            room = new int[width, height];
        }

        // draws a rectangle, first part wall, second part floor
        public void Draw(char wallChar, char flooChar)
        {
            for (int y = -1; y < height + 1; y++)
            {
                for (int x = -1; x < width + 1; x++)
                {
                    // first
                    if (y == -1 || x == -1 || y == height || x == width)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write(wallChar);
                    }
                    else 
                    {
                        // second
                        int current = room[x, y];
                        if (current == 0)
                        {
                            Console.Write(flooChar);
                        }
                    }
                }
                Console.WriteLine();
            }
        }
    }
}