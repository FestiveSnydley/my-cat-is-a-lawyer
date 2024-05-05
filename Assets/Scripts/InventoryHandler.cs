using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEditor.Progress;

public class InventoryHandler : MonoBehaviour
{
    //save the inventory gameobject
    private GameObject inventory;

    private InventoryController controller;

    public List<InventoryItem> initialItems = new List<InventoryItem>();

    private InventoryItem selectedItem;

    // Start is called before the first frame update
    private void Start()
    {
        // Unsubscribe first to avoid duplicates
        SceneManager.sceneLoaded -= OnSceneLoaded;

        // Subscribe to the sceneLoaded event
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        // Unsubscribe from the sceneLoaded event when the script is destroyed
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Find the Controller game object again when a new scene is loaded
        inventory = GameObject.Find("PlayerInventory");
        controller = inventory.GetComponent<InventoryController>();
        selectedItem = InventoryItem.GetEmptyItem();

        // Check if inventory is found
        if (inventory != null)
        {
            Debug.Log("PlayerInventory found in OnSceneLoaded()");
        }
        else
        {
            Debug.Log("PlayerInventory not found in OnSceneLoaded()");
        }
    }

    private void LoadInventory()
    {
        inventory = GameObject.Find("PlayerInventory");
        controller = inventory.GetComponent<InventoryController>();
        selectedItem = InventoryItem.GetEmptyItem();

        // Check if inventory is found
        if (inventory != null)
        {
            Debug.Log("PlayerInventory found in OnSceneLoaded()");
        }
        else
        {
            Debug.Log("PlayerInventory not found in OnSceneLoaded()");
        }
    }

    public InventoryItem FindItemByName(string itemName)
    {
        foreach (InventoryItem item in initialItems)
        {
            if (item.item != null && item.item.InternalName == itemName)
            {
                Debug.Log("Item found and assigned to selectedItem: " + itemName);
                return item;
            }
        }

        Debug.Log("Item not found: " + itemName);
        return InventoryItem.GetEmptyItem();

    }
    public void LogAllItems()
    {
        foreach (InventoryItem item in initialItems)
        {
            Debug.Log("Item Name: " + item.item.InternalName);
        }
    }

    public void AddItem(string itemName)
    {
        LoadInventory();
        InventoryItem newItem = FindItemByName(itemName);

        if (!newItem.IsEmpty)
        {
            controller.Add(newItem);
        }
        else
        {
            Debug.Log("Cannot add item. Item not found: " + itemName);
        }
    }

    public void ReplaceItem(string oldItemName, string newItemName)
    {
        LoadInventory();
        InventoryItem oldItem = FindItemByName(oldItemName);
        InventoryItem newItem = FindItemByName(newItemName);

        if (!oldItem.IsEmpty && !newItem.IsEmpty)
        {
            controller.Replace(oldItem, newItem);
        }
        else
        {
            Debug.Log("Cannot replace item. One or both items not found.");

            if (oldItem.IsEmpty)
            {
                Debug.Log("Cannot replace item. Old item not found: " + oldItemName);
            }

            if (newItem.IsEmpty)
            {
                Debug.Log("Cannot replace item. New item not found: " + newItemName);
            }
        }
    }

    public void DebugInventory()
    {
        controller.DebugSuccess();
    }

    public void Pause()
    {
        InventoryController.PauseInventory();
    }
    public void Unpause()
    {
        InventoryController.UnpauseInventory();
    }

    public void InvSummon()
    {
        InventoryController.InventorySummon();
    }


}
