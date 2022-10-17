using UnityEngine;

public class Bullet : MonoBehaviour
{
    public const int BULLET_DAMAGE = 1;
    [SerializeField] private float speed = 40;
    [SerializeField] private ParticleSystem explosion;
    [SerializeField] private GameObject shooter;
    [SerializeField] private bool isAlive = false;
    private Renderer rendererMaterial;

    private void Awake()
    {
        rendererMaterial = GetComponent<Renderer>();
    }

    private void OnEnable()
    {
        isAlive = true;
        rendererMaterial.enabled = true;
    }

    void Update()
    {
        if(isAlive) transform.Translate(0, 0, speed, Space.Self);
    }

    public void SetOwnerShooter(GameObject shooter)
    {
        this.shooter = shooter;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject != shooter && !other.gameObject.CompareTag("Bullet") && !other.gameObject.CompareTag("Bonus"))
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
