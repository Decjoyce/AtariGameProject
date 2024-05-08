using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : Interactable
{
    [SerializeField] float healAmount;
    [SerializeField] int amountOfHealthPacks = 1;

    public override void Interaction(PlayerInteraction player)
    {
        base.Interaction(player);
        player.health.Heal(healAmount);
        amountOfHealthPacks -= 1;
        if(amountOfHealthPacks <= 0)
            Destroy(gameObject);
    }
}
