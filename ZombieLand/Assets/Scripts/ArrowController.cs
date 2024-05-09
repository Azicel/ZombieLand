using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    public float force;
    public float health = 2f;
    private float lifetime = 20f;
    private float time = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if(time > lifetime)
            Destroy(gameObject);
        if (health <= 0f)
            Destroy(gameObject);
    }
}
