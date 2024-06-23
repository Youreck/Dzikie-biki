public class PositionComponent
{
    public Point Position { get; set; }

    public PositionComponent(Point startingPosition)
    {
        Position = new Point(startingPosition);
    }
}
