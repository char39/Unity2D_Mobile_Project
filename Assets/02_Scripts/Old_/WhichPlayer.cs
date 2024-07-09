using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhichPlayer : MonoBehaviour
{
    private float h, v;
    public Transform tr;
    public float moveSpeed = 3f;
    public float Halfwidth;
    public float Halfheight;
    void Start()
    {   
        //���� ������  tr�̶�� ������ �ڱ��ڽ���
        //Ʈ������ ���۳�Ʈ�� �ʱ�ȭ
        tr = GetComponent<Transform>();
        Halfheight = Screen.height * 0.5f;
                     //���� ��ũ�� ���� 
        Halfwidth = Screen.width * 0.5f;
                    //���罺ũ���ʺ�
    }
    void Update()
    {
        #region pc���ӿ��� �����̴� ����
        if (Application.platform == RuntimePlatform.WindowsEditor)
        { 
            h = Input.GetAxis("Horizontal"); //A,D
            v = Input.GetAxis("Vertical");// W,S
            Vector3 moveDir = (Vector3.right * h) + (Vector3.up * v);
            tr.Translate(moveDir.normalized * moveSpeed * Time.deltaTime);
            // normalized(����ȭ) �Ȱ��� �ӵ��� �Ǳ� �ϱ� ���� 
            // ī�޶� ����� �ʰ� �ϴ� ���� 

            tr.position = new Vector3(Mathf.Clamp(tr.position.x, -7.6f, 7.6f),
                Mathf.Clamp(tr.position.y, -2.6f, 2.6f), 0f);
         }

        #endregion
        #region ����� �ȵ���̵忡�� �����̴� ����
        if(Application.platform == RuntimePlatform.Android)
        {
            float deltaXpos = Input.GetTouch(0).position.x - Halfwidth;
            float deltaYpos = Input.GetTouch(0).position.y - Halfheight;
            float Xpos = deltaXpos - tr.position.x; // �÷��̾� ��ġ�� �̵� �� �Ÿ�
            float Ypos = deltaYpos - tr.position.y;

            tr.Translate(Xpos * moveSpeed * Time.deltaTime,Ypos * moveSpeed * Time.deltaTime,0f);
            tr.position = new Vector3(Mathf.Clamp(tr.position.x, -7.6f, 7.6f),
             Mathf.Clamp(tr.position.y, -2.6f, 2.6f), 0f);
        }

        #endregion
    }
}
