using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private const float MAX_COOLDOWN = 0.15f;
    [SerializeField] private const int BULLET_CAP = 75;
    [SerializeField] private const float DURATION_TRIPLE_SHOT= 20;
    [SerializeField] private const float ANGLE_TRIPLE_SHOT = 30;
    [SerializeField] private float tripleShotCooldown = 0;
    [SerializeField] private float cooldown;
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
                SpawnBullet(0);
                cooldown += MAX_COOLDOWN;
                if (tripleShotCooldown > 0)
                {
                    SpawnBullet(-ANGLE_TRIPLE_SHOT);
                    SpawnBullet(ANGLE_TRIPLE_SHOT);
                }
            }
        }
    }

    public void AddTripleShotTime()
    {
        tripleShotCooldown += DURATION_TRIPLE_SHOT;
    }

    private void SpawnBullet(float angle)
    {
        foreach (GameObject bullet in bullets)
        {
            if(!bullet.activeSelf)
            {
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                bullet.transform.eulerAngles = player.transform.eulerAngles + new Vector3(0, angle, 0);
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
