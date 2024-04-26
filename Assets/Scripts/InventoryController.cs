using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>
/// Controls the interaction between the Inventory UI and the underlying data.
/// </summary>
public class InventoryController : MonoBehaviour
{
    // Serialized fields allow private variables to be visible in the Unity Editor.
    [SerializeField]
    private InventoryPage inventoryPageUI;

    [SerializeField]
    private InventorySO inventoryData;

    // Default inventory size.
    public int inventorySize = 10;

    private static InventoryController instance;

    /// <summary>
    /// Called at the start of the script's execution.
    /// </summary>
    private void Start()
    {
        // Prepare the Inventory UI and initialize the inventory data.
        PrepareUI();
        inventoryData.Initialize();

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            PrepareUI();
            inventoryData.Initialize();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Prepares the Inventory UI by initializing it and subscribing to events.
    /// </summary>
    private void PrepareUI()
    {
        // Initialize the UI with the size of the inventory (number of slots).
        inventoryPageUI.InitializeInventoryUI(inventoryData.Size);
        
        // Subscribe the HandleDescriptionRequest method to the OnDescriptionRequested event.
        this.inventoryPageUI.OnDescriptionRequested += HandleDescriptionRequest;

        //this.inventoryUI.OnItemActionRequested += HandleItemActionRequest;
    }

    //private void HandleItemActionRequest(int itemIndex)
    //{

    //}

    /// <summary>
    /// Handles a description request for a specific inventory item.
    /// </summary>
    /// <param name="itemIndex">Index of the requested item.</param>
    private void HandleDescriptionRequest(int itemIndex)
    {
        // Log a message to indicate the description request.
        Debug.Log("Description requested.");

        // Retrieve the actual item in the inventory and its associated data.
        Item inventoryItem = inventoryData.GetItemAt(itemIndex);

        // If the item is empty, no further action is taken.
        if (inventoryItem.IsEmpty)
            return;

        // Get the data of the item in the inventory.
        ItemSO item = inventoryItem.item;

        // Update the inventory page UI with the item's data.
        inventoryPageUI.UpdateDescription(itemIndex, item.ItemImage,
            item.name, item.Description);
    }

    /// <summary>
    /// Called every frame.
    /// </summary>
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Debug.Log(SceneManager.GetActiveScene().buildIndex);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (SceneManager.GetActiveScene().buildIndex == 0)
            {
                SceneManager.LoadScene(1);
            }
            else
            {
                SceneManager.LoadScene(0);
            }

        }

        // Toggle the visibility of the inventory UI when the 'I' key is pressed.
        if (Input.GetKeyDown(KeyCode.I))
        {
            // If the inventory page UI is not active, show it.
            if (inventoryPageUI.isActiveAndEnabled == false)
            {
                inventoryPageUI.Show();

                // Update the UI with the current state of the inventory data.
                foreach (var item in inventoryData.GetCurrentInventoryState())
                {
                    inventoryPageUI.UpdateData(item.Key, item.Value.item.ItemImage);
                }

            }
            else
            {
                // If the inventory page UI is active, hide it.
                inventoryPageUI.Hide();
            }
        }
    }
}
