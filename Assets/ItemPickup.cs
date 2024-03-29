using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : Interactable
{
    public Item item;

    private void Start()
    {
        interactPrompt = "Pick up " + item.itemName;
    }

    public override void Interaction(PlayerInteraction player)
    {
        base.Interaction(player);
        player.AddItemToInventory(item);
        Destroy(gameObject);
    }
}
