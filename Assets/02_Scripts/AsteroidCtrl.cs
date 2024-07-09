using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidCtrl : MonoBehaviour
{
    [SerializeField] private Transform tr = null;
    public float speed = 0.0f;

    void Start()
    {
        tr = GetComponent<Transform>();
        speed = Random.Range(10.0f, 15.0f);
    }

    void Update()
    {
        tr.Translate(Vector3.left * speed * Time.deltaTime);
        if (tr.position.x < -40.0f)
            Destroy(gameObject);
    }
}
