using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using static Unity.VisualScripting.Member;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float maxHealth;
    public float currentHealth;
    public bool isDead;

    [SerializeField] MeshRenderer[] healthMeshes; //temp
    [SerializeField] Gradient healthColorGradient;

    [SerializeField] GameObject droppedItem;
    [SerializeField] LayerMask ignoreLayers;

    [SerializeField] AudioClip hitSound, deathSound;
    public AudioSource source;

    private void Start()
    {
    }

    public virtual void TakeDamage(float amount)
    {
        currentHealth -= amount;
        if (!isDead && currentHealth <= 0)
        {
            Die();
        }
        else
            source.PlayOneShot(hitSound);
        SetHealthColor();
    }

    public virtual void Die()
    {
            Instantiate(droppedItem, transform.position, Quaternion.identity);

        isDead = true;
        source.PlayOneShot(deathSound);

        Destroy(transform.parent.gameObject);
    }

    void SetHealthColor()
    {
        foreach (MeshRenderer mr in healthMeshes)
        {
            mr.material.SetColor("_EmissionColor", healthColorGradient.Evaluate((maxHealth - currentHealth) / maxHealth));
        }
    }
}
