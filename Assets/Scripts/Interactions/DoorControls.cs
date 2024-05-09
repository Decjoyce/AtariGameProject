using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControls : Interactable
{
    [SerializeField] Door door;
    [SerializeField] Renderer panel;
    [SerializeField] Material unlockedMat;
    [SerializeField] public bool engineerDoor;
    [SerializeField] public float hackDelay;

    private void Start()
    {

    }

    public override void Interaction(PlayerInteraction player)
    {
        if (canInteract && !engineerDoor)
        {
            hackDelay = hackDelay + 5 - (player.controller.playerStats.hackSpeedMod);
            base.Interaction(player);
            door.Invoke("UnlockDoor", hackDelay);
            Debug.Log("Door hacked open after delay");
            panel.material = unlockedMat;
            canInteract = false;
        }

        if (canInteract && engineerDoor)
        {
            if(player.isEngineer)
            {
                hackDelay = hackDelay - player.controller.playerStats.hackSpeedMod;
                base.Interaction(player);
                door.Invoke("UnlockDoor", hackDelay);
                panel.material = unlockedMat;
                canInteract = false;
            }

        }

    }
}
