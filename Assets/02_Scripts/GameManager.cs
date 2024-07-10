using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 1. Asteroid Prefab을 생성
// 2. 생성 시간 간격을 설정
// 3. 생성 크기를 랜덤하게 설정
// 4. 생성 위치를 랜덤하게 설정
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [Header("Spawn Vars")]
    public GameObject AsteroidPrefab;
    private float timePrev;
    private float yMinValue = -3.5f, yMaxValue = 5.0f;
    [Header("CameraShake Vars")]
    private Vector3 posCamera;   // 카메라의 위치
    private float hitBeginTime;  // 피격 시작 시간
    private bool isHit = false; // 피격 여부
    public bool isGameOver = false; // 게임오버 여부

    void Start()
    {
        timePrev = Time.time;
        instance = this;
    }

    void Update()
    {
        if (Time.time - timePrev > 2.0f)
        {
            timePrev = Time.time;
            SpawnAsteroid();
        }
        if (isHit)
        {
            float x = Random.Range(-0.05f, 0.05f);
            float y = Random.Range(-0.05f, 0.05f);
            Camera.main.transform.position += new Vector3(x, y, 0f);
            if (Time.time - hitBeginTime > 0.3f)
            {
                isHit = false;
                Camera.main.transform.position = posCamera;
            }
        }
    }
    public void TurnOn()
    {
        isHit = true;
        posCamera = Camera.main.transform.position;
        hitBeginTime = Time.time;
    }
    void SpawnAsteroid()
    {
        float RandomYpos = Random.Range(yMinValue, yMaxValue);
        float _Scale = Random.Range(1f, 2.5f);
        AsteroidPrefab.transform.localScale = Vector3.one * _Scale;
        Instantiate(AsteroidPrefab, new Vector3(13.0f, RandomYpos, AsteroidPrefab.transform.position.z), Quaternion.identity);
    }
}
