using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMove : MonoBehaviour
{
    public float speed = 5.3f;
   
    public MeshRenderer meshRenderer;
    private float x = 0f, y = 0f;
    void Start()
    {
        
    }
    void Update()
    {
        x += speed * Time.deltaTime;
        meshRenderer.material.mainTextureOffset = new Vector2(x, y);
        //�޽� ������ �ȿ� ���͸��� �ȿ� �̹����� �����δ�.
    }
}
