using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //타임라인(포션 애니메이션)
    public TimeLineController timeLineController;
    //조이스틱
    public VirtualJoystick virtualJoystick;

    //몬스터에게 피격당했을때 색상바꿔주는 변수
    SpriteRenderer spriteRenderer;
    //몬스터에게 맞을때 이펙트
    public Animator animator;
    public GameObject explosionObj;
    //몬스터에게 맞을때 소리
    public AudioSource playerHitSound;
    //체력회복 아이템 먹는 소리
    public AudioSource playerHillSound;
    //체력아이템 풀체력인 상태에서는 무적 이펙트
    public GameObject barrier;
    bool isbarrier = false;
    public AudioSource barrierSound;
    public GameObject barrierBackImage;
    public GameObject brrierEffects;
    //플레이어 체력
    public int curHealth;
    //플레이어 이동
    Animator anim;
    Rigidbody2D rigid;
    public float speed;
    float h;
    float v;
    bool isHorizonMove;
    //모바일 키 변수
    int up_Value;
    int down_Value;
    int left_Value;
    int right_Value;
    bool up_Down;
    bool down_Down;
    bool left_Down;
    bool right_Down;
    bool up_Up;
    bool down_Up;
    bool left_Up;
    bool right_Up;


    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); //색변하게 해줘야해서 넣어줌
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        Move();
       
        
    }
    void GetInput()
    {
        //Move Value
        h = Input.GetAxisRaw("Horizontal") + right_Value + left_Value; //요즘은 블루투스 키보드이런게있어서 모바일이랑 pc랑 합쳐주는게 좋다.
        v = Input.GetAxisRaw("Vertical") + up_Value + down_Value;
    }
    void Move()
    {
        //조이스틱 버전---------------------------------------------------
        float x = virtualJoystick.Horizontal(); // 왼쪽 오른쪽
        float y = virtualJoystick.Vertical(); //위 아래
        
        if (x != 0 || y != 0)
        {
            transform.position += new Vector3(x, y, 0) * speed * Time.deltaTime;
        }
        
        
        //-----------------------------------------------------------------

        //버튼 버전
        //pc +  mobile //Check Button Down&Up
        bool hDown = Input.GetButtonDown("Horizontal") || right_Down || left_Down;
        bool vDown = Input.GetButtonDown("Vertical") || up_Down || down_Down;
        bool hUp = Input.GetButtonUp("Horizontal") || right_Up || left_Up;
        bool vUp = Input.GetButtonUp("Vertical") || up_Up || down_Up;

        
        //Check Horizontal Move
        if (hDown || vUp) //왼쪽이나 오른쪽을 눌렀으면 true
        {
            isHorizonMove = true;
        }else if (vDown || hUp) //위쪽이나 아래쪽을 눌렀으면 false
        {
            isHorizonMove = false;
        }else if (hUp || vUp)
        {
            isHorizonMove = h != 0;
        }
        
        //애니메이션
        anim.SetInteger("hAxisRaw", (int)h);
        if ((int)h == 1)
        {
            spriteRenderer.flipX = true; //스프라이트 이미지 방향전환
        }
        else if ((int)h == -1)
        {
            spriteRenderer.flipX = false; //스프라이트 이미지 방향전환
            
        }
        anim.SetInteger("vAxisRaw", (int)v);
        


        up_Down = false;
        down_Down = false;
        left_Down = false;
        right_Down = false;
        up_Up= false;
        down_Up = false;
        left_Up= false;
        right_Up = false;
    }
    void FixedUpdate()
    {
        Vector2 moveVec = isHorizonMove ? new Vector2(h, 0) : new Vector2(0, v);
                                            //맞으면               //아니면
        if (curHealth == 0)
        {
            anim.SetBool("isFinish", true);
            speed = 0;
            rigid.velocity = moveVec * speed;
        }
        else
        {
            rigid.velocity = moveVec * speed;
        }
        
    }

    public void ButtonDown(string type) //버튼누를 때
    {
        switch (type)
        {
            case "U":
                up_Value = 1;
                up_Down = true;
                break;
            case "D":
                down_Value = -1;
                down_Down = true;
                break;
            case "L":
                left_Value = -1;
                left_Down = true;
                break;
            case "R":
                right_Value = 1;
                right_Down = true;
                break;

        }
    }

    public void ButtonUp(string type) //버튼땠을 때
    {
        switch (type)
        {
            case "U":
                up_Value = 0;
                up_Up = true;
                break;
            case "D":
                down_Value = 0;
                down_Up = true;
                break;
            case "L":
                left_Value = 0;
                left_Up = true;
                break;
            case "R":
                right_Value = 0;
                right_Up = true;
                break;

        }
    }
 
 
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if(isbarrier == false) { 
                StartCoroutine(Explosion());
                playerHitSound.Play();
                spriteRenderer.color = Color.red;
                Invoke("ReturnSprite", 0.2f); 
                curHealth--;
            }
        }
        if (collision.gameObject.tag == "Item")
        {
            if(curHealth == 3)
            {
                playerHillSound.Play();
                StartCoroutine(Barrier());

                spriteRenderer.color = Color.yellow;
                Invoke("ReturnSprite", 7f);
            }
            else if(curHealth == 2)
            {
                curHealth += 1;
                spriteRenderer.color = Color.yellow;
                Invoke("ReturnSprite", 0.2f);
                playerHillSound.Play();
                timeLineController.playerHealth1Anim(); //체력충전 에니메이션
            }
            else if (curHealth == 1)
            {
                curHealth += 1;
                spriteRenderer.color = Color.yellow;
                Invoke("ReturnSprite", 0.2f);
                playerHillSound.Play();
                timeLineController.playerHealth2Anim();//체력충전 에니메이션
            }

        }

        
    }
    void ReturnSprite() //맞을때 레드변하고 다시 색 돌아오게
    {
        spriteRenderer.color = Color.white;
    }
    IEnumerator Explosion() //맞을때 터지는 이펙트
    {
        explosionObj.SetActive(true);
        animator.enabled = true;
        yield return new WaitForSeconds(1f);
        explosionObj.SetActive(false);
        animator.enabled = false;
    }

    IEnumerator Barrier() //체력 3개에서 체력아이템 먹으면 무적 이펙트
    {
         
        isbarrier = true;
        barrierBackImage.SetActive(true);
        barrierSound.Play();
        barrier.SetActive(true);
        brrierEffects.SetActive(true);
        //---베리어 커지고 작아지고 타임라인---
        timeLineController.playerBarrier1Anim(); //베리어1 에니메이션
        timeLineController.playerBarrier2Anim(); //베리어2 에니메이션
        timeLineController.playerBarrier3Anim(); //베리어3 에니메이션
        yield return new WaitForSeconds(7f); //무적 7초
              
        isbarrier = false;
        barrierSound.Stop();
        barrierBackImage.SetActive(false);
        barrier.SetActive(false);
        brrierEffects.SetActive(false);



    }
}
