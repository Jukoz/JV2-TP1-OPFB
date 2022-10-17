using UnityEngine;

public class MissilePickup : BonusPickup
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            gameManager.OnMissilePickup();
        }
    }
}
