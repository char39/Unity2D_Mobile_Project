using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 1. MeshRenderer 안의 Material의 Image 가 필요
// 2. 속도
// 3. 어느 방향으로 움직일지

public class BG_FarMove : MonoBehaviour
{
    [SerializeField] private MeshRenderer meshRenderer;
    public float speed = 0.2f;
    private float x;
    
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();

    }

    void Update()
    {
        x += speed * Time.deltaTime;
        meshRenderer.material.mainTextureOffset = new Vector2(x, 0);    // MeshRenderer.Material.Image를 움직이는 코드

    }
}
