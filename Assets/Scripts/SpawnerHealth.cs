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
        gameManager.OnSpawnerKill(this.gameObject);
        this.gameObject.SetActive(false);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
            Hit(Bullet.BULLET_DAMAGE);
        else if (other.CompareTag("Missile"))
            Hit(Missile.MISSILE_DAMAGE);
    }
}
