using UnityEngine;

public class Bullet : MonoBehaviour
{
    public const int BULLET_DAMAGE = 1;
    [SerializeField] private float speed = 40;
    [SerializeField] private ParticleSystem explosion;
    [SerializeField] private bool isAlive = false;
    private Vector3 spawnPointOffset = new Vector3(0f, 3.5f, 0f);
    private Renderer rendererMaterial;
    private GameObject player;

    private void Awake()
    {
        rendererMaterial = GetComponent<Renderer>();
    }

    private void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        isAlive = true;
        rendererMaterial.enabled = true;
        Vector3 newSpawnOffset = (player.transform.forward * 10f) + spawnPointOffset;
        transform.position = player.transform.position + newSpawnOffset;
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
