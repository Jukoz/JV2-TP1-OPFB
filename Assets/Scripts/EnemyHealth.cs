using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private ParticleSystem explosion;
    [SerializeField] private AudioSource deathSFX;
    [SerializeField] private CapsuleCollider capsuleCollider;
    [SerializeField] private int maxHealth = 1;
    [SerializeField] private int currentLifePoints;
    private GameManager gameManager;

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
            if(collided.gameObject.transform.position.y < (this.transform.position.y + 1f))
            {
                gameManager.OnPlayerHit(collided);
            }
            Kill();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            Debug.Log("BULE");
            gameManager.OnAlienHit(this.gameObject);
            Kill();
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

    public void Hit()
    {
        if(--currentLifePoints == 0)
            Kill();
    }
    
    private void Kill()
    {
        explosion.gameObject.SetActive(true);
        explosion.Play();
        this.capsuleCollider.enabled = false;
        this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        this.gameObject.transform.GetChild(1).gameObject.SetActive(false);
        deathSFX.Play();
        Invoke("HandleDeath", 1.5f);
    }

    private void HandleDeath()
    {
        explosion.gameObject.SetActive(false);
        this.gameObject.SetActive(false);
    }
}
