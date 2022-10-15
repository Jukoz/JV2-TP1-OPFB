using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileManager : MonoBehaviour
{
    [SerializeField] private float cooldown;
    [SerializeField] private const int MISSILE_CAP = 10;
    [SerializeField] private const float MAX_COOLDOWN = 2;
    [SerializeField] private GameObject prefab;
    private List<GameObject> missiles;
    private GameManager gameManager;
    void Start()
    {
        cooldown = 0;
        missiles = new List<GameObject>();
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        for (int i = 0; i < MISSILE_CAP; i++)
        {
            GameObject newMissile = Instantiate(prefab);
            missiles.Add(newMissile);
            newMissile.SetActive(false);
        }
    }

    void Update()
    {
        if (!gameManager.IsAlive()) return;
        cooldown = Mathf.Max(0, cooldown - Time.deltaTime);
        if (Input.GetButton("Fire2"))
        {
            if(cooldown == 0)
            {
                SpawnMissile();
                cooldown += MAX_COOLDOWN;
            }
        }
    }

    private void SpawnMissile()
    {
        foreach (GameObject missile in missiles)
        {
            if(!missile.activeSelf)
            {
                missile.SetActive(true);
                break;
            }
        }
    }
}