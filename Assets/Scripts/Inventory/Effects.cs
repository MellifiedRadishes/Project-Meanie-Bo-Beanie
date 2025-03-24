using System;

//Defining Player stats
public class Player
{
    public int Health { get; set; } = 100;
    public int Damage { get; set; } = 10;
    public float SliderTime { get; set; } = 5.0f;

    public void ShowStats()
    {
        Console.WriteLine($"Health: {Health}, Damage: {Damage}, SliderTime: {SliderTime}s");
    }
}

// Interface for all item effects
public interface IEffect
{
    void ApplyEffect(Player player);
}

// Concrete Effect: Cake (Restores Health)
public class CakeEffect : IEffect
{
    public void ApplyEffect(Player player)
    {
        player.Health += 20;
        Console.WriteLine("You ate a Cake! +20 Health.");
    }
}

// Concrete Effect: Potion (Boosts Damage)
public class PotionEffect : IEffect
{
    public void ApplyEffect(Player player)
    {
        player.Damage += 5;
        Console.WriteLine("You drank a Potion! +5 Damage.");
    }
}

// Concrete Effect: Pizza (Extends Slider Press Time)
public class PizzaEffect : IEffect
{
    public void ApplyEffect(Player player)
    {
        player.SliderTime += 3.0f;
        Console.WriteLine("You ate a Pizza! +3s Slider Time.");
    }
}

