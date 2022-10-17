using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingBoss : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private BulletManager bulletManager;
    [SerializeField] private MissileManager missileManager;
    [SerializeField] private float bossHeightOffset = 15;
    [SerializeField] private float playerHeightOffset = 5;
    [SerializeField] private float shootingCooldown;
    private const float SHOOTING_COOLDOWN_MAX = 0.5f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        shootingCooldown = SHOOTING_COOLDOWN_MAX;
        bulletManager = GameObject.Find("BulletManager").GetComponent<BulletManager>();
        missileManager = GameObject.Find("MissileManager").GetComponent<MissileManager>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
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
                    Debug.Log("X: " + new Vector2(difference.x, difference.z).magnitude);
                    Debug.Log("Y: " + difference.y);
                    float xAngle = Mathf.Atan(new Vector2(difference.x, difference.z).magnitude / difference.y) * Mathf.PI;
                    bulletManager.SpawnBullet(this.gameObject, new Vector3(0, bossHeightOffset, 0), new Vector3(xAngle, 0, 0));
                }
                Debug.Log("halo");
            } else
            {
                Debug.Log("bye");
            } 
        }
    }
}
