using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEditor.Progress;


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

    private static bool isPaused = false;
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
        InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);

        // If the item is empty, no further action is taken.
        if (inventoryItem.IsEmpty) { 
            inventoryPageUI.ResetSelection();
            return;
        }

        // Get the data of the item in the inventory.
        ItemSO item = inventoryItem.item;

        // Update the inventory page UI with the item's data.
        inventoryPageUI.UpdateDescription(itemIndex, item.ItemImage,
            item.Name, item.Description);
    }

    /// <summary>
    /// Called every frame.
    /// </summary>
    public void Update()
    {
        if (isPaused == true)
        {
            return;
        }
        else
        {
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

    public void Add(InventoryItem item)
    {
        inventoryData.AddItem(item);
    }
    public void Replace(InventoryItem oldItem, InventoryItem newItem)
    {
        inventoryData.ReplaceItem(oldItem, newItem);
    }

    public void DebugSuccess()
    {
        Debug.Log("Successfully accessed inventory.");
    }


    /// <summary>
    /// Toggle the pause state of the inventory.
    /// </summary>
    public static void TogglePause()
    {
        isPaused = !isPaused;
    }

    /// <summary>
    /// Pause, stopping the player from accessing their inventory.
    /// </summary>
    public static void PauseInventory()
    {
        isPaused = true;
    }

    /// <summary>
    /// Unpause and allow the player to access their inventory once more.
    /// </summary>
    public static void UnpauseInventory()
    {
        isPaused = false;
    }

    /// <summary>
    /// Load the inventory screen if it isn't loaded already.
    /// </summary>
    public static void InventorySummon()
    {
        Scene inventoryScene = SceneManager.GetSceneByName("InventoryScreen");
        if (!inventoryScene.isLoaded)
        {
            SceneManager.LoadScene("InventoryScreen", LoadSceneMode.Additive);
        }
    }

}
