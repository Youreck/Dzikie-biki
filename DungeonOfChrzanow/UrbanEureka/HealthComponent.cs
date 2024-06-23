using System.Diagnostics.CodeAnalysis;
using System.IO.Compression;
using System;

internal class HealthComponent
{
    public int Hp
    {
        get => hp;
        set
        {
            hp = Math.Clamp(value, 0, MaxHp);
            if (hp == 0)
            {
                OnDeath.Invoke(this, EventArgs.Empty);
            }
        }
    }
    public int hp = 90;
    public int MaxHp { get; set; } = 100;
    public event EventHandler OnDeath;
    public void Heal(int amount)
    {
        Console.WriteLine("Healing!");
        Hp += amount;
    }
    public void TakeDamage(int amount)
    {
        Hp -= amount;
    }
    private void HandleDeath()
    {
        Console.WriteLine("Character is dead!");
    }
    public HealthComponent()
    {
        OnDeath += (sender, e) => HandleDeath();
    }
}
