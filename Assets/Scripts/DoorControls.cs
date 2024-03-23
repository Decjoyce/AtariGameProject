using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControls : Interactable
{
    [SerializeField] Door door;
    [SerializeField] Renderer panel;
    [SerializeField] Material unlockedMat;

    public override void Interaction(PlayerInteraction player)
    {
        if (canInteract)
        {
            base.Interaction(player);
            door.UnlockDoor();
            panel.material = unlockedMat;
            canInteract = false;
        }

    }
}
