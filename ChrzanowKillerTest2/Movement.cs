using System;
using System.Drawing;

public class Movement
{
    private Map map;
    private Point playerPosition;
    private CellType[] walkableCellTypes;

    public Movement(Map map, Point initialPosition, CellType[] walkableCellTypes)
    {
        this.map = map;
        this.playerPosition = initialPosition;
        this.walkableCellTypes = walkableCellTypes;
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