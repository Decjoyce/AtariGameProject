using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : Interactable
{
    public Item item;
    public bool useRandomItem;
    public SO_RandomItems randomItems;

    private void Start()
    {
        if (useRandomItem)
            item = randomItems.GetRandomItem();
        interactPrompt = "Pick up " + item.itemName;
        Instantiate(item.graphics, transform.GetChild(0));
    }

    public override void Interaction(PlayerInteraction player)
    {
        base.Interaction(player);
        player.AddItemToInventory(item);
        ScoreManager.instance.IncreaseFakeQuota(item.value);
        Destroy(gameObject);

    }
}
