using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : Interactable
{
    public Item item;
    ScoreManager SM;
    public float sheetMetalWorth = 25f;

    private void Start()
    {
        interactPrompt = "Pick up " + item.itemName;
        SM = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>();
    }

    public override void Interaction(PlayerInteraction player)
    {
        base.Interaction(player);
        player.AddItemToInventory(item);
        SM.IncreaseScore(sheetMetalWorth);
        Destroy(gameObject);

    }
}
