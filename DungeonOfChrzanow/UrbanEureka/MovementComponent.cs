internal class MovementComponent
{
    private readonly PositionComponent positionComponent;
    private readonly IInputComponent inputComponent;

    public Point PreviousPosition { get; set;}

    public MovementComponent(PositionComponent positionComponent, IInputComponent inputComponent)
    {
        PreviousPosition = new Point(positionComponent.Position);
        this.positionComponent = positionComponent;
        this.inputComponent = inputComponent;
    }

    public void Move(Point targetPosition)
    {
        PreviousPosition.X = positionComponent.Position.X;
        PreviousPosition.Y = positionComponent.Position.Y;
        
        positionComponent.Position.X = targetPosition.X;
        positionComponent.Position.Y = targetPosition.Y;
    }

    public Point GetNextPosition()
    {
        Point nextPosition = new Point(positionComponent.Position);
        Point direction = inputComponent.GetDirection();
        nextPosition.X += direction.X;
        nextPosition.Y += direction.Y;

        return nextPosition;
    }
}
