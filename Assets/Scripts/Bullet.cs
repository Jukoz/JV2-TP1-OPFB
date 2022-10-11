using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 40;
    private Vector3 spawnPointOffset = new Vector3(0f, 3.5f, 0f);

    private void OnEnable()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Debug.Log(player.transform.forward);
        Vector3 newSpawnOffset = (player.transform.forward * 10f) + spawnPointOffset;
        transform.position = player.transform.position + newSpawnOffset;
        transform.eulerAngles = new Vector3(player.transform.eulerAngles.x, player.transform.eulerAngles.y, player.transform.eulerAngles.z);
    }

    void Update()
    {
        transform.Translate(0, 0, speed, Space.Self);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        if(!other.gameObject.CompareTag("Player"))
        {
            this.gameObject.SetActive(false);
        }
        
    }
}
