using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard_Fire : MonoBehaviour
{
    [SerializeField] float damage;
    [SerializeField] float tickDelay = 0.2f;
    float timeBeforeNextTick;


    private void OnEnable()
    {
        PlayerHealth.OnPlayerDied += PlayerHasDied;
    }

    private void OnDisable()
    {
        PlayerHealth.OnPlayerDied -= PlayerHasDied;
    }

    List<PlayerHealth> playersInFire = new List<PlayerHealth>();
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playersInFire.Add(other.GetComponent<PlayerHealth>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(playersInFire.Contains(other.GetComponent<PlayerHealth>()))
                playersInFire.Remove(other.GetComponent<PlayerHealth>());
        }
    }

    private void Update()
    {
        if(playersInFire.Count > 0)
        {
            if (timeBeforeNextTick <= 0)
            {
                foreach (PlayerHealth health in playersInFire)
                {
                    health.TakeDamage(damage);
                }
                timeBeforeNextTick = tickDelay;
            }
            else
                timeBeforeNextTick -= Time.deltaTime;
        }
    }

    public void PlayerHasDied(GameObject player, int playerID)
    {
        if (playersInFire.Contains(player.GetComponent<PlayerHealth>()))
            playersInFire.Remove(player.GetComponent<PlayerHealth>());
    }

}
