using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHelper : MonoBehaviour
{
    [SerializeField] AudioSource source;
    public void HelpPlaySound(AudioClip clip)
    {
        source.PlayOneShot(clip);
    }
}
