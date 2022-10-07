using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float speed = 10;

    float hAxis = 0;
    float vAxis = 0;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 direction = Vector3.Normalize(new Vector3(hAxis, 0, vAxis));
        rb.position += direction * speed * Time.fixedDeltaTime;
    }
}
