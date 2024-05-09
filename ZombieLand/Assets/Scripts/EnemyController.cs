using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    
    public Transform player;
    private NavMeshAgent agent;
    private float health = 3f;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(player.position);
        if (health <= 0)
            Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Arrow"))
        {
            health--;
            Destroy(collision.gameObject);
            if(collision.gameObject.GetComponent<ArrowController>().force >= 100f)
            {
                collision.gameObject.GetComponent<ArrowController>().health--;
                health--;
            }
        }
    }
}
