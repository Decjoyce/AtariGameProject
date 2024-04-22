using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControls : Interactable
{
    [SerializeField] Door door;
    [SerializeField] Renderer panel;
    [SerializeField] Material unlockedMat;
    [SerializeField] public bool engineerDoor;

    public override void Interaction(PlayerInteraction player)
    {
        if (canInteract && !engineerDoor)
        {
            base.Interaction(player);
            door.UnlockDoor();
            panel.material = unlockedMat;
            canInteract = false;
        }

        if (canInteract && engineerDoor)
        {
            if(player.isEngineer)
            {
                base.Interaction(player);
                door.UnlockDoor();
                panel.material = unlockedMat;
                canInteract = false;
            }

        }

    }
}
