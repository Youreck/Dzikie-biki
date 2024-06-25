public class NPC
{
    public int X { get; set; }
    public int Y { get; set; }
    private Random random;

    public NPC(int maxX, int maxY)
    {
        random = new Random();
        X = random.Next(0, maxX);
        Y = random.Next(0, maxY);
    }

    public bool IsNearPlayer(int playerX, int playerY)
    {
        return Math.Abs(playerX - X) <= 1 && Math.Abs(playerY - Y) <= 1;
    }
}
public class Game
{
    private Player player;
    private NPC npc;
    private int mapWidth;
    private int mapHeight;

    public Game(int mapWidth, int mapHeight)
    {
        this.mapWidth = mapWidth;
        this.mapHeight = mapHeight;
        player = new Player();
        npc = new NPC(mapWidth, mapHeight);
    }

    public void Update()
    {
        player.Update();

        if (npc.IsNearPlayer(player.X, player.Y))
        {
            Console.WriteLine("Poproszę monety");
        }

        
    }
}