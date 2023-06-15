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
    //��ġ ���۽� 1ȸ
    public void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log("Touch Begin : " + eventData);
    }

    //��ġ ������ �� �� ������
    public void OnDrag(PointerEventData eventData)
    {
         touchPosition = Vector2.zero;

        //���̽�ƽ ��ġ�� ����ֵ� ������ ���� �����ϱ� ����
        //touchPosition�� ��ġ�� ���� �̹����� ���� ��ġ�� ��������
        //�󸶳� �־����ִ����� ���� �ٸ��� ���´�.
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(joystickImageBackGround.rectTransform, eventData.position, eventData.enterEventCamera, out touchPosition))
        {
            //touchPosition ���� ����ȭ [0~1]
            //touchPosition�� �̹��� ũ��� ����
            touchPosition.x = (touchPosition.x / joystickImageBackGround.rectTransform.sizeDelta.x);
            touchPosition.y = (touchPosition.y / joystickImageBackGround.rectTransform.sizeDelta.y);

            //touchPositon ���� ����ȭ[-n ~ n]
            //����(-1), �߽�(0), ������(1)�� �����ϱ����� touchPositon.x*2-1
            //�Ʒ�(-1), �߽�(0), ��(1)�� �����ϱ����� touchPositon.y*2-1
            //�̼����� Pivot�� ���� �޶�����. (���ϴ� Pivot ����)
            touchPosition = new Vector2(touchPosition.x * 2 - 1, touchPosition.y * 2 - 1);

            //touchPosition ���� ����ȭ [-1 ~ 1]
            //���� ���̽�ƽ ����̹����� ������ ������ �Ǹ� -1~1���� ū���� ���ü� �ִ�.
            //�̋� normailzed�� �̿��� -1 ~ 1 ������ ������ ����ȭ
            touchPosition = (touchPosition.magnitude > 1) ? touchPosition.normalized : touchPosition;

            //�������̽�ƽ ��Ʈ�ѷ� �̹��� �̵�
            joystickImageController.rectTransform.anchoredPosition = new Vector2(
                touchPosition.x * joystickImageBackGround.rectTransform.sizeDelta.x / 2,
                touchPosition.y * joystickImageBackGround.rectTransform.sizeDelta.y / 2);
            //Debug.Log("Touch & Drag : " + eventData);

        }
        
    }

    //��ġ ����� 1ȸ
    public void OnPointerUp(PointerEventData eventData)
    {
        //��ġ ����� ��Ʈ�ѷ� �̹��� �ٽ� �߾����� �����Ѵ�.
        joystickImageController.rectTransform.anchoredPosition = Vector2.zero;
        //�ٸ� ������Ʈ���� �̵��������� ����ϱ� ������ �̵����⵵ �ʱ�ȭ
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
