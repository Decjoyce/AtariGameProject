using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth_SecurityBot : EnemyHealth
{
    public override void TakeDamage(float amount)
    {
        base.TakeDamage(amount);
    }

    public override void Die()
    {
        Destroy(transform.parent.gameObject);
    }

}
