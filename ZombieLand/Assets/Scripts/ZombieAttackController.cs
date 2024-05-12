using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAttackController : MonoBehaviour
{
    private float timeBetweenAttacks = 1f;
    private float time = 0f;
    private PlayerController player;


    void FixedUpdate()
    {
        time += Time.fixedDeltaTime;
        if (time > timeBetweenAttacks)
            if (player is not null)
            {
                player.RecieveDamage();
                time = 0;
            }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.GetComponent<PlayerController>();
            player.RecieveDamage();
            time = 0;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = null;
        }
    }
}
