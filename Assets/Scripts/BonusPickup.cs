using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BonusPickup : MonoBehaviour
{
    protected GameManager gameManager;

    public void SetGameManager(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }
}
