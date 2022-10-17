using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 5;
    [SerializeField] private int currentLifePoints;
    private GameManager gameManager;
    void Start()
    {
        currentLifePoints = maxHealth;
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Bullet"))
        {
            Hit(1); //manque const
        }
        else if (other.CompareTag("Missile"))
        {

            Hit(5);
        }
    }
    
    public bool IsAlive()
    {
        return currentLifePoints > 0;
    }
    
    public void Hit(int damage)
    {
        if((currentLifePoints -= damage) <= 0)
            Kill();
    }

    public void SetGameManager(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }
    
    private void Kill()
    {
        if(this.gameObject == null)
            Debug.Log("penis");
        Debug.Log(this.gameObject);
        gameManager.OnSpawnerKill(this.gameObject);
        this.gameObject.SetActive(false);
    }
}
