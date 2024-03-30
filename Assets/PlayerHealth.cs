using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    PlayerController controller;

    [SerializeField] float maxHealth;
    private float currentHealth;

    [SerializeField] MeshRenderer[] healthMeshes; //temp
    [SerializeField] Gradient healthColorGradient;

    bool isDead;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<PlayerController>();
        currentHealth = maxHealth;
        SetHealthColor();
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
        SetHealthColor();
    }

    public void Heal(float amount)
    {
        currentHealth += amount;
        if (currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
        }
        SetHealthColor();
    }

    void SetHealthColor()
    {
        foreach(MeshRenderer mr in healthMeshes)
        {
            mr.material.SetColor("_EmissionColor", healthColorGradient.Evaluate((maxHealth - currentHealth) / maxHealth));
        }

    }

}
