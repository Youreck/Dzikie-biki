using System;
using System.Drawing;

public class Movement
{
    private Map map;
    private Point playerPosition;
    private CellType[] walkableCellTypes;
    private int diamondCount;

    public Movement(Map map, Point initialPosition, CellType[] walkableCellTypes)
    {
        this.map = map;
        this.playerPosition = initialPosition;
        this.walkableCellTypes = walkableCellTypes;
        this.diamondCount = 0;
    }

    public void MovePlayer(ConsoleKey key)
    {
        Point newPosition = playerPosition;
        switch (key)
        {
            case ConsoleKey.W:
                newPosition.Y--;
                break;
            case ConsoleKey.S:
                newPosition.Y++;
                break;
            case ConsoleKey.A:
                newPosition.X--;
                break;
            case ConsoleKey.D:
                newPosition.X++;
                break;
        }

        if (IsWalkable(newPosition))
        {
            var cellType = map.GetCellAt(newPosition.X, newPosition.Y);
            if (cellType == CellType.Diamond)
            {
                diamondCount++;
                Console.WriteLine($"You got {diamondCount} diamond{(diamondCount > 1 ? "s" : "")}!");
                map.PickUpDiamond(newPosition); 
            }
            playerPosition = newPosition;
        }

        map.Display(playerPosition);
    }

    private bool IsWalkable(Point position)
    {
        if (position.X < 0 || position.X >= map.Size.X || position.Y < 0 || position.Y >= map.Size.Y)
        {
            return false;
        }

        var cellType = map.GetCellAt(position.X, position.Y);
        return Array.Exists(walkableCellTypes, type => type == cellType);
    }

    public Point GetPlayerPosition()
    {
        return playerPosition;
    }
}
