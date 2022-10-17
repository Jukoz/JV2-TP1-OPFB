using UnityEngine;

public class Bullet : MonoBehaviour
{
    public const int BULLET_DAMAGE = 1;
    [SerializeField] private float speed = 40;
    [SerializeField] private ParticleSystem explosion;
    [SerializeField] private bool isAlive = false;
    private Renderer renderer;

    private void Awake()
    {
        rendererMaterial = GetComponent<Renderer>();
    }

    private void OnEnable()
    {
        isAlive = true;
        renderer.enabled = true;
    }

    void Update()
    {
        if(isAlive) transform.Translate(0, 0, speed, Space.Self);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!other.gameObject.CompareTag("Player") && !other.gameObject.CompareTag("Bullet") && !other.gameObject.CompareTag("Bonus"))
        {
            isAlive = false;
            rendererMaterial.enabled = false;
            explosion.gameObject.SetActive(true);
            explosion.Play();
            Invoke("DelayDisable", 1.5f);
        }
    }

    private void DelayDisable()
    {
        explosion.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }
}
