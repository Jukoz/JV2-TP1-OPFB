using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 40;
    [SerializeField] private ParticleSystem explosion;
    [SerializeField] private bool isAlive = false;
    private Vector3 spawnPointOffset = new Vector3(0f, 3.5f, 0f);
    private Renderer renderer;
    private GameObject player;

    private void Awake()
    {
        renderer = GetComponent<Renderer>();
    }

    private void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        isAlive = true;
        renderer.enabled = true;
        transform.eulerAngles = player.transform.eulerAngles;
    }

    void Update()
    {
        if(isAlive) transform.Translate(0, 0, speed, Space.Self);
    }

    public void EnableBullet(Vector3 offset)
    {
        gameObject.SetActive(true);
        Vector3 newSpawnOffset = (player.transform.forward * 10f) + spawnPointOffset;
        transform.position = player.transform.position + newSpawnOffset + offset;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!other.gameObject.CompareTag("Player") && !other.gameObject.CompareTag("Bullet"))
        {
            isAlive = false;
            renderer.enabled = false;
            explosion.gameObject.SetActive(true);
            explosion.Play();
            Invoke("DelayDisable", 1.5f);
        }
    }

    private void DelayDisable()
    {
        explosion.gameObject.SetActive(false);
        this.gameObject.SetActive(false);
    }
}
