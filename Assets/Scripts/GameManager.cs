using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private AudioSource marineHurtSFX;
    [SerializeField] private AudioSource marineDeathSFX;
    [SerializeField] private TMP_Text livesText;
    [SerializeField] private TMP_Text tripleShotText;
    [SerializeField] private TMP_Text missileText;
    [SerializeField] private int kills = 0;
    [SerializeField] private int lives;
    [SerializeField] private bool alive;
    private BonusManager bonusManager;
    private BulletManager bulletManager;
    private MissileManager missileManager;

    void Start()
    {
        lives = 5;
        alive = true;
        bonusManager = GameObject.Find("BonusManager").GetComponent<BonusManager>();
        bulletManager = GameObject.Find("BulletManager").GetComponent<BulletManager>();
        missileManager = GameObject.Find("MissileManager").GetComponent<MissileManager>();
    }

    void Update()
    {
        livesText.text = lives.ToString();
        tripleShotText.text = bulletManager.GetTripleShotTime().ToString("0.0");
    }

    public void OnAlienKill(GameObject alien)
    {
        if(alien.CompareTag("Alien"))
        {
            EnemyMovement enemyMovement = alien.GetComponent<EnemyMovement>();
            bonusManager.SpawnBonus(alien.gameObject.transform.position);
            kills++;
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

    public void OnHealthBonusPickup()
    {
        lives++;
    }

    public void OnMissilePickup()
    {
        missileManager.AddMissiles();
    }

    public void OnTripleShotPickup()
    {
        bulletManager.AddTripleShotTime();
    }

    public bool IsAlive()
    {
        return alive;
    }
}
