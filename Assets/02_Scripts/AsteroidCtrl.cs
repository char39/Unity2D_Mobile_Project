using UnityEngine;

public class AsteroidCtrl : MonoBehaviour
{
    [SerializeField] private GameObject explosionEffect;
    public AudioSource source;
    public AudioClip hitClip;
    private Transform hitPos;
    private Transform tr;
    private SpriteRenderer spriteRenderer;
    private CircleCollider2D circleCollider2D;
    private float speed;

    void Awake()
    {
        tr = GetComponent<Transform>();
        source = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        circleCollider2D = GetComponent<CircleCollider2D>();
        speed = Random.Range(10.0f, 15.0f);
        spriteRenderer.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        circleCollider2D.enabled = true;
    }

    void OnEnable()
    {
        speed = Random.Range(10.0f, 15.0f);
        spriteRenderer.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        circleCollider2D.enabled = true;
    }

    void Update()
    {
        tr.Translate(speed * Time.deltaTime * Vector3.left);
        if (tr.position.x < -40.0f)
            SetDeActive();
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("BULLET"))
        {
            source.PlayOneShot(hitClip, 1.0f);
            hitPos = coll.GetComponent<Transform>();
            GameManager.instance.SetActiveExplosionFromPool(transform);
            Destroy(coll.gameObject);
            spriteRenderer.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
            circleCollider2D.enabled = false;
            circleCollider2D.GetComponentInChildren<ParticleSystem>().Stop();
            Invoke(nameof(SetDeActive), 0.3f);
        }
        else if (coll.CompareTag("Player"))
        {
            hitPos = coll.GetComponent<Transform>();
            GameManager.instance.SetActiveExplosionFromPool(transform);
            spriteRenderer.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
            circleCollider2D.enabled = false;
            circleCollider2D.GetComponentInChildren<ParticleSystem>().Stop();
            Invoke(nameof(SetDeActive), 0.3f);
        }
    }
    private void SetDeActive() => gameObject.SetActive(false);
}
