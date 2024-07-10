using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class RocketCtrl : MonoBehaviour
{
    public GameObject StarEffect;
    public AudioSource source;
    public AudioClip hitClip;
    private Transform tr = null;
    [Header("Vars")]
    private float speed = 10.0f;
    private float h = 0f, v = 0f;
    private string Asteroid_Tag = "ASTEROID";
    private float halfHeight = 0.0f, halfWidth = 0.0f;
    public Transform firePos;
    public GameObject coinBullet;
    private Vector3 moveVector;
    [SerializeField] private JoyStick_TouchPad joyStick_TouchPad;

    void Start()
    {
        tr = GetComponent<Transform>();
        source = GetComponent<AudioSource>();
        halfHeight = Screen.height * 0.5f;
        halfWidth = Screen.width * 0.5f;
        joyStick_TouchPad = GameObject.Find("Image_JoyStickPad").GetComponent<JoyStick_TouchPad>();
    }

    void Update()
    {
        AppPlatform();
        CameraOutLimit();       // 카메라 밖으로 나가지 못하게 하는 함수
        QuitApp();
    }

    public void OnStickPos(Vector3 stickPos)
    {
        h = stickPos.x;
        v = stickPos.y;
    }
    private void AppPlatform()
    {
        if (Application.platform == RuntimePlatform.Android)        // 안드로이드 플랫폼일 때
        {
            JoyStickCtrl();
        }
        if (Application.platform == RuntimePlatform.WindowsEditor)  // 윈도우 에디터(유니티) 플랫폼일 때
        {
            JoyStickCtrl();
        }
        if (Application.platform == RuntimePlatform.IPhonePlayer)   // 아이폰 플랫폼일 때
        {
            JoyStickCtrl();
        }
    }
    private void JoyStickCtrl()
    {
        if (GetComponent<Rigidbody2D>())
        {
            Vector2 speed = GetComponent<Rigidbody2D>().velocity;
            speed.x = h * 4.0f;
            speed.y = v * 4.0f;
            GetComponent<Rigidbody2D>().velocity = speed;
        }
    }
    private void CameraOutLimit()
    {
        #region 카메라 밖으로 나가지 못하게 하는 함수 (1)
        // if (tr.position.x >= 8.5f)      // 우측 벽에 닿았을 때 우측 벽에 고정
        // {
        //     tr.position = new Vector3(8.5f, tr.position.y, tr.position.z);
        // }
        // if (tr.position.x <= -8.5f)     // 좌측 벽에 닿았을 때 좌측 벽에 고정
        // {
        //     tr.position = new Vector3(-8.5f, tr.position.y, tr.position.z);
        // }
        // if (tr.position.y >= 4.5f)      // 상단 벽에 닿았을 때 상단 벽에 고정
        // {
        //     tr.position = new Vector3(tr.position.x, 4.5f, tr.position.z);
        // }
        // if (tr.position.y <= -3.0f)     // 하단 벽에 닿았을 때 하단 벽에 고정
        // {
        //     tr.position = new Vector3(tr.position.x, -3.0f, tr.position.z);
        // }
        #endregion
        #region 카메라 밖으로 나가지 못하게 하는 함수 (2)
        tr.position = new Vector3(Mathf.Clamp(tr.position.x, -8.5f, 8.5f), Mathf.Clamp(tr.position.y, -3.0f, 4.5f), tr.position.z);
        #endregion
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(Asteroid_Tag))
        {
            GameManager.instance.TurnOn();
            Destroy(other.gameObject);
            source.PlayOneShot(hitClip, 1.0f);
            GameObject effect = Instantiate(StarEffect, other.transform.position, Quaternion.identity);
            Destroy(effect, 0.5f);
        }
    }
    void QuitApp()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
        }
    }
    public void Fire()
    {
        Instantiate(coinBullet, firePos.position, Quaternion.identity);
    }

}
