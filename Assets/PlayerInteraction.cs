using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    PlayerController controller;

    private List<Interactable> availableInteractions = new List<Interactable>();

    bool canInteract;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (availableInteractions.Count <= 0)
            canInteract = false;
        else
            canInteract = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Interactable"))
        {
            if(other.GetComponent<Interactable>())
                availableInteractions.Add(other.GetComponent<Interactable>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Interactable"))
        {
            if (other.GetComponent<Interactable>())
                RemoveInteraction(other.GetComponent<Interactable>());
        }        
    }

    public void RemoveInteraction(Interactable interactable)
    {
        //Checks if the object that left is in availableInteractions and if so remove it from the list 
        if (availableInteractions.Contains(interactable))
            availableInteractions.Remove(interactable);
    }

    public Interactable FindClosestInteractable()
    {
        float closestInt = 50f;
        Interactable temp_ClostestInteractable = null;
        foreach (Interactable interaction in availableInteractions)
        {
            float dist = Vector3.SqrMagnitude(transform.position - interaction.transform.position);

            if (dist < closestInt)
            {
                closestInt = dist;
                temp_ClostestInteractable = interaction;
            }
        }
        return temp_ClostestInteractable;
    }

    public void Interact()
    {
        if (canInteract)
        {
            FindClosestInteractable().Interaction(this);
        }
    }


}
