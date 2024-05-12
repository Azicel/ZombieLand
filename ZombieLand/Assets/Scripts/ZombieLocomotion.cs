using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Windows;

public class ZombieLocomotion : MonoBehaviour
{
    Animator animator;
    float speed;
    void Start()
    {
        animator = GetComponent<Animator>();

    }
    void Update()
    {
        speed = GetComponent<NavMeshAgent>().speed;
        animator.SetFloat("Speed", speed);
    }
}
