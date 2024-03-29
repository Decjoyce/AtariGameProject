using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    PlayerController controller;

    [SerializeField] float maxHealth;
    private float currentHealth;

    bool isDead;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<PlayerController>();
        currentHealth = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        if(currentHealth <= 0 && !isDead)
        {
            Debug.Log(gameObject.name + " HAS DIED");
            controller.SwitchState("DEATH");
            isDead = true;
        }
    }

    public void Heal(float amount)
    {
        currentHealth += amount;
        if (currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

}
