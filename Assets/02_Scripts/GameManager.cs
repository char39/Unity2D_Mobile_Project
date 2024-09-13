using System.Collections.Generic;
using UnityEngine;

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

    public List<GameObject> AsteList;
    public List<GameObject> ExplosionList;
    public GameObject ExplosionPrefab;

    void Awake()
    {
        timePrev = Time.time;
        instance = this;
    }

    void Start()
    {
        CreateAsteroidPool();
        CreateExplosionPool();
    }

    void Update()
    {
        if (Time.time - timePrev > 0.25f)
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
        int _AsteroidIndex = GetAsteroidFromPool();
        if (_AsteroidIndex == -1) return;
        AsteList[_AsteroidIndex].transform.position = new Vector3(13.0f, RandomYpos, AsteroidPrefab.transform.position.z);
        AsteList[_AsteroidIndex].transform.rotation = Quaternion.identity;
        AsteList[_AsteroidIndex].transform.localScale = Vector3.one * _Scale;
        AsteList[_AsteroidIndex].SetActive(true);
    }

    private void CreateAsteroidPool()
    {
        for (int i = 0; i < 30; i++)
        {
            GameObject _Asteroid = Instantiate(AsteroidPrefab, GameObject.Find("ObjPool").transform);
            _Asteroid.name = "Asteroid_" + (i + 1).ToString("00");
            AsteList.Add(_Asteroid);
            _Asteroid.SetActive(false);
        }
    }
    private int GetAsteroidFromPool()
    {
        for (int i = 0; i < AsteList.Count; i++)
        {
            if (!AsteList[i].activeSelf)
            {
                AsteList[i].SetActive(true);
                return i;
            }
        }
        return -1;
    }

    private void CreateExplosionPool()
    {
        for (int i = 0; i < 30; i++)
        {
            GameObject _Explosion = Instantiate(ExplosionPrefab, GameObject.Find("ObjPoolEff").transform);
            _Explosion.name = "Explosion_" + (i + 1).ToString("00");
            ExplosionList.Add(_Explosion);
            _Explosion.SetActive(false);
        }
    }
    public void SetActiveExplosionFromPool(Transform tr)
    {
        for (int i = 0; i < ExplosionList.Count; i++)
        {
            if (!ExplosionList[i].activeSelf)
            {
                ExplosionList[i].SetActive(true);
                ExplosionList[i].transform.position = new Vector3(tr.position.x - 1, tr.position.y, tr.position.z);
                ExplosionList[i].transform.rotation = Quaternion.identity;
                break;
            }
        }
    }
}
