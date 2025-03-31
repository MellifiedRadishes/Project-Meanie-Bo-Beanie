using System;
using System.Collections.Generic;

// Interface for all item effects
public interface IEffect
{
    void ApplyEffect(MeaniePlayer player);
}

// Concrete Effect: Cake (Restores Health)
public class CakeEffect : IEffect
{
    public void ApplyEffect(MeaniePlayer player)
    {
        player.Health += 20;
        Console.WriteLine("You ate a Cake! +20 Health.");
    }
}

// Concrete Effect: Potion (Boosts Damage)
public class PotionEffect : IEffect
{
    public void ApplyEffect(MeaniePlayer player)
    {
        player.Damage += 5;
        Console.WriteLine("You drank a Potion! +5 Damage.");
    }
}

// Concrete Effect: Pizza (Extends Slider Press Time)
public class PizzaEffect : IEffect
{
    public void ApplyEffect(MeaniePlayer player)
    {
        player.SliderTime += 3.0f;
        Console.WriteLine("You ate a Pizza! +3s Slider Time.");
    }
}

public class ItemsLookup
{
    public Dictionary<string, IEffect> Items = new Dictionary<string, IEffect>()
    {
        { "Cake", new CakeEffect() },
        { "Potion", new PotionEffect() },
        { "Pizza", new PizzaEffect() }
    };
}


