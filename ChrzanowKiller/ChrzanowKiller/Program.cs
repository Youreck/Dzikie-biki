using System;
using System.Drawing;

class Program
{
    static void Main()
    {
        Map map = new Map();
        Point initialPosition = new Point(80, 16);
        CellType[] walkableCellTypes = new CellType[] {
            CellType.Floor,
            CellType.Grass,
            CellType.Diamond,
            CellType.Door,
            CellType.Shadow
        };

        Movement movement = new Movement(map, initialPosition, walkableCellTypes);
        map.Display(movement.GetPlayerPosition());

        while (true)
        {
            var key = Console.ReadKey(true).Key;
            movement.MovePlayer(key);
        }
    }
}
