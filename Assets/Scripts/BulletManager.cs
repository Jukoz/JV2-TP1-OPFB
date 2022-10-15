using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private float cooldown;
    [SerializeField] private const float MAX_COOLDOWN = 0.15f;
    [SerializeField] private const int BULLET_CAP = 50;
    [SerializeField] private const float timeOnTripleShotPickup= 20;
    [SerializeField] private float tripleShotCooldown = 0;
    private GameManager gameManager;
    private List<GameObject> bullets;

    void Start()
    {
        cooldown = 0;
        bullets = new List<GameObject>();
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        for (int i = 0; i < BULLET_CAP; i++)
        {
            GameObject newBullet = Instantiate(prefab);
            bullets.Add(newBullet);
            newBullet.SetActive(false);
        }
    }

    void Update()
    {
        if (!gameManager.IsAlive()) return;
        cooldown = Mathf.Max(0, cooldown - Time.deltaTime);
        tripleShotCooldown = Mathf.Max(0, tripleShotCooldown - Time.deltaTime);
        if (Input.GetButton("Fire1"))
        {
            if(cooldown == 0)
            {
                SpawnBullet(Vector3.zero);
                cooldown += MAX_COOLDOWN;
                if (tripleShotCooldown > 0)
                {
                    SpawnBullet(Vector3.left);
                    SpawnBullet(Vector3.right);
                }
            }
        }
    }

    public void AddTripleShotTime()
    {
        tripleShotCooldown += timeOnTripleShotPickup;
    }

    private void SpawnBullet(Vector3 offset)
    {
        foreach (GameObject bullet in bullets)
        {
            if(!bullet.activeSelf)
            {
                bullet.GetComponent<Bullet>().EnableBullet(offset);
                break;
            }
        }
    }
}
