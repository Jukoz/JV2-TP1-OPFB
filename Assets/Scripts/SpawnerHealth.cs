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

    public bool IsAlive()
    {
        return currentLifePoints > 0;
    }
    
    public void Hit()
    {
        if(--currentLifePoints == 0)
            Kill();
    }
    
    private void Kill()
    {
        this.gameObject.SetActive(false);
    }
}
