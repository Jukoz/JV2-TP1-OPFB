using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private GameObject goal;
    private UnityEngine.AI.NavMeshAgent navMeshAgent;

    void OnEnable()
    {
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        navMeshAgent.enabled = false;
    }

    void FixedUpdate()
    {
        if (navMeshAgent.enabled) 
            navMeshAgent.destination = goal.transform.position;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("ground"))
            navMeshAgent.enabled = true;
    }
    
}
