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
            if(gameObject.CompareTag("Alien"))
            {
                if(collided.gameObject.transform.position.y < (this.transform.position.y + 1f))
                {
                    gameManager.OnPlayerHit(collided);
                }
            }
            Hit(1);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            if(other.gameObject.GetComponent<Bullet>().GetOwnerShooter() != this.gameObject)
            {
                Hit(Bullet.BULLET_DAMAGE);
            }
        } else if (other.CompareTag("Splash"))
        {
            Hit(Missile.SPLASH_DAMAGE);
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
        if (this.gameObject.CompareTag("Boss")) gameManager.Victory();
        Invoke("HandleDeath", 1.5f);
    }

    private void HandleDeath()
    {
        explosion.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }
}
