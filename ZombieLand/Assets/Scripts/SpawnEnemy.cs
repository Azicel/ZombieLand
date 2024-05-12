using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject[] enemyList;
    public GameObject[] spawnPoints;
    private GameObject setEnemy;
    [SerializeField] private int maxEnemyCount = 5;
    [SerializeField] private GameObject player;
    private float timeToMoreEnemies = 0;
    public int currentEnemycount = 0;
    // Start is called before the first frame update
    void Start()
    {
        for (currentEnemycount = 0; currentEnemycount < maxEnemyCount; currentEnemycount++)
        {
            spawnEnemy();
        }
    }

    void FixedUpdate()
    {
        if (currentEnemycount < maxEnemyCount)
        {
            spawnEnemy();
            currentEnemycount++;
        }
        if (Mathf.Floor(Time.realtimeSinceStartup / 100) > maxEnemyCount%5)
            maxEnemyCount++;
    }

    void spawnEnemy()
    {
        setEnemy = Instantiate(enemyList[Random.Range(0, enemyList.Length)]);
        setEnemy.transform.position = spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position;
        setEnemy.GetComponent<EnemyController>().player = player.transform;
    }
}
