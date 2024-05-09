using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public delegate void PlayerDied(GameObject player, int playerID);
    public static event PlayerDied OnPlayerDied;
    

    PlayerController controller;

    [SerializeField] float maxHealth;
    public float currentHealth;

    [SerializeField] MeshRenderer[] healthMeshes; //temp
    [SerializeField] SkinnedMeshRenderer[] NEWhealthMeshes; //Will replace healthMeshes eventually
    [SerializeField] Gradient healthColorGradient;

    public Slider healthSlider;

    bool isDead;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<PlayerController>();
        currentHealth = maxHealth;
        SetHealthColor();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
            currentHealth = 0;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;

            healthSlider.value = currentHealth;

        if (currentHealth <= 0 && !isDead)
        {
            Die();
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
            healthSlider.value = currentHealth;

        SetHealthColor();
    }

    public void Die()
    {
        Debug.Log(gameObject.name + " HAS DIED");
        controller.SwitchState("DEATH");
        isDead = true;
        OnPlayerDied(gameObject, controller.playerNum);
    }

    public void Revive()
    {
        isDead = false;
        SetHealthColor();
    }

    public void SetHealthColor()
    {
        foreach(SkinnedMeshRenderer mr in NEWhealthMeshes)
        {
            mr.material.SetColor("_EmissionColor", healthColorGradient.Evaluate((maxHealth - currentHealth) / maxHealth));
        }
        foreach (MeshRenderer mr in healthMeshes)
        {
            mr.material.SetColor("_EmissionColor", healthColorGradient.Evaluate((maxHealth - currentHealth) / maxHealth));
        }
    }

}
