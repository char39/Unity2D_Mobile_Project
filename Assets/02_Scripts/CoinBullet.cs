using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBullet : MonoBehaviour
{
    private Rigidbody2D rb2D;
    [SerializeField] private GameObject Effect;
    private float speed;
    void Start()
    {
        speed = 1000.0f;
        rb2D = GetComponent<Rigidbody2D>();
        rb2D.AddForce(transform.right * speed);
        Destroy(gameObject, 1.5f);
    }

}
