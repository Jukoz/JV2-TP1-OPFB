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

    void Start()
    {
        lives = 5;
        alive = true;
    }

    void Update()
    {
        livesText.text = lives.ToString();
    }

    public void OnAlienHit(GameObject alien)
    {
        if(alien.CompareTag("Alien"))
        {
            EnemyMovement enemyMovement = alien.GetComponent<EnemyMovement>();
            kills++;
            enemyMovement.Kill();
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
                Debug.Log("HURT");
                marineHurtSFX.Play();
            }
        }
    }

    public bool isAlive()
    {
        return alive;
    }
}
