using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float maxHealth;
    public float currentHealth;

    [SerializeField] MeshRenderer[] healthMeshes; //temp
    [SerializeField] Gradient healthColorGradient;

    [SerializeField] GameObject droppedItem;
    [SerializeField] LayerMask ignoreLayers;
 
    public virtual void TakeDamage(float amount)
    {
        currentHealth -= amount;
        if(currentHealth <= 0)
        {
            Die();
        }
        SetHealthColor();
    }

    public virtual void Die()
    {
            Instantiate(droppedItem, transform.position, Quaternion.identity);

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
