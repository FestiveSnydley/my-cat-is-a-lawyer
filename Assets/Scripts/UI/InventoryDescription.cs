using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;
// Everything above is generated with Unity

///if UNITY_EDITOR
/// UnityEditor;
///endif

/// <summary>
/// Represents the description panel for an inventory item.
/// </summary>
public class InventoryDescription : MonoBehaviour
{
    // Serialized fields allow private variables to be visible in the Unity Editor.
    [SerializeField]
    private Image itemImage;

    [SerializeField]
    private TMP_Text title;

    [SerializeField]
    private TMP_Text description;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    public void Awake()
    {
        ResetDescription();
    }

    /// <summary>
    /// Resets the description panel, hiding the item image and clearing text fields.
    /// </summary>
    public void ResetDescription()
    {
        this.itemImage.gameObject.SetActive(false);
        this.title.text = "";
        this.description.text = "";

        ///SetTextAssetState(false);
    }

    /// <summary>
    /// Sets the description panel with the specified sprite, item name, and item description.
    /// </summary>
    /// <param name="sprite">The sprite to be displayed.</param>
    /// <param name="itemName">The name of the item.</param>
    /// <param name="itemDescription">The description of the item.</param>
    public void SetDescription(Sprite sprite, string itemName, string itemDescription)
    {
        this.itemImage.gameObject.SetActive(true);
        this.itemImage.sprite = sprite;
        this.title.text = itemName;
        this.description.text = itemDescription;
        /// SetTextAssetState(true);
    }
    /// void SetTextAssetState(bool state)
    ///{
    // Modify the text file content based on the state
    /// text = state ? "TRUE" : "FALSE";
    ///#if UNITY_EDITOR
    ///  System.IO.File.WriteAllText(AssetDatabase.GetAssetPath(itemActive), text);
    ///#endif
    ///EditorUtility.SetDirty(itemActive);
    ///    UnityEngine.Debug.Log("Text asset state set to: " + (state ? "TRUE" : "FALSE"));
    ///
    ///
    ///}
}