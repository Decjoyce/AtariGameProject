using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float currentHealth;

    public virtual void TakeDamage(float amount)
    {
        currentHealth -= amount;
        if(currentHealth <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    public virtual void Die()
    {
        gameObject.SetActive(false);
    }
}
