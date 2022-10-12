using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int kills = 0;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void OnAlienHit(GameObject alien)
    {
        if(alien.CompareTag("Alien"))
        {
            EnemyMovement enemyMovement = alien.GetComponent<EnemyMovement>();
            kills++;
            enemyMovement.Kill();
        }
    }
}
