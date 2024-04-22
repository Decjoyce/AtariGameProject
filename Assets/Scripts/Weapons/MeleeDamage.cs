using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeDamage : MonoBehaviour
{
    [SerializeField] float damage;
    public Collider col;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyHitbox"))
        {
            other.GetComponent<EnemyHealth>().TakeDamage(damage);
        }       
    }
}
