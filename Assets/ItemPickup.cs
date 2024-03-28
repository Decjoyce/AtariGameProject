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
        //Code For Inventory Stuff
        player.RemoveInteraction(this);
        Destroy(gameObject);
    }
}
