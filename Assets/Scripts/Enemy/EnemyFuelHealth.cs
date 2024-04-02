using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFuelHealth : EnemyHealth
{
    [SerializeField] GameObject explosion;

    public override void TakeDamage(float amount)
    {
        base.TakeDamage(amount);
    }

    public override void Die()
    {
        Instantiate(explosion, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
