using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] float damage;
    [SerializeField] float forwardForce, upwardForce,torqueForce; //Upwards force dont work bc up is right and right is up and when pivot rotates it messes it up and stuff so ye need to fix basically
    [SerializeField] float delayBeforeDelete = 3f;
    [SerializeField] float range;

    Vector3 startPos;

    private void Start()
    {
        rb.AddForce((transform.right * -upwardForce) + (transform.up * forwardForce), ForceMode.Impulse);
        rb.AddTorque(transform.forward * torqueForce);
        startPos = transform.position;
        Destroy(gameObject, delayBeforeDelete);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("HIT " + collision.gameObject.name);
            float dist = Vector3.Distance(transform.position, startPos);
            if (dist < range)
            {
                collision.gameObject.GetComponent<EnemyHealthTest>().TakeDamage(damage);
                Debug.Log("IN RANGE - Dam: " + damage + " Dist: " + dist);
            }
            else
            {
                float newDam = damage / ((dist * dist) / (range * range));
                collision.gameObject.GetComponent<EnemyHealthTest>().TakeDamage(newDam);
                Debug.Log("OUT OF RANGE - Dam: " + newDam + " Dist:" + dist);
            }

        }
        Debug.Log("HIT " + collision.gameObject.name);
        Destroy(gameObject);
    }
}
