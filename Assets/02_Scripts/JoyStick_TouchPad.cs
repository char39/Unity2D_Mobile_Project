using UnityEngine;
using UnityEngine.EventSystems;

public class JoyStick_TouchPad : MonoBehaviour
{
    private RectTransform rectTr_TouchPad;  // 터치패드의 RectTransform 컴포넌트
    private Vector3 startPos = Vector3.zero;    // 터치패드의 시작 위치
    private float dragRadius = 125.0f;   // 터치패드의 반지름
    private int touchID = -1;   // 터치패드 내에서의 터치 ID. -1이면 터치 없음, 0 이상이면 터치 중
    private bool isTouch = false;   // 터치 입력 중 여부
    public JoyStick_TouchPad joyStick_TouchPad;

    public Vector3 diff;
    [SerializeField] private RocketCtrl rocketCtrl;

    void Start()
    {
        joyStick_TouchPad = this;
        rectTr_TouchPad = GetComponent<RectTransform>();
        startPos = rectTr_TouchPad.position;
        rocketCtrl = GameObject.FindWithTag("Player").GetComponent<RocketCtrl>();
    }
    public void ButtonDown()
    {
        isTouch = true;
    }
    public void ButtonUp()
    {
        isTouch = false;
        rectTr_TouchPad.position = startPos;
        rocketCtrl.rb2D.velocity = Vector2.zero;  // 로켓 속도 초기화
    }

    private void FixedUpdate()  
    {
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
            HandleTouchInput();
        if (Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.OSXPlayer)
            HandleInput(Input.mousePosition);
    }
    void HandleTouchInput()     // 모바일용 패드 이동 함수
    {
        int i = 0;
        if (Input.touchCount > 0)   // 터치 입력이 있을 때
        {
            foreach (Touch touch in Input.touches)  // 모든 터치 입력에 대해 반복
            {
                i++;
                Vector2 touchPos = new Vector2(touch.position.x, touch.position.y); // 터치 입력 위치
                if (touch.phase == TouchPhase.Began)    // 터치가 시작되었을 때
                {
                    if (touch.position.x <= startPos.x + dragRadius && touch.position.y <= startPos.y + dragRadius &&
                        touch.position.x >= startPos.x - dragRadius && touch.position.y >= startPos.y - dragRadius) // 터치패드 내에서 터치가 시작되었는지 확인
                        touchID = i;    // 터치패드 내에서 터치 중
                }
                if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)    // 터치가 움직이거나 멈춰있을 때
                    if (touchID == i)   // 터치패드 내에서 터치 중일 때
                        HandleInput(touchPos);  // 터치 입력 위치로 이동
                if (touch.phase == TouchPhase.Ended) // 터치가 끝났을 때
                {
                    if (touchID == i)   // 터치패드 내에서 터치 중일 때
                    {
                        touchID = -1;   // 터치패드 내에서 터치 없음
                        ButtonUp();     // 터치패드 위치 초기화
                    }
                }
            }
        }
    }
    void HandleInput(Vector3 input)  // PC용 패드 이동 함수
    {
        if (isTouch)    // 터치 입력 중일 때
        {
            Vector3 diffVector = input - startPos;   // 터치 입력 위치와 터치패드 시작 위치의 차이
            if (diffVector.sqrMagnitude < dragRadius * dragRadius)    // 터치 입력 위치가 터치패드 내에 있을 때
                rectTr_TouchPad.position = startPos + diffVector;  // 터치 입력 위치로 이동
            else    // 터치 입력 위치가 터치패드 밖에 있을 때
            {
                diffVector = diffVector.normalized * dragRadius;    // 터치 입력 위치를 터치패드 밖으로 이동
                rectTr_TouchPad.position = startPos + diffVector;  // 터치 입력 위치로 이동
            }
        }
        diff = rectTr_TouchPad.position - startPos;  // 터치패드 위치와 터치패드 시작 위치의 차이
        Vector2 normalDiff = new Vector2(diff.x / dragRadius, diff.y / dragRadius);  // 터치패드 위치와 터치패드 시작 위치의 차이를 정규화

        if (rocketCtrl != null)
            rocketCtrl.OnStickPos(normalDiff);  // 로켓 이동 함수 호출
    }
}
