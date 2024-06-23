class ComposedPlayer
{
    public VisualComponent VisualComponent { get; }
    public HealthComponent Health { get; }
    public PositionComponent PositionComponent { get; }
    public MovementComponent Movement { get; }
    public IInputComponent InputComponent { get; }
    public AttackComponent AttackComponent { get; }

    public ComposedPlayer(string visual, Point startingPosition)
    {
        VisualComponent = new VisualComponent(visual);
        Health = new HealthComponent();
        PositionComponent = new PositionComponent(startingPosition);
        InputComponent = new KeyboardInputComponent();
        Movement = new MovementComponent(PositionComponent, InputComponent);
        AttackComponent = new AttackComponent(PositionComponent);
    }
}
