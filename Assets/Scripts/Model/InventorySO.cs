using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// ScriptableObject representing inventory to store and manage inventory data.
/// </summary>
[CreateAssetMenu]
public class InventorySO : ScriptableObject
{
    // Serialized fields allow private variables to be visible in the Unity Editor.
    [SerializeField]
    private List<Item> inventoryItems;

    [field: SerializeField]
    public int Size { get; private set; } = 10;

    /// <summary>
    /// Initialization method to set up the inventory with empty items.
    /// </summary>
    public void Initialize()
    {
        // Create a new list to hold items.
        inventoryItems = new List<Item>();

        // Populate the list with empty items.
        for (int i = 0; i < Size; i++)
        {
            inventoryItems.Add(Item.GetEmptyItem());
        }
    }

    /// <summary>
    /// Adds an item to the inventory.
    /// </summary>
    /// <param name="item">Item to be added.</param>
    public void AddItem(ItemSO item)
    {
        for (int i = 0; i < inventoryItems.Count; i++)
        {
            if (inventoryItems[i].IsEmpty)
            {
                inventoryItems[i] = new Item
                {
                    item = item
                };
            }
        }
    }

    /// <summary>
    /// Retrieves the current state of the inventory as a dictionary.
    /// </summary>
    /// <returns>Dictionary representing the current inventory state.</returns>
    public Dictionary<int, Item> GetCurrentInventoryState()
    {
        // Dictionary to store non-empty items and their indices.
        Dictionary<int, Item> returnValue =
            new Dictionary<int, Item>();

        // Loop through the inventory to filter out empty slots.
        for (int i = 0; i < inventoryItems.Count; i++)
        {
            // Skip empty slots.
            if (inventoryItems[i].IsEmpty)
                continue;

            // Add non-empty items to the dictionary with their indices.
            returnValue[i] = inventoryItems[i];
        }
        // Return the dictionary representing the current inventory state.
        return returnValue;
    }

    /// <summary>
    /// Gets the item at a specific index in the inventory.
    /// </summary>
    /// <param name="itemIndex">Index of the item.</param>
    /// <returns>Item at the specified index.</returns>
    public Item GetItemAt(int itemIndex)
    {
        return inventoryItems[itemIndex];
    }
}

/// <summary>
/// Serializable struct representing an item in the inventory.
/// </summary>
[Serializable]
public struct Item
{
    // Reference to the ScriptableObject representing the item.
    public ItemSO item;

    // Indicates whether the item is empty.
    public bool IsEmpty => item == null;

    /// <summary>
    /// Static method to get an empty item.
    /// </summary>
    /// <returns>Empty item.</returns>
    public static Item GetEmptyItem()
        => new Item
        {
            item = null,
        };
}