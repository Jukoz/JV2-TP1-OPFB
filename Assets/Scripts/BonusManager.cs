using System.Collections.Generic;
using UnityEngine;

public class BonusManager : MonoBehaviour
{
    [SerializeField] private int maxBonusPerType = 5;
    [SerializeField] private GameObject healingBonusPrefab;
    [SerializeField] private GameObject missileBonusPrefab;
    [SerializeField] private GameObject tripleShotBonusPrefab;
    [SerializeField] private AudioSource spawnSFX;
    [SerializeField] private int spawningChance = 25; 
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
            InstantiateBonus(healingBonuses, healingBonusPrefab, gameManager);
            InstantiateBonus(missileBonuses, missileBonusPrefab, gameManager);
            InstantiateBonus(tripleShotBonuses, tripleShotBonusPrefab, gameManager);
        }
    }

    public void InstantiateBonus(List<GameObject> bonusList, GameObject prefab, GameManager gameManager)
    {
        GameObject newBonus = Instantiate(prefab);
        newBonus.GetComponent<BonusPickup>().SetGameManager(gameManager);
        newBonus.transform.parent = this.transform.parent;
        bonusList.Add(newBonus);
        newBonus.SetActive(false);
    }

    public void SpawnBonus(Vector3 position)
    {
        if (Random.Range(0, 100) < spawningChance)
        {
            spawnSFX.Play();
            float bonusType = Random.Range(0, 3);
            if(bonusType < 1)
                ActivateInactiveBonus(healingBonuses, position);
            else if(bonusType < 2)
                ActivateInactiveBonus(missileBonuses, position);
            else
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
