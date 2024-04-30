using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class MoveToGoal : MonoBehaviour
{

    public float speed = 0.5f;
    public float accuracy = 0.5f;
    public Transform goal;

    void LateUpdate()
    {
        transform.LookAt(goal.position);
        Vector3 direction = goal.position - transform.position;

        Debug.DrawRay(transform.position, direction, Color.green);

        if(direction.magnitude > accuracy)
        {
            transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);
        }
    }
}
