using System.Collections.Generic;
using UnityEngine;

public class MissileManager : MonoBehaviour
{
    private const int MISSILE_CAP = 10;
    private const float MAX_COOLDOWN = 2;
    private const int MISSILES_PER_PICKUP = 5;
    [SerializeField] private float cooldown;
    [SerializeField] private GameObject prefab;
    [SerializeField] private int missilesLeft = 0;
    private List<GameObject> missiles;
    private GameManager gameManager;
    void Start()
    {
        cooldown = 0;
        missilesLeft = 0;
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
        if (Input.GetButton("Fire2") && !Input.GetButton("Fire1"))
        {
            if(cooldown == 0 && missilesLeft > 0)
            {
                SpawnMissile();
                cooldown += MAX_COOLDOWN;
                missilesLeft--;
            }
        }
    }

    public void AddMissiles()
    {
        missilesLeft += MISSILES_PER_PICKUP;
    }
    
    public int GetMissiles()
    {
        return missilesLeft;
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
