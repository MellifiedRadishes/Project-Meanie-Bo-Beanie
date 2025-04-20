//using System;
//using System.Collections.Generic;

//public class InventoryItem
//{
//    public int Amount { get; set; }
//    public IEffect Effect { get; private set; }

//    public InventoryItem(int amount, IEffect effect)
//    {
//        Amount = amount;
//        Effect = effect;
//    }
//}

//public class InventorySystem
//{
//    private Dictionary<string, InventoryItem> inventory = new Dictionary<string, InventoryItem>();

//    // Add an item to the inventory
//    public void AddItem(string itemName, int amount, IEffect effect)
//    {
//        if (inventory.ContainsKey(itemName))
//        {
//            inventory[itemName].Amount += amount;
//        }
//        else
//        {
//            inventory[itemName] = new InventoryItem(amount, effect);
//        }
//        Console.WriteLine($"Added {amount} {itemName}(s) to inventory.");
//    }

//    // Use an item from inventory
//    public void UseItem(string itemName, MeaniePlayer player)
//    {
//        if (inventory.ContainsKey(itemName) && inventory[itemName].Amount > 0)
//        {
//            inventory[itemName].Effect.ApplyEffect(player);
//            inventory[itemName].Amount--;

//            if (inventory[itemName].Amount == 0)
//            {
//                inventory.Remove(itemName);
//                Console.WriteLine($"{itemName} is used up and removed from inventory.");
//            }
//        }
//        else
//        {
//            Console.WriteLine($"No {itemName} available in inventory.");
//        }
//    }

//    // Display inventory contents
//    public void ShowInventory()
//    {
//        Console.WriteLine("\nCurrent Inventory:");
//        if (inventory.Count == 0)
//        {
//            Console.WriteLine("Inventory is empty.");
//        }
//        else
//        {
//            foreach (var item in inventory)
//            {
//                Console.WriteLine($"{item.Key}: {item.Value.Amount}");
//            }
//        }
//    }
//}