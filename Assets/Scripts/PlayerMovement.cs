using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private float speed = 10;
    [SerializeField] public bool isGrounded;
    [SerializeField] public Vector3 raycastOffset = new Vector3(0, 0.3f, 0);
    [SerializeField] public float raycastLength = 0.4f;
    [SerializeField] private float jumpForce;
    [SerializeField] private float gravityForce;

    float hAxis = 0;
    float vAxis = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    private void Update()
    {
        if (!isAlive()) return;
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
        RaycastHit hit;
        if (Physics.Raycast(transform.position + raycastOffset, Vector3.down, out hit, raycastLength))
        {
            isGrounded = (hit.transform.tag == "ground");
        }
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * raycastLength, Color.gray);
        Jump();
    }

    void FixedUpdate()
    {
        if (!isAlive()) return;
        Vector3 direction = Vector3.Normalize(new Vector3(hAxis, 0, vAxis));
        Vector3 newVelocity = rb.transform.TransformDirection(direction) * speed * Time.fixedDeltaTime;
        rb.velocity = new Vector3(newVelocity.x, rb.velocity.y, newVelocity.z);
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
            {
                rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
                rb.AddForce(0, jumpForce, 0, ForceMode.Impulse);
                isGrounded = false;
            }
        }
        rb.AddForce(Vector3.down * gravityForce);
    }

    public void Kill()
    {
        // Do some animation stuff;
    }

    public bool isAlive()
    {
        return gameManager.isAlive();
    }
}
