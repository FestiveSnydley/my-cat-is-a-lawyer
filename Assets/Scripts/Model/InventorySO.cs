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
    private List<InventoryItem> inventoryItems;

    [field: SerializeField]
    public int Size { get; private set; } = 10;

    /// <summary>
    /// Initialization method to set up the inventory with empty items.
    /// </summary>
    public void Initialize()
    {
        // Create a new list to hold items.
        inventoryItems = new List<InventoryItem>();

        // Populate the list with empty items.
        for (int i = 0; i < Size; i++)
        {
            inventoryItems.Add(InventoryItem.GetEmptyItem());
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
                inventoryItems[i] = new InventoryItem
                {
                    item = item
                };
                return;
            }
        }
    }

    public void AddItem(InventoryItem item)
    {
        AddItem(item.item);
    }

    /// <summary>
    /// Retrieves the current state of the inventory as a dictionary.
    /// </summary>
    /// <returns>Dictionary representing the current inventory state.</returns>
    public Dictionary<int, InventoryItem> GetCurrentInventoryState()
    {
        // Dictionary to store non-empty items and their indices.
        Dictionary<int, InventoryItem> returnValue =
            new Dictionary<int, InventoryItem>();

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
    public InventoryItem GetItemAt(int itemIndex)
    {
        return inventoryItems[itemIndex];
    }

    /// <summary>
    /// Finds the position of an item by its name.
    /// </summary>
    /// <param name="itemName">Name of the item to find.</param>
    /// <returns>The index of the item if found; otherwise, -1.</returns>
    public int FindItemPosition(ItemSO item)
    {
        for (int i = 0; i < inventoryItems.Count; i++)
        {
            if (!inventoryItems[i].IsEmpty && inventoryItems[i].item == item)
            {
                Debug.Log("Item found at position: " + i);
                return i;
            }
        }
        Debug.Log("Item not found.");
        return -1;
    }

    /// <summary>
    /// Replaces an item with a specified name with a new item.
    /// </summary>
    /// <param name="itemName">Name of the item to replace.</param>
    /// <param name="newItem">New item to replace with.</param>
    public void ReplaceItem(ItemSO oldItem, ItemSO newItem)
    {
        int position = FindItemPosition(oldItem);
        if (position != -1)
        {
            inventoryItems[position] = new InventoryItem
                {
                    item = newItem
                };
            Debug.Log("Item replaced successfully.");
        }
        else
        {
            Debug.Log("Item not found or replacement failed.");
        }
    }

    public void ReplaceItem(InventoryItem oldItem, InventoryItem newItem)
    {
        ReplaceItem(oldItem.item, newItem.item);
    }
}

/// <summary>
/// Serializable struct representing an item in the inventory.
/// </summary>
[Serializable]
public struct InventoryItem
{
    // Reference to the ScriptableObject representing the item.
    public ItemSO item;

    // Indicates whether the item is empty.
    public bool IsEmpty => item == null;

    /// <summary>
    /// Static method to get an empty item.
    /// </summary>
    /// <returns>Empty item.</returns>
    public static InventoryItem GetEmptyItem()
        => new InventoryItem
        {
            item = null,
        };
}