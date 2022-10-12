using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // Start is called before the first frame 

    private GameObject[] spawners;
    void Start()
    {
        spawners = GameObject.FindGameObjectsWithTag("alien");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnEnemy()
    {
        
    }
}
