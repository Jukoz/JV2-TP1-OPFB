using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private ParticleSystem explosion;
    private GameObject goal;
    private UnityEngine.AI.NavMeshAgent navMeshAgent;
    private bool alive;

    void OnEnable()
    {
        goal = GameObject.Find("SpaceMarine");
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        navMeshAgent.enabled = false;
        this.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        this.gameObject.transform.GetChild(1).gameObject.SetActive(true);
    }

    void FixedUpdate()
    {
        Debug.Log(navMeshAgent.enabled);
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
        if (collision.gameObject.CompareTag("ground"))
            navMeshAgent.enabled = true;
    }
    
    public void Kill()
    {
        explosion.gameObject.SetActive(true);
        explosion.Play();
        this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        this.gameObject.transform.GetChild(1).gameObject.SetActive(false);
        Invoke("Death", 1.5f);
    }

    private void Death()
    {
        explosion.gameObject.SetActive(false);
        this.gameObject.SetActive(false);
    }
}
