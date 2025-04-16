using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public string itemName = "Cake";
    public int amount = 1;

    private bool playerInRange = false;
    private InventoryComponent inventoryComponent;

    private static ItemsLookup itemsLookup = new ItemsLookup();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            inventoryComponent = other.GetComponent<InventoryComponent>();

            if (inventoryComponent == null)
            {
                Debug.LogError("Player is missing InventoryComponent script!");
            }
            else
            {
                Debug.Log($"Player in range! Press E to pick up {itemName}.");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            inventoryComponent = null;
            Debug.Log("Player left range.");
        }
    }

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (itemsLookup.Items.TryGetValue(itemName, out IEffect effect))
            {
                inventoryComponent?.AddToInventory(itemName, amount, effect);
                Debug.Log($"Picked up {amount} {itemName}(s)!");
            }
            else
            {
                Debug.LogWarning($"No effect found for item: {itemName}");
            }

            Destroy(gameObject);
        }
    }
}

