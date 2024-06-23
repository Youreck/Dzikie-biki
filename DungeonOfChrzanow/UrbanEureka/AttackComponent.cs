class AttackComponent
{
    private int range = 2;
    private int strength = 10;
    private readonly PositionComponent positionComponent;

    public AttackComponent(PositionComponent positionComponent)
    {
        this.positionComponent = positionComponent;
    }

    public bool IsTargetInRange(Point targetPosition)
    {
        int distanceX = Math.Abs(positionComponent.Position.X - targetPosition.X);
        int distanceY = Math.Abs(positionComponent.Position.Y - targetPosition.Y);


        return (distanceX <= range && distanceY == 0) || (distanceX == 0 && distanceY <= range);
    }

    public void Attack(HealthComponent targetHealthComponent, GameObject npc)
    {
        targetHealthComponent.TakeDamage(strength);


        if (targetHealthComponent.CurrentHealth <= 0)
        {
            npc.SetActive(false);
        }
    }
}




