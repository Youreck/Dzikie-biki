class ComposedEnemy
{
    public VisualComponent VisualComponent { get; }
    public HealthComponent Health { get; }
    public PositionComponent PositionComponent { get; }
    public MovementComponent Movement { get; }
    public IInputComponent InputComponent { get; }

    public ComposedEnemy(string visual, Point startingPosition)
    {
        VisualComponent = new VisualComponent(visual);
        Health = new HealthComponent();
        PositionComponent = new PositionComponent(startingPosition);
        InputComponent = new RandomInputComponent();
        Movement = new MovementComponent(PositionComponent, InputComponent);
    }
}