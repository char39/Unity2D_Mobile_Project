using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 1. MeshRenderer 안의 Material의 Image 가 필요
// 2. 속도
// 3. 어느 방향으로 움직일지

public class BG_FarMove : MonoBehaviour
{
    #region 3D에서 사용하는 코드
    // [SerializeField] private MeshRenderer meshRenderer;
    // public float speed = 0.2f;
    // private float x;
    
    // void Start()
    // {
    //     meshRenderer = GetComponent<MeshRenderer>();

    // }

    // void Update()
    // {
    //     x += speed * Time.deltaTime;
    //     meshRenderer.material.mainTextureOffset = new Vector2(x, 0);
    //     // MeshRenderer.Material.Image를 움직이는 코드 (3d에서 사용하는 코드)

    // }
    #endregion

    private float speed;
    private Transform tr;
    private float width;
    private BoxCollider2D boxCollider2D;
    IEnumerator Start()
    {
        speed = 10.0f;
        boxCollider2D = GetComponent<BoxCollider2D>();
        tr = GetComponent<Transform>();
        width = boxCollider2D.size.x;   // BoxCollider2D(배경오브젝트의 col만큼)의 x값을 가져옴
        yield return null;  // 한 프레임 대기
        StartCoroutine(BackGroundLoop());   // 배경을 움직이는 함수 실행
    }
    IEnumerator BackGroundLoop()    // 배경을 움직이는 함수
    {
        while (!GameManager.instance.isGameOver)    // 게임오버가 아닐 때까지
        {
            tr.Translate(Vector3.left * speed * Time.deltaTime);    // 왼쪽으로 이동
            if (tr.position.x <= -width * 2f)   // 배경이 왼쪽으로 이동하다가 -width * 2f보다 작아지면
                RePosition();   // 배경이 끝나면 다시 배경을 우측으로 이동
            yield return new WaitForSeconds(0.02f);  // 0.02초마다 실행
        }
    }
    void RePosition()   // 배경을 우측으로 이동시키는 함수
    {
        Vector2 offset = new Vector2 (width * 3f, 0.0f);
        //               new Vector3 (width, 0.0f, tr.position.z);
        tr.position = (Vector2)tr.position + offset;
    }

}
