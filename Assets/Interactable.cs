using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public bool canInteract;

    private void OnTriggerEnter(Collider other)
    {
        if (canInteract && other.CompareTag("Player"))
        {
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {

        }
    }
}
