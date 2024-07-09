using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGNearMove : MonoBehaviour
{
    public float speed = 5.0f;
    public BoxCollider2D collider2D;
    public Transform tr;
    public float width = 0.0f;
    void Start()
    {
        tr = GetComponent<Transform>();
        collider2D = GetComponent<BoxCollider2D>();
        width = collider2D.size.x;
        //박스콜라이더 2d의 넓이값이 width 안으로 초기화 됨
    }
    void Update()
    {             
        //포지션 이동함수 
        tr.Translate(Vector3.left  * speed * 
            Time.deltaTime);
        #region x값을 원래대로 되돌림 화면에 튄다.
        //if (transform.position.x <= -30f)
        //    transform.position = new Vector3(-10f,
        //        transform.position.y, transform.position.z);
        #endregion

        if(tr.position.x <= -width)
        {
            tr.position = new Vector3(width * 0.75f, tr.position.y,tr.position.z);
        }

    }
}
