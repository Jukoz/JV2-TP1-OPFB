using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private GameObject player;
    [SerializeField] private float tripleShotCooldown = 0;
    [SerializeField] private float cooldown;
    [SerializeField] private AudioSource shootSFX;
    [SerializeField] private AudioSource tripleSFX;
    private Vector3 spawnPointOffset = new Vector3(0f, 3.5f, 0f);
    private const float MAX_COOLDOWN = 0.15f;
    private const int BULLET_CAP = 75;
    private const float DURATION_TRIPLE_SHOT= 20;
    private const float ANGLE_TRIPLE_SHOT = 30;
    private GameManager gameManager;
    private List<GameObject> bullets;

    void Start()
    {
        cooldown = 0;
        bullets = new List<GameObject>();
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        player = GameObject.FindGameObjectWithTag("Player");
        for (int i = 0; i < BULLET_CAP; i++)
        {
            GameObject newBullet = Instantiate(prefab);
            newBullet.transform.parent = this.transform.parent;
            bullets.Add(newBullet);
            newBullet.SetActive(false);
        }
    }

    void Update()
    {
        if (!gameManager.IsAlive()) return;
        cooldown = Mathf.Max(0, cooldown - Time.deltaTime);
        tripleShotCooldown = Mathf.Max(0, tripleShotCooldown - Time.deltaTime);
        if (Input.GetButton("Fire1") && !Input.GetButton("Fire2"))
        {
            if(cooldown == 0)
            {
                player = GameObject.FindGameObjectWithTag("Player");
                Vector3 newSpawnOffset = (player.transform.forward * 10f) + spawnPointOffset;
                transform.position = player.transform.position + newSpawnOffset;
                SpawnBullet(player, newSpawnOffset, Vector3.zero);
                cooldown += MAX_COOLDOWN;
                if (tripleShotCooldown > 0)
                {
                    tripleSFX.Play();
                    SpawnBullet(player, newSpawnOffset, new Vector3(0, -ANGLE_TRIPLE_SHOT, 0));
                    SpawnBullet(player, newSpawnOffset, new Vector3(0, ANGLE_TRIPLE_SHOT, 0));
                } else
                {
                    shootSFX.Play();
                }
            }
        }
    }

    public void AddTripleShotTime()
    {
        tripleShotCooldown += DURATION_TRIPLE_SHOT;
    }

    public void SpawnBullet(GameObject shooter, Vector3 offset, Vector3 angle)
    {
        foreach (GameObject bullet in bullets)
        {
            if(!bullet.activeSelf)
            {
                bullet.transform.position = shooter.transform.position + offset;
                bullet.transform.eulerAngles = shooter.transform.eulerAngles + angle;
                bullet.SetActive(true);
                break;
            }
        }
    }

    public float GetTripleShotTime()
    {
        return tripleShotCooldown;
    }
}
