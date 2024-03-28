using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthTest : MonoBehaviour
{
    public float currentHealth;

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        if(currentHealth <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
