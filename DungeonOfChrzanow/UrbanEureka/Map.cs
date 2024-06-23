using System.Drawing;

public class Map
{
    public Point Origin { get; set;}
    public Point Size { get; }

    private int[][] mapData;
    private Dictionary<CellType, char> cellVisuals = new Dictionary<CellType, char>{
        { CellType.WallCorner, '+'},
        { CellType.WallHorizontal, '─'},
        { CellType.WallVertical, '│'},
        { CellType.Floor, '.'},
        { CellType.Empty, ' '},
        { CellType.Grass, ','}, 
        { CellType.Gold, '*'},
    };

    private Dictionary<CellType, ConsoleColor> colorMap = new Dictionary<CellType, ConsoleColor> {
        { CellType.WallCorner, ConsoleColor.DarkBlue},
        { CellType.WallHorizontal, ConsoleColor.DarkBlue},
        { CellType.WallVertical, ConsoleColor.DarkBlue},
        { CellType.Floor, ConsoleColor.DarkGray},
        { CellType.Empty, ConsoleColor.DarkGreen},
        { CellType.Grass, ConsoleColor.Green},
        { CellType.Gold, ConsoleColor.Yellow},
    };

    private CellType[] walkableCellTypes = new CellType[] { 
        CellType.Floor, 
        CellType.Grass,
        CellType.Gold,
    };

    public Map()
    {
        mapData = new int[][] {
        new []{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},
        new []{0,2,0,0,0,0,0,2,0,0,0,0,0,0,0,0,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},
        new []{0,0,0,0,4,4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5,4,4,4,4,4,4,4,4,4,4,4,4,4,4,5,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,0,0,0,0,},
        new []{0,0,0,3,5,5,0,0,0,0,0,0,2,0,0,0,0,0,0,0,0,3,1,1,1,1,1,1,1,1,1,1,1,1,1,1,3,0,0,0,0,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,0,0,0,},
        new []{0,0,0,3,5,5,4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3,1,1,1,1,1,1,1,8,1,1,1,1,1,1,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,0,0,0,0,0,0,0,0,0,0,0,0,0,2,0,0,0,0,0,0,0,0,0,0,},
        new []{0,0,0,0,3,5,3,0,0,0,0,0,0,0,0,2,0,0,0,0,0,3,1,1,1,1,1,1,1,1,1,1,1,1,1,1,3,0,0,0,0,0,0,0,5,4,4,4,4,4,4,4,4,4,4,5,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,0,0,0,0,0,0,0,0,0,0,0,0,0,},
        new []{0,0,0,0,0,4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3,1,1,1,1,1,1,1,1,1,1,1,1,1,1,3,0,2,0,0,0,0,0,3,1,1,1,1,1,1,1,1,1,1,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4,3,0,2,0,0,0,0,0,0,0,},
        new []{0,2,0,0,0,0,0,0,0,0,0,2,0,0,0,5,4,4,4,4,4,3,1,1,5,4,4,4,4,4,4,4,4,4,4,4,5,0,0,0,0,0,0,0,3,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3,5,5,3,0,0,0,0,0,0,0,0,0,},
        new []{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3,1,1,1,1,1,3,1,1,3,0,5,4,4,4,4,4,5,0,0,0,0,0,0,0,0,0,0,0,3,1,1,1,8,1,1,1,1,1,1,1,0,2,0,0,0,0,0,0,0,0,0,0,0,0,0,3,5,5,0,2,0,0,0,0,0,0,0,0,},
        new []{0,0,0,0,0,0,0,0,0,0,2,0,0,0,0,3,1,1,1,1,1,3,1,1,3,0,3,1,1,1,1,1,3,0,0,0,0,0,0,0,0,0,0,0,3,1,1,1,1,1,1,1,1,1,1,3,0,0,0,0,0,0,0,2,0,0,0,0,2,0,0,0,0,4,0,0,0,0,0,0,0,0,0,0,},
        new []{0,0,2,0,0,0,0,0,0,0,0,0,0,0,0,3,1,1,1,1,1,3,1,1,3,0,3,1,1,8,1,1,3,0,5,4,4,4,4,4,4,4,4,4,5,4,4,4,5,1,1,5,4,4,4,5,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,0,0,0,0,},
        new []{0,0,0,0,2,0,0,0,0,2,0,0,0,0,0,3,1,1,8,1,1,1,1,1,3,0,3,1,1,1,1,1,3,0,3,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,0,2,0,0,0,0,0,0,0,0,0,0,},
        new []{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3,1,1,1,1,1,3,1,1,3,0,5,4,5,1,5,4,5,0,3,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},
        new []{0,0,0,0,4,4,0,0,0,0,0,0,0,0,0,3,1,1,1,1,1,3,1,1,3,0,0,0,3,1,3,0,0,0,3,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},
        new []{0,0,0,3,5,5,3,0,0,0,0,0,0,0,0,3,1,1,1,1,1,3,1,1,5,4,4,4,5,1,5,4,4,4,5,1,1,1,1,1,8,1,1,1,1,1,1,1,1,1,1,3,0,0,0,0,0,0,0,0,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},
        new []{0,0,0,0,4,4,0,0,0,2,0,0,0,0,0,5,4,4,4,4,4,3,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,3,0,0,0,0,0,0,0,0,0,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,0,0,0,0,0,0,},
        new []{2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},
        new []{0,0,0,0,0,2,0,0,0,0,0,0,2,0,0,0,0,0,0,0,0,5,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,5,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},  
        };

        int y = mapData.Length;
        int x = 0;

        foreach (int[] row in mapData)
        {
            if (row.Length > x)
            {
                x = row.Length;
            }
        }

        Size = new Point(x, y);
        Origin = new Point(0, 0);
    }

    public CellType GetCellAt(Point point)
    {
        return GetCellAt(point.X, point.Y);
    }

    private CellType GetCellAt(int x, int y)
    {
        return (CellType)mapData[y][x];
    }

    public char GetCellVisualAt(Point point)
    {
        return cellVisuals[GetCellAt(point)];
    }

    public void Display(Point origin)
    {
        Origin = origin;
        Console.CursorTop = origin.Y;
        for (int y = 0; y < mapData.Length; y++)
        {
            Console.CursorLeft = origin.X;
            for (int x = 0; x < mapData[y].Length; x++)
            {
                var cellValue = GetCellAt(x, y);
                var cellVisual = cellVisuals[cellValue];
                var cellColor = GetCellColorByValue(cellValue);
                Console.ForegroundColor = cellColor;
                Console.Write(cellVisual);
                Console.ResetColor();
            }
            Console.WriteLine();
        }
    }

    internal bool IsPointCorrect(Point point)
    {
        if (point.Y >= 0 && point.Y < mapData.Length)
        {
            if (point.X >= 0 && point.X < mapData[point.Y].Length)
            {
                if (walkableCellTypes.Contains(GetCellAt(point)))
                {
                    return true;
                }
            }
        }

        return false;
    }

    internal void DrawSomethingAt(char visual, Point position)
    {
        Console.SetCursorPosition(position.X + Origin.X, position.Y + Origin.Y);
        Console.Write(visual);
    }

    internal void DrawSomethingAt(string visual, Point position)
    {
        Console.SetCursorPosition(position.X + Origin.X, position.Y + Origin.Y);
        Console.Write(visual);
    }

    private ConsoleColor GetCellColorByValue(CellType value)
    {
        return colorMap.GetValueOrDefault(value, ConsoleColor.Gray);
    }

    internal void RedrawCellAt(Point position)
    {
        var cellValue = GetCellAt(position);
        var cellVisual = GetCellVisualAt(position);
        var cellColor = GetCellColorByValue(cellValue);

        Console.ForegroundColor = cellColor;
        DrawSomethingAt(cellVisual, position);
        Console.ResetColor();
    }
}