using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private AudioSource marineHurtSFX;
    [SerializeField] private AudioSource marineDeathSFX;
    [SerializeField] private TMP_Text livesText;
    [SerializeField] private int kills = 0;
    [SerializeField] private int lives;
    [SerializeField] private bool alive;
    private EnemySpawner _enemySpawner; 

    void Start()
    {
        lives = 5;
        alive = true;
        _enemySpawner = GameObject.FindWithTag("SpawnerManager").GetComponent<EnemySpawner>();
    }

    void Update()
    {
        livesText.text = lives.ToString();
    }

    public void OnAlienHit(GameObject alien)
    {
        if(alien.CompareTag("Alien"))
        {
            EnemyHealth enemy = alien.GetComponent<EnemyHealth>();
            enemy.Hit();
            if(!enemy.IsAlive()) kills++;
        }
    }

    public void OnSpawnerHit(GameObject spawner)
    {
        if (spawner.CompareTag("Spawner"))
        {   
            Debug.Log("good");
            SpawnerHealth spawnerHealth = spawner.GetComponent<SpawnerHealth>();
            spawnerHealth.Hit();
            if (!spawnerHealth.IsAlive()) _enemySpawner.RemoveSpawner(spawner);
        }
    }

    public void OnPlayerHit(GameObject player)
    {
        if (player.CompareTag("Player"))
        {
            PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
            lives--;
            if(lives == 0)
            {
                playerMovement.Kill();
                marineDeathSFX.Play();
                alive = false;
            } else
            {
                marineHurtSFX.Play();
            }
        }
    }

    public bool isAlive()
    {
        return alive;
    }
}
