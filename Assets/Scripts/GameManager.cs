using UnityEngine;
using TMPro;
using System.Collections;

public class GameManager : MonoBehaviour
{
    [SerializeField] private AudioSource marineHurtSFX;
    [SerializeField] private AudioSource marineDeathSFX;
    [SerializeField] private AudioSource gameMusic;
    [SerializeField] private AudioSource victoryMusic;
    [SerializeField] private TMP_Text livesText;
    [SerializeField] private TMP_Text tripleShotText;
    [SerializeField] private TMP_Text missileText;
    [SerializeField] private TMP_Text victoryText;
    private EnemySpawner spawnerManager;
    private BonusManager bonusManager;
    private BulletManager bulletManager;
    private MissileManager missileManager;
    private PlayerHealth playerHealth;
    private const float FADE_TIME = 3;
    private bool gameOver;

    void Start()
    {
        gameOver = false;
        gameMusic.Play();
        playerHealth = GameObject.Find("SpaceMarine").GetComponent<PlayerHealth>();
        bonusManager = GameObject.Find("BonusManager").GetComponent<BonusManager>();
        bulletManager = GameObject.Find("BulletManager").GetComponent<BulletManager>();
        missileManager = GameObject.Find("MissileManager").GetComponent<MissileManager>();
        spawnerManager = GameObject.Find("SpawnerManager").GetComponent<EnemySpawner>();
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
            bonusManager.SpawnBonus(alien.gameObject.transform.position);
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

    public void OnSpawnerKill(GameObject spawner)
    {
        spawnerManager.RemoveSpawner(spawner);
    }

    public void Victory()
    {
        victoryText.gameObject.SetActive(true);
        gameOver = true;
        FadeMusic(FADE_TIME);
    }

    public bool IsGameOver()
    {
        if (gameOver) return true;
        else return !IsAlive();
    }

    IEnumerator FadeMusic(float time)
    {
        float loopTime = time / 10;
        for (float volume = 1f; volume >= 0; volume -= 0.1f)
        {
            gameMusic.volume = volume;
            yield return new WaitForSeconds(loopTime);
        }
        gameMusic.Stop();
        victoryMusic.Play();
    }
}
