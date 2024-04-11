using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Items/New Item")]
public class Item : ScriptableObject
{
    public string itemName;
    public int value;
    public ItemRarity rarity;
    public GameObject graphics;
}

public enum ItemRarity
{
    common,
    uncommon, 
    rare,
    veryRare,
    scarce,
    unique,
    doesNotSpawn
}