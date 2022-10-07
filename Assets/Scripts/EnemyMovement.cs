using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private GameObject goal;
    private UnityEngine.AI.NavMeshAgent navMeshAgent;

    void Start()
    {
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }
    
    void FixedUpdate()
    {
        if (navMeshAgent != null) 
            navMeshAgent.destination = goal.transform.position;
    }
    void Update()
    {
        if (navMeshAgent == null)
        {
            Debug.Log("bad");
            transform.position = Vector3.MoveTowards(transform.position, goal.transform.position, Time.deltaTime);
        }
    }
}
