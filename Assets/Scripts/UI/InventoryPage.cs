using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
// Everything above is generated with Unity

/// <summary>
/// Represents a page within the inventory, managing UI elements and interactions.
/// </summary>
public class InventoryPage : MonoBehaviour
{
    // Serialized fields allow private variables to be visible in the Unity Editor.
    [SerializeField]
    private InventorySlot itemPrefab;

    [SerializeField]
    private RectTransform contentPanel;

    [SerializeField]
    private InventoryDescription itemDescription;

    // Public variables for testing purposes.
    public Sprite image;
    public string title, description;

    // List to store instantiated InventorySlot objects.
    List<InventorySlot> listOfSlots = new List<InventorySlot>();

    // Event triggered when the description of an item is requested.
    public event Action<int> OnDescriptionRequested;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        // Hide the inventory page and reset the item description panel on awake.
        Hide();
        itemDescription.ResetDescription();
    }

    /// <summary>
    /// Initializes the empty inventory slots for the UI of the inventory page with a specified size.
    /// </summary>
    /// <param name="inventorySize">The size of the inventory.</param>
    public void InitializeInventoryUI(int inventorySize)
    {
        for(int i = 0; i < inventorySize; i++)
        {
            // Instantiate InventorySlot objects (empty slots)
            InventorySlot Slot = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);

            // Set InventorySlot parent to the Content panel which is the list of slots
            Slot.transform.SetParent(contentPanel);

            // Set scale of slot relative to the parent to keep the size consistent.
            Slot.transform.localScale = new Vector3(1, 1, 1);

            listOfSlots.Add(Slot);

            Slot.OnItemClicked += HandleItemSelection;
        }
    }

    /// <summary>
    /// Updates the data for a specific slot at the given index.
    /// </summary>
    /// <param name="itemIndex">Index of the item to update.</param>
    /// <param name="itemImage">Sprite representing the item.</param>
    public void UpdateData(int itemIndex,
            Sprite itemImage)
    {
        // If the number of slots is greater than given index (not out of bounds),
        if (listOfSlots.Count > itemIndex)
        {
            // Set sprite to given image.
            listOfSlots[itemIndex].SetData(itemImage);
        }
    }

    /// <summary>
    /// Handles the selection of an InventorySlot, triggering the OnDescriptionRequested event.
    /// </summary>
    /// <param name="slot">Selected InventorySlot.</param>
    private void HandleItemSelection(InventorySlot slot)
    {
        int index = listOfSlots.IndexOf(slot);

        // If slot is not in the list
        if (index == -1)
            return;

        UnityEngine.Debug.Log($"Item selection at index {index}.");

        OnDescriptionRequested?.Invoke(index);
    }

    /// <summary>
    /// Shows the inventory page and resets the selection.
    /// </summary>
    public void Show()
    {
        UnityEngine.Debug.Log("Inventory has been opened.");
        gameObject.SetActive(true);
        itemDescription.ResetDescription();
        ResetSelection();
    }

    /// <summary>
    /// Hides the inventory page.
    /// </summary>
    public void Hide()
    {
        UnityEngine.Debug.Log("Inventory has been hidden.");
        gameObject.SetActive(false);
    }

    /// <summary>
    /// Resets the selection, hiding the item description and deselecting all items.
    /// </summary>
    public void ResetSelection()
    {
        itemDescription.ResetDescription();
        DeselectAllItems();
    }

    public void ResetPage()
    {
        itemDescription.ResetDescription();
        DeselectAllItems();
        ResetAllItems();
    }

    /// <summary>
    /// Deselects all items (slots) in the inventory.
    /// </summary>
    private void DeselectAllItems()
    {
        foreach (InventorySlot slot in listOfSlots)
        {
            slot.Deselect();
        }
    }

    private void ResetAllItems()
    {
        foreach (InventorySlot slot in listOfSlots)
        {
            slot.ResetData();
        }
    }

    /// <summary>
    /// Updates the description panel with the specified item data and selects the corresponding item.
    /// </summary>
    /// <param name="itemIndex">Index of the item to update.</param>
    /// <param name="itemImage">Sprite representing the item.</param>
    /// <param name="name">Name of the item.</param>
    /// <param name="description">Description of the item.</param>
    internal void UpdateDescription(int itemIndex, Sprite itemImage, string name, string description)
    {
        // Set the description panel and select the corresponding item.
        itemDescription.SetDescription(itemImage, name, description);
        DeselectAllItems();
        listOfSlots[itemIndex].Select();

        UnityEngine.Debug.Log($"Description updated for item at index {itemIndex}.");
    }
}
