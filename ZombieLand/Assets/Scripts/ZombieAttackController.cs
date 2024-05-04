using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAttackController : MonoBehaviour
{
    private float timeBetweenAttacks = 1f;
    private float time = 0f;
    private PlayerController player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > timeBetweenAttacks)
            if (player is not null)
                player.RecieveDamage();
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
