using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ProximityTrigger : MonoBehaviour
{
    public delegate void ProximityEntered(Collider other);
    public delegate void ProximityExited(Collider other);
    public event ProximityEntered OnEnter;
    public event ProximityExited OnExit;

    [SerializeField] string[] checkTags;

    private void OnTriggerEnter(Collider other)
    {
        if (CheckTags(other.tag))
        {
            OnEnter(other);
            Debug.Log("diskd");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (CheckTags(other.tag))
        {
            OnExit(other);
        }
    }

    bool CheckTags(string theTag)
    {
        foreach(string s in checkTags)
        {
            if (theTag == s)
                return true;
        }
        return false;
    }
}
