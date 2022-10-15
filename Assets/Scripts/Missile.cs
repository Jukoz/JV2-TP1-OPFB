using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    [SerializeField] private float speed = 20;
    private GameObject player;
    private Vector3 spawnPointOffset = new Vector3(0f, 3.5f, 0f);

    void Update()
    {
        if(gameObject.activeSelf) transform.Translate(0, 0, speed * Time.deltaTime , Space.Self);
    }

    private void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        transform.eulerAngles = player.transform.eulerAngles;
        Vector3 newSpawnOffset = (player.transform.forward * 10f) + spawnPointOffset;
        transform.position = player.transform.position + newSpawnOffset;
    }
}
