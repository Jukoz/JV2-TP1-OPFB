using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private List<GameObject> bullets;
    [SerializeField] private float cooldown;
    [SerializeField] private const float MAX_COOLDOWN = 0.15f;
    [SerializeField] private const int BULLET_CAP = 50;

    void Start()
    {
        cooldown = 0;
        bullets = new List<GameObject>();
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        for (int x = 0; x < BULLET_CAP; x++)
        {
            GameObject newBullet = Instantiate(prefab);
            newBullet.GetComponent<Bullet>().SetGameManager(gameManager);
            bullets.Add(newBullet);
            newBullet.SetActive(false);
        }
    }

    void Update()
    {
        if (!gameManager.isAlive()) return;
        cooldown = Mathf.Max(0, cooldown - Time.deltaTime);
        if (Input.GetButton("Fire1"))
        {
            if(cooldown == 0)
            {
                cooldown = MAX_COOLDOWN;
                foreach (GameObject bullet in bullets)
                {
                    if(!bullet.activeSelf)
                    {
                        bullet.SetActive(true);
                        break;
                    }
                }
            }
        }
    }
}
