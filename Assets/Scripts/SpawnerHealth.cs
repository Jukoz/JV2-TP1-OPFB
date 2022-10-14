using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 5;
    [SerializeField] private int currentLifePoints;
    void Start()
    {
        currentLifePoints = maxHealth;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            Hit(1); //manque const
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
    
    private void Kill()
    {
        this.gameObject.SetActive(false);
    }
}
