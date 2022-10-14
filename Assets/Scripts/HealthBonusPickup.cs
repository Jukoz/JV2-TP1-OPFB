using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBonusPickup : MonoBehaviour
{
    private GameManager gameManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            gameManager.OnHealthBonusPickup();
        }
    }

    public void SetGameManager(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }
}
