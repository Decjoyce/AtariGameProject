using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class Elevator : Interactable
{
    List<PlayerController> playerAtLift = new List<PlayerController>();

    [SerializeField] bool bottomFloor;

    public override void Interaction(PlayerInteraction player)
    {
        base.Interaction(player);

        bottomFloor = !bottomFloor;

        MoveElevator();

        foreach (PlayerController play3r in playerAtLift)
        {
            //play3r.SwitchState("Elevator");
            play3r.rb.MovePosition(new Vector3(play3r.transform.position.x, transform.position.y, play3r.transform.position.z));
        }
    }

    void MoveElevator()
    {
        if (bottomFloor)
            transform.position = new(transform.position.x, transform.position.y - 22.25f, transform.position.z);
        else
            transform.position = new(transform.position.x, transform.position.y + 22.25f, transform.position.z);

        foreach (PlayerController play3r in playerAtLift)
        {
            //play3r.SwitchState("Elevator");
            play3r.rb.MovePosition(new Vector3(play3r.transform.position.x, transform.position.y, play3r.transform.position.z));
        }
    }

    public void CallElevator(bool floor)
    {
        if(bottomFloor != floor)
        {
            bottomFloor = floor;
            MoveElevator();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerAtLift.Add(other.GetComponent<PlayerController>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerAtLift.Add(other.GetComponent<PlayerController>());
        }
    }
}
