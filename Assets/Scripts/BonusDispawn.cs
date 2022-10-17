using UnityEngine;

public class BonusDispawn : MonoBehaviour
{
    [SerializeField] private float lifetime;
    private const float MAX_LIFETIME = 15;

    private void OnEnable()
    {
        lifetime = MAX_LIFETIME;
    }

    void Update()
    {
        if(gameObject.activeSelf)
            if((lifetime -= Time.deltaTime) <= 0)
                gameObject.SetActive(false);
    }
}
