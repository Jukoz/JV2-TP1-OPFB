using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private ParticleSystem explosion;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private AudioSource deathSFX;
    [SerializeField] private CapsuleCollider capsuleCollider;
    private GameObject goal;
    private UnityEngine.AI.NavMeshAgent navMeshAgent;
    private bool alive;

    void OnEnable()
    {
        goal = GameObject.Find("SpaceMarine");
        alive = true;
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        navMeshAgent.enabled = false;
        this.capsuleCollider.enabled = true;
        this.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        this.gameObject.transform.GetChild(1).gameObject.SetActive(true);
    }

    void FixedUpdate()
    {
        if(alive)
        {
            if (navMeshAgent.enabled)
            {
                navMeshAgent.destination = goal.transform.position;
            }
        }
        
    }

    void OnCollisionEnter(Collision collision)
    {
        GameObject collided = collision.gameObject;
        if (collided.CompareTag("ground"))
        {
            navMeshAgent.enabled = true;
        } else if(collided.CompareTag("Player"))
        {
            if(collided.gameObject.transform.position.y > (this.transform.position.y + 1f))
            {
                Kill();
            } else
            {
                gameManager.OnPlayerHit(collided);
            }
        }
    }
    
    public void Kill()
    {
        explosion.gameObject.SetActive(true);
        explosion.Play();
        alive = false;
        this.capsuleCollider.enabled = false;
        this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        this.gameObject.transform.GetChild(1).gameObject.SetActive(false);
        deathSFX.Play();
        Invoke("Death", 1.5f);
    }

    private void Death()
    {
        explosion.gameObject.SetActive(false);
        this.gameObject.SetActive(false);
    }

    public void SetGameManager(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }
}
