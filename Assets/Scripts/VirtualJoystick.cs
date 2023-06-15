using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class VirtualJoystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    private Image joystickImageBackGround;
    private Image joystickImageController;
    private Vector2 touchPosition;
    void Awake()
    {
        joystickImageBackGround = GetComponent<Image>();
        joystickImageController = transform.GetChild(0).GetComponent<Image>();
    }
    //터치 시작시 1회
    public void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log("Touch Begin : " + eventData);
    }

    //터치 상태일 때 매 프레임
    public void OnDrag(PointerEventData eventData)
    {
         touchPosition = Vector2.zero;

        //조이스틱 위치가 어디에있든 동일한 값을 연산하기 위해
        //touchPosition의 위치의 값은 이미지의 현재 위치를 기준으로
        //얼마나 멀어져있는지에 따라 다르게 나온다.
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(joystickImageBackGround.rectTransform, eventData.position, eventData.enterEventCamera, out touchPosition))
        {
            //touchPosition 값을 정규화 [0~1]
            //touchPosition을 이미지 크기로 나눔
            touchPosition.x = (touchPosition.x / joystickImageBackGround.rectTransform.sizeDelta.x);
            touchPosition.y = (touchPosition.y / joystickImageBackGround.rectTransform.sizeDelta.y);

            //touchPositon 값의 정규화[-n ~ n]
            //왼쪽(-1), 중심(0), 오른쪽(1)로 번경하기위해 touchPositon.x*2-1
            //아래(-1), 중심(0), 위(1)로 번경하기위해 touchPositon.y*2-1
            //이수식은 Pivot에 따라 달라진다. (최하단 Pivot 기준)
            touchPosition = new Vector2(touchPosition.x * 2 - 1, touchPosition.y * 2 - 1);

            //touchPosition 값의 정규화 [-1 ~ 1]
            //가상 조이스틱 배경이미지가 밖으로 나가게 되면 -1~1보다 큰값이 나올수 있다.
            //이떄 normailzed를 이용해 -1 ~ 1 사이의 값으로 정규화
            touchPosition = (touchPosition.magnitude > 1) ? touchPosition.normalized : touchPosition;

            //가상조이스틱 컨트롤러 이미지 이동
            joystickImageController.rectTransform.anchoredPosition = new Vector2(
                touchPosition.x * joystickImageBackGround.rectTransform.sizeDelta.x / 2,
                touchPosition.y * joystickImageBackGround.rectTransform.sizeDelta.y / 2);
            //Debug.Log("Touch & Drag : " + eventData);

        }
        
    }

    //터치 종료시 1회
    public void OnPointerUp(PointerEventData eventData)
    {
        //터치 종료시 컨트롤러 이미지 다시 중앙으로 오게한다.
        joystickImageController.rectTransform.anchoredPosition = Vector2.zero;
        //다른 프로젝트에서 이동방향으로 사용하기 때문에 이동방향도 초기화
        touchPosition = Vector2.zero;
       // Debug.Log("Touch Ended : " + eventData);
    }
    public float Horizontal()
    {
        return touchPosition.x;
    }

    public float Vertical()
    {
        return touchPosition.y;
    }
}
