using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 40;
    [SerializeField] private ParticleSystem explosion;
    [SerializeField] private bool isAlive = false;
    //[SerializeField] private AudioSource shootingSound;
    private Vector3 spawnPointOffset = new Vector3(0f, 3.5f, 0f);
    private Renderer renderer;

    private void Awake()
    {
        renderer = GetComponent<Renderer>();
        //shootingSound = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        isAlive = true;
        renderer.enabled = true;
        //shootingSound.Play();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Vector3 newSpawnOffset = (player.transform.forward * 10f) + spawnPointOffset;
        transform.position = player.transform.position + newSpawnOffset;
        transform.eulerAngles = player.transform.eulerAngles;
    }

    void Update()
    {
        if(isAlive) transform.Translate(0, 0, speed, Space.Self);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!other.gameObject.CompareTag("Player"))
        {
            isAlive = false;
            renderer.enabled = false;
            explosion.gameObject.SetActive(true);
            explosion.Play();
            Invoke("delayDisable", 1.5f);
        }
        
    }

    private void delayDisable()
    {
        explosion.gameObject.SetActive(false);
        this.gameObject.SetActive(false);
    }
}
