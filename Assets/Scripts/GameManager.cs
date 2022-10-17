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
    private BonusManager bonusManager;
    private BulletManager bulletManager;
    private MissileManager missileManager;
    private PlayerHealth playerHealth;

    void Start()
    {
        playerHealth = GameObject.Find("SpaceMarine").GetComponent<PlayerHealth>();
        bonusManager = GameObject.Find("BonusManager").GetComponent<BonusManager>();
        bulletManager = GameObject.Find("BulletManager").GetComponent<BulletManager>();
        missileManager = GameObject.Find("MissileManager").GetComponent<MissileManager>();
    }

    void Update()
    {
        livesText.text = playerHealth.GetLives().ToString();
        tripleShotText.text = bulletManager.GetTripleShotTime().ToString("0.0");
        missileText.text = missileManager.GetMissiles().ToString();
    }

    public void OnAlienKill(GameObject alien)
    {
        if(alien.CompareTag("Alien"))
        {
            bonusManager.SpawnBonus(alien.gameObject.transform.position);
        }
    }

    public void OnPlayerHit(GameObject player)
    {
        if (player.CompareTag("Player"))
        {
            if (!playerHealth.IsAlive()) return;
            if(!playerHealth.IsInvincible())
            {
                playerHealth.LoseLife();
                if(!IsAlive())
                    marineDeathSFX.Play();
                 else
                    marineHurtSFX.Play();
            }
        }
    }

    public void OnHealthBonusPickup()
    {
        playerHealth.AddLive();
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
        return playerHealth.IsAlive();
    }
}
