using UnityEngine;

public class HealthBonusPickup : BonusPickup
{ 
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            gameManager.OnHealthBonusPickup();
        }
    }
}
