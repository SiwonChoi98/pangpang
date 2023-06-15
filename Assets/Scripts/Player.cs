using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Ÿ�Ӷ���(���� �ִϸ��̼�)
    public TimeLineController timeLineController;
    //���̽�ƽ
    public VirtualJoystick virtualJoystick;

    //���Ϳ��� �ǰݴ������� ����ٲ��ִ� ����
    SpriteRenderer spriteRenderer;
    //���Ϳ��� ������ ����Ʈ
    public Animator animator;
    public GameObject explosionObj;
    //���Ϳ��� ������ �Ҹ�
    public AudioSource playerHitSound;
    //ü��ȸ�� ������ �Դ� �Ҹ�
    public AudioSource playerHillSound;
    //ü�¾����� Ǯü���� ���¿����� ���� ����Ʈ
    public GameObject barrier;
    bool isbarrier = false;
    public AudioSource barrierSound;
    public GameObject barrierBackImage;
    public GameObject brrierEffects;
    //�÷��̾� ü��
    public int curHealth;
    //�÷��̾� �̵�
    Animator anim;
    Rigidbody2D rigid;
    public float speed;
    float h;
    float v;
    bool isHorizonMove;
    //����� Ű ����
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
        spriteRenderer = GetComponent<SpriteRenderer>(); //�����ϰ� ������ؼ� �־���
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
        h = Input.GetAxisRaw("Horizontal") + right_Value + left_Value; //������ ������� Ű�����̷����־ ������̶� pc�� �����ִ°� ����.
        v = Input.GetAxisRaw("Vertical") + up_Value + down_Value;
    }
    void Move()
    {
        //���̽�ƽ ����---------------------------------------------------
        float x = virtualJoystick.Horizontal(); // ���� ������
        float y = virtualJoystick.Vertical(); //�� �Ʒ�
        
        if (x != 0 || y != 0)
        {
            transform.position += new Vector3(x, y, 0) * speed * Time.deltaTime;
        }
        
        
        //-----------------------------------------------------------------

        //��ư ����
        //pc +  mobile //Check Button Down&Up
        bool hDown = Input.GetButtonDown("Horizontal") || right_Down || left_Down;
        bool vDown = Input.GetButtonDown("Vertical") || up_Down || down_Down;
        bool hUp = Input.GetButtonUp("Horizontal") || right_Up || left_Up;
        bool vUp = Input.GetButtonUp("Vertical") || up_Up || down_Up;

        
        //Check Horizontal Move
        if (hDown || vUp) //�����̳� �������� �������� true
        {
            isHorizonMove = true;
        }else if (vDown || hUp) //�����̳� �Ʒ����� �������� false
        {
            isHorizonMove = false;
        }else if (hUp || vUp)
        {
            isHorizonMove = h != 0;
        }
        
        //�ִϸ��̼�
        anim.SetInteger("hAxisRaw", (int)h);
        if ((int)h == 1)
        {
            spriteRenderer.flipX = true; //��������Ʈ �̹��� ������ȯ
        }
        else if ((int)h == -1)
        {
            spriteRenderer.flipX = false; //��������Ʈ �̹��� ������ȯ
            
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
                                            //������               //�ƴϸ�
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

    public void ButtonDown(string type) //��ư���� ��
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

    public void ButtonUp(string type) //��ư���� ��
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
                timeLineController.playerHealth1Anim(); //ü������ ���ϸ��̼�
            }
            else if (curHealth == 1)
            {
                curHealth += 1;
                spriteRenderer.color = Color.yellow;
                Invoke("ReturnSprite", 0.2f);
                playerHillSound.Play();
                timeLineController.playerHealth2Anim();//ü������ ���ϸ��̼�
            }

        }

        
    }
    void ReturnSprite() //������ ���庯�ϰ� �ٽ� �� ���ƿ���
    {
        spriteRenderer.color = Color.white;
    }
    IEnumerator Explosion() //������ ������ ����Ʈ
    {
        explosionObj.SetActive(true);
        animator.enabled = true;
        yield return new WaitForSeconds(1f);
        explosionObj.SetActive(false);
        animator.enabled = false;
    }

    IEnumerator Barrier() //ü�� 3������ ü�¾����� ������ ���� ����Ʈ
    {
         
        isbarrier = true;
        barrierBackImage.SetActive(true);
        barrierSound.Play();
        barrier.SetActive(true);
        brrierEffects.SetActive(true);
        //---������ Ŀ���� �۾����� Ÿ�Ӷ���---
        timeLineController.playerBarrier1Anim(); //������1 ���ϸ��̼�
        timeLineController.playerBarrier2Anim(); //������2 ���ϸ��̼�
        timeLineController.playerBarrier3Anim(); //������3 ���ϸ��̼�
        yield return new WaitForSeconds(7f); //���� 7��
              
        isbarrier = false;
        barrierSound.Stop();
        barrierBackImage.SetActive(false);
        barrier.SetActive(false);
        brrierEffects.SetActive(false);



    }
}
