public class Player
{
    public int X { get; set; }
    public int Y { get; set; }

    public Player()
    {
        X = 0;
        Y = 0;
    }

    public void Update()
    {
        if (Console.KeyAvailable)
        {
            var key = Console.ReadKey(true).Key;

            switch (key)
            {
                case ConsoleKey.UpArrow:
                    Y--;
                    break;
                case ConsoleKey.DownArrow:
                    Y++;
                    break;
                case ConsoleKey.LeftArrow:
                    X--;
                    break;
                case ConsoleKey.RightArrow:
                    X++;
                    break;
            }
        }
    }
}