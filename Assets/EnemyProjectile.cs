using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] int damage;
    [SerializeField] float forwardForce, upwardForce, torqueForce; //Upwards force dont work bc up is right and right is up and when pivot rotates it messes it up and stuff so ye need to fix basically
    [SerializeField] float delayBeforeDelete = 3f;

    private void Start()
    {
        rb.AddForce((transform.right * upwardForce) + (transform.up * forwardForce), ForceMode.Impulse);
        rb.AddTorque(transform.forward * torqueForce);
        Destroy(gameObject, delayBeforeDelete);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //Debug.Log("HIT " + collision.gameObject.name);
            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
        }
        //Debug.Log("HIT " + collision.gameObject.name);
        Destroy(gameObject);
    }
}
