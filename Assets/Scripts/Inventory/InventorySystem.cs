using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class InventorySlot
{
    public InventoryItemData itemData;
    public int amount;
    public IEffect effect; // You can serialize this with ScriptableObjects too
}

public class InventorySystem : MonoBehaviour
{
    public static InventorySystem Instance { get; private set; }

    [SerializeField]
    private List<InventorySlot> inventory = new List<InventorySlot>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public void AddItem(InventoryItemData itemData, int amount, IEffect effect)
    {
        InventorySlot slot = inventory.Find(i => i.itemData == itemData);

        if (slot != null)
        {
            slot.amount = Mathf.Min(slot.amount + amount, itemData.maxStack);
        }
        else
        {
            inventory.Add(new InventorySlot { itemData = itemData, amount = amount, effect = effect });
        }

        Debug.Log($"Added {amount} {itemData.itemName}(s) to inventory.");
    }

    public void UseItem(InventoryItemData itemData, MeaniePlayer player)
    {
        InventorySlot slot = inventory.Find(i => i.itemData == itemData);

        if (slot != null && slot.amount > 0)
        {
            slot.effect?.ApplyEffect(player);
            slot.amount--;

            if (slot.amount == 0)
            {
                inventory.Remove(slot);
                Debug.Log($"{itemData.itemName} is used up and removed from inventory.");
            }
        }
        else
        {
            Debug.Log($"No {itemData.itemName} available in inventory.");
        }
    }

    public void ShowInventory()
    {
        Debug.Log("Current Inventory:");
        if (inventory.Count == 0)
        {
            Debug.Log("Inventory is empty.");
        }
        else
        {
            foreach (var item in inventory)
            {
                Debug.Log($"{item.itemData.itemName}: {item.amount}");
            }
        }
    }
}
