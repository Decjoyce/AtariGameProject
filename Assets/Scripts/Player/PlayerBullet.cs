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
    bool madeContact;

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
        if (!madeContact)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                DamageEnemy(collision);
            }

            if (collision.gameObject.CompareTag("CritSpot"))
            {
                DamageCritSpot(collision);
            }
        }
        Debug.Log("HIT " + collision.gameObject.name);
        madeContact = true;
        Destroy(gameObject);
    }

    void DamageEnemy(Collision collision)
    {
            Debug.Log("HIT " + collision.gameObject.name);
            float dist = Vector3.Distance(transform.position, startPos);
            if (dist < range)
            {
                collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(damage);
                //Debug.Log("IN RANGE - Dam: " + damage + " Dist: " + dist);
            }
            else
            {
                float newDam = damage / ((dist * dist) / (range * range));
                collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(newDam);
                //Debug.Log("OUT OF RANGE - Dam: " + newDam + " Dist:" + dist);
            }
    }

    void DamageCritSpot(Collision collision)
    {
        Debug.Log("HIT " + collision.gameObject.name);
        float dist = Vector3.Distance(transform.position, startPos);
        if (dist < range)
        {
            collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(damage * 2f);
            //Debug.Log("IN RANGE - Dam: " + damage + " Dist: " + dist);
        }
        else
        {
            float newDam = damage / ((dist * dist) / (range * range));
            collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(newDam * 2f);
            //Debug.Log("OUT OF RANGE - Dam: " + newDam + " Dist:" + dist);
        }
    }

}
