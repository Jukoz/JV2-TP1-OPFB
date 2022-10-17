using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private ParticleSystem explosion;
    [SerializeField] private AudioSource deathSFX;
    [SerializeField] private CapsuleCollider capsuleCollider;
    [SerializeField] private int maxHealth = 1;
    [SerializeField] private int currentLifePoints;
    [SerializeField] private GameManager gameManager;

    private void OnEnable()
    {
        this.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        this.gameObject.transform.GetChild(1).gameObject.SetActive(true);
        this.capsuleCollider.enabled = true;
        currentLifePoints = maxHealth; 
    }

    void OnCollisionEnter(Collision collision)
    {
        GameObject collided = collision.gameObject;
        if(collided.CompareTag("Player"))
        {
            if (!gameManager.IsAlive()) return;
            if(collided.gameObject.transform.position.y < (this.transform.position.y + 1f))
            {
                gameManager.OnPlayerHit(collided);
            }
            Kill();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet") || other.CompareTag("Splash"))
        {
            Hit(Bullet.BULLET_DAMAGE);
        }
        else if (other.CompareTag("Missile"))
        {
            Hit(Missile.MISSILE_DAMAGE);
        }
    }

    public bool IsAlive()
    {
        return currentLifePoints > 0;
    }

    public void SetGameManager(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }

    public void Hit(int damage)
    {
        if((currentLifePoints -= damage) <= 0)
            Kill();
    }
    
    private void Kill()
    {
        explosion.gameObject.SetActive(true);
        explosion.Play();
        capsuleCollider.enabled = false;
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
        gameObject.transform.GetChild(1).gameObject.SetActive(false);
        deathSFX.Play();
        gameManager.OnAlienKill(this.gameObject);
        Invoke("HandleDeath", 1.5f);
    }

    private void HandleDeath()
    {
        explosion.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }
}
