using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [SerializeField] int damage;

    private void Start()
    {
        Destroy(gameObject, 3f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("HIT " + collision.gameObject.name);
            collision.gameObject.GetComponent<EnemyHealthTest>().TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}
