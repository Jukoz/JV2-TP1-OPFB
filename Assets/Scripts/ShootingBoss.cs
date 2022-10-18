using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingBoss : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private BulletManager bulletManager;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private float bossHeightOffset = 0;
    [SerializeField] private float playerHeightOffset = 20;
    [SerializeField] private float shootingCooldown;
    private const float SHOOTING_COOLDOWN_MAX = 0.5f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        shootingCooldown = SHOOTING_COOLDOWN_MAX;
        bulletManager = GameObject.Find("BulletManager").GetComponent<BulletManager>();
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    void FixedUpdate()
    {
        if (gameManager.IsGameOver()) return;
        shootingCooldown = Mathf.Max(0, shootingCooldown - Time.deltaTime);
        Vector3 gunPosition = transform.position + new Vector3(0, bossHeightOffset, 0);
        Vector3 playerPosition = player.transform.position + new Vector3(0, playerHeightOffset, 0);
        Vector3 difference = playerPosition - gunPosition;
        RaycastHit hit;
        Debug.DrawRay(gunPosition, difference, Color.red);
        if (Physics.Raycast(gunPosition, difference, out hit))
        {
            if(hit.collider.CompareTag("Player"))
            {
                if(shootingCooldown == 0)
                {
                    shootingCooldown = SHOOTING_COOLDOWN_MAX;
                    float distance = Vector2.Distance(new Vector2(playerPosition.x, playerPosition.z), new Vector2(gunPosition.x, gunPosition.z));
                    float xAngle = (Mathf.Atan2(distance, difference.y) * Mathf.Rad2Deg) - 90;
                    bulletManager.SpawnBullet(this.gameObject, new Vector3(0, bossHeightOffset, 0), new Vector3(xAngle, 0,0));
                }
            }
        }
    }
}
