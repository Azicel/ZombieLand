using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject[] enemyList;
    public Vector3[] spawnPointEnemy;
    private GameObject setEnemy;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i <= spawnPointEnemy.Length - 1; i++)
        {
            setEnemy = Instantiate(enemyList[Random.Range(0,enemyList.Length - 1)]);
            setEnemy.transform.position = spawnPointEnemy[i];
        }
    }
}
