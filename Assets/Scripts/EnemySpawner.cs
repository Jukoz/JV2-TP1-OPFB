using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    private const float MAX_COOLDOWN = 1;

    private List<GameObject> spawners;
    [SerializeField] private float cooldown;
    [SerializeField] private GameObject enemyPefab;
    [SerializeField] private const int ENEMY_CAP = 20;
    [SerializeField] private GameManager gameManager;
    private List<GameObject> enemies;
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        spawners = GameObject.FindGameObjectsWithTag("Spawner").ToList();
        cooldown = MAX_COOLDOWN;
        enemies = new List<GameObject>();
        for (int i = 0; i < ENEMY_CAP; i++)
        {
            GameObject newEnemy = Instantiate(enemyPefab);
            newEnemy.GetComponent<EnemyMovement>().SetGameManager(gameManager);
            enemies.Add(newEnemy);
            newEnemy.SetActive(false);
        }
    }

    void Update()
    {
        cooldown = Mathf.Max(0, cooldown - Time.deltaTime);
        if(cooldown == 0)
        {
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        cooldown = MAX_COOLDOWN;
        foreach (GameObject enemy in enemies)
        {
            if(!enemy.activeSelf)
            {
                enemy.transform.position = spawners[Random.Range(0, spawners.Count)].transform.position;
                enemy.SetActive(true);
                break;
            }
        }
    }
}
