using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusManager : MonoBehaviour
{
    [SerializeField] private int maxBonusPerType = 5;
    [SerializeField] private GameObject healingBonusPrefab;
    [SerializeField] private GameObject missileBonusPrefab;
    [SerializeField] private GameObject tripleShotBonusPrefab;
    [SerializeField] private int spawningChance = 80; 
    private List<GameObject> healingBonuses;
    private List<GameObject> missileBonuses;
    private List<GameObject> tripleShotBonuses;
    void Start()
    {
        GameManager gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        healingBonuses = new List<GameObject>();
        missileBonuses = new List<GameObject>();
        tripleShotBonuses = new List<GameObject>();
        for (int i = 0; i < maxBonusPerType; i++)
        {
            GameObject newHealingBonus = Instantiate(healingBonusPrefab);
            // GameObject newMissileBonus = Instantiate(missileBonusPrefab);
            GameObject newTripleShotBonus = Instantiate(tripleShotBonusPrefab);
            newHealingBonus.GetComponent<HealthBonusPickup>().SetGameManager(gameManager);
            newTripleShotBonus.GetComponent<TripleShotPickup>().SetGameManager(gameManager);
            healingBonuses.Add(newHealingBonus);
            // missileBonuses.Add(newMissileBonus);
            tripleShotBonuses.Add(newTripleShotBonus);
            newHealingBonus.SetActive(false);
            // newMissileBonus.SetActive(false);
            newTripleShotBonus.SetActive(false);
        }
    }

    public void SpawnBonus(Vector3 position)
    {
        if (Random.Range(0, 100) < spawningChance)
        {
            ActivateInactiveBonus(tripleShotBonuses, position);
        }
    }

    private void ActivateInactiveBonus(List<GameObject> bonuses, Vector3 position)
    {
        foreach (GameObject bonus in bonuses)
        {
            if (!bonus.activeSelf)
            {
                bonus.transform.position = position;
                bonus.SetActive(true);
                break;
            }
        }
    }
}
