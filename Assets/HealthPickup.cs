using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : Interactable
{
    [SerializeField] float healAmount;

    public override void Interaction(PlayerInteraction player)
    {
        base.Interaction(player);
        player.health.Heal(healAmount);
        player.RemoveInteraction(this);
        Destroy(gameObject);
    }
}
