using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ScriptableObject representing an item in the game.
/// </summary>
[CreateAssetMenu]
public class ItemSO : ScriptableObject
{
    public int ID => GetInstanceID();

    // Serialized fields allow private variables to be visible in the Unity Editor.
    [field: SerializeField]
    public string Name { get; set; }

    [field: SerializeField]
    public string InternalName { get; set; }

    [field: SerializeField]
    [field: TextArea]
    public string Description { get; set; }

    [field: SerializeField]
    public Sprite ItemImage { get; set; }

}
