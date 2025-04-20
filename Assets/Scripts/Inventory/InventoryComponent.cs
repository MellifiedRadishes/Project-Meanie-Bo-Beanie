using UnityEngine;

public class InventoryComponent : MonoBehaviour
{
    public InventorySystem inventorySystem = new InventorySystem();

    public void AddToInventory(string itemName, int amount, IEffect effect)
    {
        // inventorySystem.AddItem(itemName, amount, effect);
    }

    public void UseItem(string itemName, MeaniePlayer player)
    {
        // inventorySystem.UseItem(itemName, player);
    }

    public void ShowInventory()
    {
        inventorySystem.ShowInventory();
    }
}
