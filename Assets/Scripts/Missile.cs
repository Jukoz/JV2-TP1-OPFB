using UnityEngine;

public class Missile : MonoBehaviour
{
    public const int MISSILE_DAMAGE = 5;
    public const int SPLASH_DAMAGE = 1;
    [SerializeField] private float speed = 40;
    [SerializeField] private ParticleSystem explosion;
    [SerializeField] private GameObject rocket;
    [SerializeField] private GameObject splashArea;
    [SerializeField] private bool isAlive = false;
    private GameObject player;
    private Vector3 spawnPointOffset = new Vector3(0f, 3.5f, 0f);

    private void OnEnable()
    {
        rocket.SetActive(true);
        splashArea.SetActive(false);
        isAlive = true;
        player = GameObject.FindGameObjectWithTag("Player");
        transform.eulerAngles = player.transform.eulerAngles;
        Vector3 newSpawnOffset = (player.transform.forward * 10f) + spawnPointOffset;
        transform.position = player.transform.position + newSpawnOffset;
    }

    void Update()
    {
        if(isAlive)
            transform.Translate(0, 0, speed * Time.deltaTime , Space.Self);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player") && !other.gameObject.CompareTag("Bullet") && !other.gameObject.CompareTag("Bonus"))
        {
            rocket.SetActive(false);
            splashArea.SetActive(true);
            explosion.gameObject.SetActive(true);
            explosion.Play();
            isAlive = false;
            Invoke("DelayDisable", 1.5f);
        }
    }

    private void DelayDisable()
    {
        explosion.gameObject.SetActive(false);
        this.gameObject.SetActive(false);
    }
}
