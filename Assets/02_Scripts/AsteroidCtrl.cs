using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidCtrl : MonoBehaviour
{
    [SerializeField] private GameObject explosionEffect;
    public AudioSource source;
    public AudioClip hitClip;
    private Transform hitPos;
    private Transform tr = null;
    private SpriteRenderer spriteRenderer;
    private CircleCollider2D circleCollider2D;
    private float speed;

    void Start()
    {
        tr = GetComponent<Transform>();
        speed = Random.Range(10.0f, 15.0f);
        source = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        circleCollider2D = GetComponent<CircleCollider2D>();
    }

    void Update()
    {
        tr.Translate(Vector3.left * speed * Time.deltaTime);
        if (tr.position.x < -40.0f)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("BULLET"))
        {
            source.PlayOneShot(hitClip, 1.0f);
            hitPos = coll.GetComponent<Transform>();
            GameObject effect = Instantiate(explosionEffect, hitPos.position, Quaternion.identity);
            Destroy(effect, 0.5f);
            Destroy(coll.gameObject);
            spriteRenderer.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
            circleCollider2D.enabled = false;
            circleCollider2D.GetComponentInChildren<ParticleSystem>().Stop();
            Destroy(gameObject, 0.3f);
        }
    }
}
