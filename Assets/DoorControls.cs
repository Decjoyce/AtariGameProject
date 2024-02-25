using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControls : Interactable
{
    [SerializeField] Door door;

    public override void Interaction(PlayerInteraction player)
    {
        base.Interaction(player);
        door.UnlockDoor();
        player.RemoveInteraction(this);
        Destroy(this);
    }
}
