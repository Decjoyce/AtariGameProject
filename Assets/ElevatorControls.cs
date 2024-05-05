using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorControls : Interactable
{
    [SerializeField] Elevator elevator;
    [SerializeField] bool isBottomFloor;

    public override void Interaction(PlayerInteraction player)
    {
        base.Interaction(player);
        if (canInteract)
        {
            elevator.CallElevator(isBottomFloor);
        }
    }
}
