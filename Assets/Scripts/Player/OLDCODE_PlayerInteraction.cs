
/////////
/// OLD CODE ONLY HERE IF SOMETHING BREAKS
//////




/*using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractionOLD : MonoBehaviour
{
    public delegate void InteractedWithSomething(Interactable other);
    public static event InteractedWithSomething OnInteractedWith;

    [HideInInspector] public PlayerController controller;
    [HideInInspector] public PlayerAttack attack;
    [HideInInspector] public PlayerHealth health;

    private List<Interactable> availableInteractions = new List<Interactable>();

    bool canInteract;
    public bool isEngineer;

    [SerializeField] TextMeshProUGUI interactionText;

    List<Item> inventory = new List<Item>();

    Coroutine coroutine_findingInteractions;

    [SerializeField] float findInteractionDelay;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<PlayerController>();
        attack = GetComponent<PlayerAttack>();
        health = GetComponent<PlayerHealth>();
        coroutine_findingInteractions = StartCoroutine(FindInteractionsWithDelay());
    }

    private void OnEnable()
    {
        OnInteractedWith += RemoveInteraction;
    }

    private void OnDisable()
    {
        OnInteractedWith -= RemoveInteraction;
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
            SetInteractionText();
        }
        if (isEngineer && other.CompareTag("eng_Interactable"))
        {
            if (other.GetComponent<Interactable>())
                availableInteractions.Add(other.GetComponent<Interactable>());
            SetInteractionText();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Interactable"))
        {
            if (other.GetComponent<Interactable>())
                RemoveInteraction(other.GetComponent<Interactable>());
        }
        if (isEngineer && other.CompareTag("eng_Interactable"))
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
        SetInteractionText();
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
            Interactable closestInt = FindClosestInteractable();
            closestInt.Interaction(this);
            SetInteractionText();

            OnInteractedWith.Invoke(closestInt);
        }
    }

    void SetInteractionText()
    {
        Interactable closestInt = FindClosestInteractable();
        if (closestInt != null && closestInt.canInteract)
            interactionText.text = FindClosestInteractable().interactPrompt;
        else
            interactionText.text = null;
    }

    IEnumerator FindInteractionsWithDelay()
    {
        while (true)
        {
            yield return new WaitForSeconds(findInteractionDelay);
            SetInteractionText();
        }
    }

    public void AddItemToInventory(Item item)
    {
        inventory.Add(item);
    }

    public void ClearInventory()
    {
        inventory.Clear();
    }

}
*/