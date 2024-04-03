using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exploision : MonoBehaviour
{
    [SerializeField] float damage, growSpeed, explosionTime;

    private void Start()
    {
        Destroy(gameObject, explosionTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale += Vector3.one * growSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerHealth>().TakeDamage(damage);
        }
        if (other.CompareTag("EnemyHitbox"))
        {
            other.GetComponent<EnemyHealth>().TakeDamage(damage * 4);
        }
    }
}
