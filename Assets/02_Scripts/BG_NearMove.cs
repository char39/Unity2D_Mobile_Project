using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BG_NearMove : MonoBehaviour
{
    [SerializeField] private Transform tr = null;
    private Vector3 offset = Vector3.zero;
    public float speed = 10f;
    

    void Start()
    {
        tr = GetComponent<Transform>();
    }

    void Update()
    {
        tr.Translate(Vector3.left * speed * Time.deltaTime);
        if (tr.position.x <= -61f)
        {
            tr.position = new Vector3(-19.4f, tr.position.y, tr.position.z);
        }

    }
}
