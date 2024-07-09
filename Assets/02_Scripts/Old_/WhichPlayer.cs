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
        //게임 시작전  tr이라는 변수에 자기자신의
        //트랜스폼 컴퍼넌트로 초기화
        tr = GetComponent<Transform>();
        Halfheight = Screen.height * 0.5f;
                     //현재 스크린 높이 
        Halfwidth = Screen.width * 0.5f;
                    //현재스크린너비
    }
    void Update()
    {
        #region pc게임에서 움직이는 로직
        if (Application.platform == RuntimePlatform.WindowsEditor)
        { 
            h = Input.GetAxis("Horizontal"); //A,D
            v = Input.GetAxis("Vertical");// W,S
            Vector3 moveDir = (Vector3.right * h) + (Vector3.up * v);
            tr.Translate(moveDir.normalized * moveSpeed * Time.deltaTime);
            // normalized(정규화) 똑같은 속도가 되기 하기 위해 
            // 카메라를 벗어나지 않게 하는 로직 

            tr.position = new Vector3(Mathf.Clamp(tr.position.x, -7.6f, 7.6f),
                Mathf.Clamp(tr.position.y, -2.6f, 2.6f), 0f);
         }

        #endregion
        #region 모바일 안드로이드에서 움직이는 로직
        if(Application.platform == RuntimePlatform.Android)
        {
            float deltaXpos = Input.GetTouch(0).position.x - Halfwidth;
            float deltaYpos = Input.GetTouch(0).position.y - Halfheight;
            float Xpos = deltaXpos - tr.position.x; // 플레이어 터치로 이동 할 거리
            float Ypos = deltaYpos - tr.position.y;

            tr.Translate(Xpos * moveSpeed * Time.deltaTime,Ypos * moveSpeed * Time.deltaTime,0f);
            tr.position = new Vector3(Mathf.Clamp(tr.position.x, -7.6f, 7.6f),
             Mathf.Clamp(tr.position.y, -2.6f, 2.6f), 0f);
        }

        #endregion
    }
}
