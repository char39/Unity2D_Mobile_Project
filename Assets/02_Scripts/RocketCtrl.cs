using UnityEngine;

public class RocketCtrl : MonoBehaviour
{
    public GameObject StarEffect;
    public AudioSource source;
    public AudioClip hitClip;
    private Transform tr = null;
    public Rigidbody2D rb2D;
    [Header("Vars")]
    private float speed;
    private float h = 0f, v = 0f;
    private string Asteroid_Tag = "ASTEROID";
    private float halfHeight = 0.0f, halfWidth = 0.0f;
    public Transform firePos;
    public GameObject coinBullet;
    private Vector3 moveVector;
    [SerializeField] private JoyStick_TouchPad joyStick_TouchPad;

    void Start()
    {
        speed = 6.0f;
        rb2D = GetComponent<Rigidbody2D>();
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
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)        // 안드로이드 플랫폼일 때
            JoyStickCtrl();
        if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.OSXPlayer)  // 윈도우 에디터(유니티) 플랫폼일 때
            JoyStickCtrl();
    }
    private void JoyStickCtrl()
    {
        Vector2 speed = rb2D.velocity;
        speed.x = h * this.speed;
        speed.y = v * this.speed;
        rb2D.velocity = speed;
    }
    private void CameraOutLimit()
    {
        tr.position = new Vector3(Mathf.Clamp(tr.position.x, -8.5f, 8.5f), Mathf.Clamp(tr.position.y, -3.0f, 4.5f), tr.position.z);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(Asteroid_Tag))
        {
            source.PlayOneShot(hitClip, 1.0f);
        }
    }
    void QuitApp()
    {
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.OSXPlayer || Application.platform == RuntimePlatform.WindowsPlayer)
            if (Input.GetKeyDown(KeyCode.Escape))
                Application.Quit();
    }
    public void Fire()
    {
        Instantiate(coinBullet, firePos.position, Quaternion.identity);
    }

}
