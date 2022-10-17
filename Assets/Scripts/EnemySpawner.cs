using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    private const float MAX_COOLDOWN = 1;

    private const int ENEMY_CAP = 25;
    [SerializeField] private float cooldown;
    [SerializeField] private GameObject enemyPefab;
    [SerializeField] private int ENEMY_GAME_CAP = 20;
    [SerializeField] private int ENEMY_TOTAL_CAP = 100;
    [SerializeField] private int spawnedEnemies = 0;
    [SerializeField] private GameManager gameManager;
    private List<GameObject> spawners;
    private List<GameObject> leftSpawners;
    private List<GameObject> enemies;
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        spawners = GameObject.FindGameObjectsWithTag("Spawner").ToList();
        foreach (GameObject spawner in spawners)
        {
            spawner.GetComponent<SpawnerHealth>().SetGameManager(gameManager);    
        }
        cooldown = MAX_COOLDOWN;
        enemies = new List<GameObject>();
        for (int i = 0; i < ENEMY_CAP; i++)
        {
            GameObject newEnemy = Instantiate(enemyPefab);
            newEnemy.GetComponent<EnemyHealth>().SetGameManager(gameManager);
            enemies.Add(newEnemy);
            newEnemy.SetActive(false);
        }
    }

    void Update()
    {
        cooldown = Mathf.Max(0, cooldown - Time.deltaTime);
        if (cooldown == 0 && GetRemainingEnemies() <= ENEMY_GAME_CAP)
        {
            if(spawners.Count > 0)
            {
                int randomIndex = Random.Range(0, spawners.Count);
                Vector3 position = spawners[randomIndex].transform.position;
                SpawnEnemy(position);
            } else
            {
                if(GetRemainingEnemies() == 0)
                {
                    Debug.Log("WIN");
                }
            }
        }
    }

    public void RemoveSpawner(GameObject spawner)
    {
        spawners.Remove(spawner);
    }

    private void SpawnEnemy(Vector3 position)
    {
        if (spawnedEnemies >= ENEMY_TOTAL_CAP) return;
        cooldown = MAX_COOLDOWN;
        foreach (GameObject enemy in enemies)
        {
            if(!enemy.activeSelf)
            {
                spawnedEnemies++;
                enemy.transform.position = position;
                enemy.SetActive(true);
                break;
            }
        }
    }

    private int GetRemainingEnemies()
    {
        int total = 0;
        foreach (GameObject enemy in enemies)
        {
            if (enemy.activeSelf)
            {
                total++;
            }
        }
        return total;
    }
}
