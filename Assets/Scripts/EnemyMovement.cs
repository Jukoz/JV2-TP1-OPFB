using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody rb;
    private GameObject goal;
    private UnityEngine.AI.NavMeshAgent navMeshAgent;
    private EnemyHealth enemyHealth;
    Vector3 lastPosition = Vector3.zero;

    void OnEnable()
    {
        if(rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }
        goal = GameObject.Find("SpaceMarine");
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        navMeshAgent.enabled = false;
        enemyHealth = GetComponent<EnemyHealth>();
    }

    void FixedUpdate()
    {
        if(enemyHealth.IsAlive())
        {
            if (navMeshAgent.enabled)
                navMeshAgent.destination = goal.transform.position;

            float speed = (transform.position - lastPosition).magnitude;
            lastPosition = transform.position;
            animator.SetFloat("Speed", speed);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("ground"))
            navMeshAgent.enabled = true;
    }
}
