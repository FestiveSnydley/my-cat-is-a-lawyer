using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Represents an individual item (slot) within the inventory.
/// </summary>
public class InventorySlot : MonoBehaviour
{
    // Serialized fields allow private variables to be visible in the Unity Editor.
    [SerializeField]
    private Image slotImage;

    [SerializeField]
    private Image borderImage;

    // Event triggered when the item is clicked.
    public event Action<InventorySlot> OnItemClicked;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    public void Awake()
    {
        // Initialize the item by resetting data and deselecting.
        ResetData();
        Deselect();
    }

    /// <summary>
    /// Sets the item data, displaying the specified sprite.
    /// </summary>
    /// <param name="sprite">The sprite to be displayed.</param>
    public void SetData(Sprite sprite)
    {
        this.slotImage.gameObject.SetActive(true);
        this.slotImage.sprite = sprite;
    }

    /// <summary>
    /// Resets the item data, hiding the item image.
    /// </summary>
    public void ResetData()
    {
        this.slotImage.gameObject.SetActive(false);
    }


    /// <summary>
    /// Highlights the item by enabling its border.
    /// </summary>
    public void Select()
    {
        borderImage.enabled = true;
    }

    /// <summary>
    /// Removes the highlight from the item by disabling its border.
    /// </summary>
    public void Deselect()
    {
        borderImage.enabled = false;
    }

    /// <summary>
    /// Handles the pointer click event.
    /// </summary>
    /// <param name="data">Event data associated with the pointer click.</param>
    public void OnPointerClick(BaseEventData data)
    {
        PointerEventData pointerData = (PointerEventData)data;
        if (pointerData.button == PointerEventData.InputButton.Right)
        {
            OnItemClicked?.Invoke(this);
            Debug.Log("User has right clicked.");
        }
        else
        {
            OnItemClicked?.Invoke(this);
            Debug.Log("User has left clicked.");
        }
    }
}
