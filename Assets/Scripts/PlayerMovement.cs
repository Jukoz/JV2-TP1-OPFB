using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] public bool isGrounded;
    [SerializeField] public Vector3 raycastOffset = new Vector3(0, 1.2f, 0);
    [SerializeField] public float raycastLength = 1.5f;
    [SerializeField] protected AudioSource pickupSFX;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private float speed = 10;
    [SerializeField] private float jumpForce;
    [SerializeField] private float gravityForce;

    private float hAxis = 0;
    private float vAxis = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    private void Update()
    {
        if (!IsAlive()) return;
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
        RaycastHit hit;
        if (Physics.Raycast(transform.position + raycastOffset, Vector3.down, out hit, raycastLength))
        {
            isGrounded = (hit.transform.tag == "ground");
        } else
        {
            isGrounded = false;
        }
        Jump();
    }

    void FixedUpdate()
    {
        if (!IsAlive()) return;
        Vector3 direction = Vector3.Normalize(new Vector3(hAxis, 0, vAxis));
        Vector3 newVelocity = rb.transform.TransformDirection(direction) * speed * Time.fixedDeltaTime;
        rb.velocity = new Vector3(newVelocity.x, rb.velocity.y, newVelocity.z);
    }
    
    public bool IsAlive()
    {
        return gameObject.GetComponent<PlayerHealth>().IsAlive();
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump"))
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
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bonus"))
        {
            pickupSFX.Play();
        }
    }
}
