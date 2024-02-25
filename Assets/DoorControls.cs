using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControls : Interactable
{
    [SerializeField] Door door;

    public override void Interaction(PlayerInteraction player)
    {
        if (canInteract)
        {
            base.Interaction(player);
            door.UnlockDoor();
            canInteract = false;
        }

    }
}
