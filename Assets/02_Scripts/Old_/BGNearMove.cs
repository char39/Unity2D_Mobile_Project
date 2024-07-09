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
        //�ڽ��ݶ��̴� 2d�� ���̰��� width ������ �ʱ�ȭ ��
    }
    void Update()
    {             
        //������ �̵��Լ� 
        tr.Translate(Vector3.left  * speed * 
            Time.deltaTime);
        #region x���� ������� �ǵ��� ȭ�鿡 Ƥ��.
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
