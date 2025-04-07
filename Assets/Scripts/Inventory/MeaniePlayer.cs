using System;
public class MeaniePlayer
{
    public int Health { get; set; } = 100;
    public int Damage { get; set; } = 10;
    public float SliderTime { get; set; } = 5.0f;

    public void ShowStats()
    {
        Console.WriteLine($"Health: {Health}, Damage: {Damage}, SliderTime: {SliderTime}s");
    }
}
