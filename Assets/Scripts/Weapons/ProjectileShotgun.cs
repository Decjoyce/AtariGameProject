using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileShotgun : MonoBehaviour
{
    [SerializeField] GameObject pellets;
    [SerializeField] float angle, offset, delayB4Destroy;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, delayB4Destroy);
        for(int i = -4; i < 4; i++)
        {
            float newAng = i * angle;
            Vector3 spawnRot = transform.localEulerAngles + new Vector3(Random.Range(newAng - offset, newAng + offset), 0f, 0f);
            Instantiate(pellets, transform.position, Quaternion.Euler(spawnRot), transform);
        }  
    }
}
