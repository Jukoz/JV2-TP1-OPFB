using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private GameObject goal;
    private UnityEngine.AI.NavMeshAgent navMeshAgent;
    private EnemyHealth _enemyHealth;

    void OnEnable()
    {
        goal = GameObject.Find("SpaceMarine");
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        navMeshAgent.enabled = false;
        _enemyHealth = GetComponent<EnemyHealth>();
    }

    void FixedUpdate()
    {
        if(_enemyHealth.IsAlive())
        {
            if (navMeshAgent.enabled)
                navMeshAgent.destination = goal.transform.position;
        }
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("ground"))
            navMeshAgent.enabled = true;
    }
}
