using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private const float INVICIBILITY_MAX = 3;
    private const int INITIAL_HEALTH = 5;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private float invicibility;
    [SerializeField] private int lives;
    void Start()
    {
        lives = INITIAL_HEALTH;
        SetMaxInvincibility();
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    void Update()
    {
        invicibility = Mathf.Max(0, invicibility -= Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {  
        if (other.gameObject.CompareTag("Bullet"))
        {
            if (other.gameObject.GetComponent<Bullet>().GetOwnerShooter() != this.gameObject)
            {
                gameManager.OnPlayerHit(this.gameObject);
            }
        }
    }

    public bool IsAlive()
    {
        return lives > 0;
    }

    public int GetLives()
    {
        return lives;
    }

    public bool IsInvincible()
    {
        return invicibility > 0;
    }

    public void LoseLife()
    {
        if (IsAlive())
        {
            SetMaxInvincibility();
            lives--;
        }
    }

    public void AddLive()
    {
        lives++;
    }

    private void SetMaxInvincibility()
    {
        invicibility = INVICIBILITY_MAX;
    }
}
