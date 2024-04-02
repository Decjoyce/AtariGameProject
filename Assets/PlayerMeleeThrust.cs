using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeleeThrust : MonoBehaviour
{
    [SerializeField] CapsuleCollider col;
    [SerializeField] int damage;
    [SerializeField] float range, speed;

    private void Start()
    {
        Destroy(gameObject, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        float offset = Time.deltaTime * speed;
        col.height += offset;
        transform.localPosition = new(0, transform.localPosition.y + offset/2, 0);
        if (col.height >= range)
            Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("HIT " + other.gameObject.name);
            other.gameObject.GetComponent<EnemyHealth>().TakeDamage(damage);
        }
    }

}
