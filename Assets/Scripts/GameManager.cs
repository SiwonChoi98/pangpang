using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public ReplayButton replayButton;
    //포션관련
    public GameObject potion;
    public Transform[] potionPoints; //포션 생성위치
    
    //적생성
    public string[] enemyObjs; //적 프리팹 배열
    public Transform[] spawnPoints;//적 생성위치
    public float maxspawnDelay;//적 생성 딜레이
    public float curspawnDelay;
    public ObjectManager objectManager;
    SpriteRenderer spriteRenderer;
    [Header("UI")]
    //점수
    public int savedScore;
    public int score = 0;
    public int bestScore = 0;
    public Text scoreTxt;
    public Text gameOverScoreTxt;
    public float time; //스테이지 점수 올라가는거 델타타임으로 올려주기
    //스테이지 숫자
    public GameObject[] stageNum;
    public int stage = 1;
    //스테이지 올라갈 때 마다 맵바꿔주기
    public GameObject[] map;
    //플레이어 체력
    public Player player;
    public GameObject[] playerHealth;
    //게임오버 판넬
    public GameObject gameOverPanel;
    //설정 메뉴 판넬
    public GameObject settingPanel;
    //사운드 이미지
    public GameObject SoundOpen; //켜지는 이미지
    public GameObject SoundClose; //꺼지는 이미지
    public AudioSource backgroundSound; //게임배경소리
    public AudioSource uiSound; //ui 클릭 사운드
    public AudioSource deadSound; //죽었을때 배경소리
    //옵션 조이스틱 on/off
    public GameObject joystickKey;
    public GameObject moveKey;

    public GameObject onButton;
    public GameObject offButton;

    public static GameManager instance;
    //public RectTransform HealthBar;
    //public Player player;
    
    void Awake()
    {
        if (instance == null) { instance = this; }
        else if (instance != this) { Destroy(gameObject); }//gameObject는 현재 scripts가 포함된 GameObject를 의미한다. //중복될경우 오브젝트 제거
        savedScore = PlayerPrefs.GetInt("BestScore2"); //키를 받아서 savedScore로 저장후 죽었을때 비교해서 score가 크면 넘겨줌
        gameOverPanel.SetActive(false); //게임오버 판넬 시작시 다시 비활성화
        enemyObjs = new string[]{"enemyA", "enemyB", "enemyC" , "enemyD"};
        spriteRenderer = GetComponent<SpriteRenderer>(); //색변하게 해줘야해서 넣어줌
      
    }

    void Update()
    { 
        
        curspawnDelay += Time.deltaTime;
        time += Time.deltaTime;

        if (player.curHealth > 0)
        {
            if (time > 1) //점수 올리기
            {
                score += 100;
                time = 0;
            }
            if (curspawnDelay > maxspawnDelay) //적 생성주기
            {
                SpawnEnemy();       
                maxspawnDelay = Random.Range(0.5f, 2.7f); //적생성주기 랜덤값
                curspawnDelay = 0; //적 생성후에는 다시 초기화
            }
            
        }
        else
        {
            if (score > savedScore) 
            {
                PlayerPrefs.SetInt("BestScore", score); //퍼스트씬에 스코어 점수 넘기기   
            }
            else
            {
                return;
            }
        }

    }
    
    //적생성코드
    void SpawnEnemy()
    {
        int potionRan = Random.Range(0, 25);
        
        int potionRanPoint = Random.Range(0, 8); //포션 생성위치
        int ranPoint = Random.Range(0, 8);//아래쪽으로) 세로몬스터 나오는 적생성위치
        int ranPoint2 = Random.Range(8, 21); //왼쪽으로) 가로몬스터 나오는 적생성위치
        int ranPoint3 = Random.Range(21, 32); //오른쪽으로) 가로몬스터 나오는 적생성위치
        int ranPoint4 = Random.Range(32, 39); //위쪽으로) 세로몬스터 나오는 적생성위치

       
        
        if (potionRan > 23) //포션생성 
        {
            Instantiate(potion, potionPoints[potionRanPoint].position, potion.transform.rotation);
            
        }
        

        GameObject enemy = objectManager.MakeObj(enemyObjs[0]);
        enemy.transform.position = spawnPoints[ranPoint].position;
        Rigidbody2D rigid = enemy.GetComponent<Rigidbody2D>();
        

        if (score > 50000) //7스테이지
        {
            stage++;
            stageNum[0].SetActive(false);
            stageNum[5].SetActive(false);
            stageNum[6].SetActive(true);
            
            map[5].SetActive(true);
            map[4].SetActive(false);
            
            spriteRenderer = enemy.GetComponent<SpriteRenderer>(); //위에서 나오는 적 색 바꿔주기
            spriteRenderer.color = Color.red;
            rigid.velocity = Vector2.down * 5f;

            GameObject enemy2 = objectManager.MakeObj(enemyObjs[1]);
            enemy2.transform.position = spawnPoints[ranPoint2].position;
            Rigidbody2D rigid2 = enemy2.GetComponent<Rigidbody2D>();
            spriteRenderer = enemy2.GetComponent<SpriteRenderer>(); //위에서 나오는 적 색 바꿔주기
            spriteRenderer.color = Color.red;
            rigid2.velocity = Vector2.right * 5f;

            GameObject enemy3 = objectManager.MakeObj(enemyObjs[2]);
            enemy3.transform.position = spawnPoints[ranPoint3].position;
            Rigidbody2D rigid3 = enemy3.GetComponent<Rigidbody2D>();
            spriteRenderer = enemy3.GetComponent<SpriteRenderer>(); //위에서 나오는 적 색 바꿔주기
            spriteRenderer.color = Color.red;
            rigid3.velocity = Vector2.left * 5f;

            GameObject enemy4 = objectManager.MakeObj(enemyObjs[3]);
            enemy4.transform.position = spawnPoints[ranPoint4].position;
            Rigidbody2D rigid4 = enemy4.GetComponent<Rigidbody2D>();
            spriteRenderer = enemy4.GetComponent<SpriteRenderer>(); //위에서 나오는 적 색 바꿔주기
            spriteRenderer.color = Color.red;
            rigid4.velocity = Vector2.up * 5f;
        }
        else if(score > 30000) //6스테이지
        {
            stage++;
            stageNum[0].SetActive(false);
            stageNum[4].SetActive(false);
            stageNum[5].SetActive(true);
            
            map[4].SetActive(true);
            map[3].SetActive(false);
            
            spriteRenderer = enemy.GetComponent<SpriteRenderer>(); //위에서 나오는 적 색 바꿔주기
            spriteRenderer.color = Color.yellow;
            rigid.velocity = Vector2.down * 4f;

            GameObject enemy2 = objectManager.MakeObj(enemyObjs[1]);
            enemy2.transform.position = spawnPoints[ranPoint2].position;
            Rigidbody2D rigid2 = enemy2.GetComponent<Rigidbody2D>();
            spriteRenderer = enemy2.GetComponent<SpriteRenderer>(); //위에서 나오는 적 색 바꿔주기
            spriteRenderer.color = Color.yellow;
            rigid2.velocity = Vector2.right * 4f;

            GameObject enemy3 = objectManager.MakeObj(enemyObjs[2]);
            enemy3.transform.position = spawnPoints[ranPoint3].position;
            Rigidbody2D rigid3 = enemy3.GetComponent<Rigidbody2D>();
            spriteRenderer = enemy3.GetComponent<SpriteRenderer>(); //위에서 나오는 적 색 바꿔주기
            spriteRenderer.color = Color.yellow;
            rigid3.velocity = Vector2.left * 4f;

            GameObject enemy4 = objectManager.MakeObj(enemyObjs[3]);
            enemy4.transform.position = spawnPoints[ranPoint4].position;
            Rigidbody2D rigid4 = enemy4.GetComponent<Rigidbody2D>();
            spriteRenderer = enemy4.GetComponent<SpriteRenderer>(); //위에서 나오는 적 색 바꿔주기
            spriteRenderer.color = Color.yellow;
            rigid4.velocity = Vector2.up * 4f;
        }
        else if(score > 20000) //5스테이지
        {
            stage++;
            stageNum[0].SetActive(false);
            stageNum[3].SetActive(false);
            stageNum[4].SetActive(true);
            
            map[3].SetActive(true);
            map[2].SetActive(false);
           
            spriteRenderer = enemy.GetComponent<SpriteRenderer>(); //위에서 나오는 적 색 바꿔주기
            spriteRenderer.color = Color.yellow;
            rigid.velocity = Vector2.down * 4f;

            GameObject enemy2 = objectManager.MakeObj(enemyObjs[1]);
            enemy2.transform.position = spawnPoints[ranPoint2].position;
            Rigidbody2D rigid2 = enemy2.GetComponent<Rigidbody2D>();
            spriteRenderer = enemy2.GetComponent<SpriteRenderer>(); //위에서 나오는 적 색 바꿔주기
            spriteRenderer.color = Color.yellow;
            rigid2.velocity = Vector2.right * 4f;

            GameObject enemy3 = objectManager.MakeObj(enemyObjs[2]);
            enemy3.transform.position = spawnPoints[ranPoint3].position;
            Rigidbody2D rigid3 = enemy3.GetComponent<Rigidbody2D>();
            spriteRenderer = enemy3.GetComponent<SpriteRenderer>(); //위에서 나오는 적 색 바꿔주기
            spriteRenderer.color = Color.green;
            rigid3.velocity = Vector2.left * 3f;

            GameObject enemy4 = objectManager.MakeObj(enemyObjs[3]);
            enemy4.transform.position = spawnPoints[ranPoint4].position;
            Rigidbody2D rigid4 = enemy4.GetComponent<Rigidbody2D>();
            spriteRenderer = enemy4.GetComponent<SpriteRenderer>(); //위에서 나오는 적 색 바꿔주기
            spriteRenderer.color = Color.green;
            rigid4.velocity = Vector2.up * 3f;
        }
        else if (score > 15000) // 4스테이지
        {
            stage++;
            stageNum[0].SetActive(false);
            stageNum[2].SetActive(false);
            stageNum[3].SetActive(true);
            
            map[2].SetActive(true);
            map[1].SetActive(false);
           
            spriteRenderer = enemy.GetComponent<SpriteRenderer>(); //위에서 나오는 적 색 바꿔주기
            spriteRenderer.color = Color.green;
            rigid.velocity = Vector2.down * 3f;

            GameObject enemy2 = objectManager.MakeObj(enemyObjs[1]);
            enemy2.transform.position = spawnPoints[ranPoint2].position;
            Rigidbody2D rigid2 = enemy2.GetComponent<Rigidbody2D>();
            spriteRenderer = enemy2.GetComponent<SpriteRenderer>(); //위에서 나오는 적 색 바꿔주기
            spriteRenderer.color = Color.green;
            rigid2.velocity = Vector2.right * 3f;

            GameObject enemy3 = objectManager.MakeObj(enemyObjs[2]);
            enemy3.transform.position = spawnPoints[ranPoint3].position;
            Rigidbody2D rigid3 = enemy3.GetComponent<Rigidbody2D>();
            spriteRenderer = enemy3.GetComponent<SpriteRenderer>(); //위에서 나오는 적 색 바꿔주기
            spriteRenderer.color = Color.green;
            rigid3.velocity = Vector2.left * 3f;

            GameObject enemy4 = objectManager.MakeObj(enemyObjs[3]);
            enemy4.transform.position = spawnPoints[ranPoint4].position;
            Rigidbody2D rigid4 = enemy4.GetComponent<Rigidbody2D>();
            rigid4.velocity = Vector2.up * 2f;
        }
        else if (score > 10000) //3스테이지
        {
            stage++;
            stageNum[1].SetActive(false);
            stageNum[2].SetActive(true);
            
            map[1].SetActive(true);
            map[0].SetActive(false);
            
            spriteRenderer = enemy.GetComponent<SpriteRenderer>(); //위에서 나오는 적 색 바꿔주기
            spriteRenderer.color = Color.green;
            rigid.velocity = Vector2.down * 3f;

            GameObject enemy2 = objectManager.MakeObj(enemyObjs[1]);
            enemy2.transform.position = spawnPoints[ranPoint2].position;
            Rigidbody2D rigid2 = enemy2.GetComponent<Rigidbody2D>();
            spriteRenderer = enemy2.GetComponent<SpriteRenderer>(); //위에서 나오는 적 색 바꿔주기
            spriteRenderer.color = Color.green;
            rigid2.velocity = Vector2.right * 3f;

            GameObject enemy3 = objectManager.MakeObj(enemyObjs[2]);
            enemy3.transform.position = spawnPoints[ranPoint3].position;
            Rigidbody2D rigid3 = enemy3.GetComponent<Rigidbody2D>();
            rigid3.velocity = Vector2.left * 2f;
        }
        else if (score > 5000) //2스테이지 
        {
            stage++;
            stageNum[0].SetActive(false);
            stageNum[1].SetActive(true);
            map[0].SetActive(true);

            spriteRenderer = enemy.GetComponent<SpriteRenderer>(); //위에서 나오는 적 색 바꿔주기
            spriteRenderer.color = Color.green;
            rigid.velocity = Vector2.down * 3f;

            GameObject enemy2 = objectManager.MakeObj(enemyObjs[1]);
            enemy2.transform.position = spawnPoints[ranPoint2].position;
            Rigidbody2D rigid2 = enemy2.GetComponent<Rigidbody2D>();
            rigid2.velocity = Vector2.right * 2f;

        }
        else //1스테이지
        {
            rigid.velocity = Vector2.down * 2f;
        }
        
      
    }

   
    void LateUpdate()
    {
        //점수
        scoreTxt.text = "" + score; //게임판넬에서 나오는 점수
        gameOverScoreTxt.text = "" + score; //게임오버판넬에서 나오는 점수
        //플레이어 체력 깍이면 하트 하나씩 사라지게
        if (player.curHealth == 3)
        {
            for(int i=0; i<player.curHealth; i++)
            {
                playerHealth[i].SetActive(true);
            }
        }else if (player.curHealth == 2)
        {
            playerHealth[0].SetActive(false);
            playerHealth[1].SetActive(true);
            playerHealth[2].SetActive(true);
        }
        else if(player.curHealth == 1)
        {
            playerHealth[0].SetActive(false);
            playerHealth[1].SetActive(false);
            playerHealth[2].SetActive(true);
        }
        else
        {
            replayButton.ShowAd();
            gameOverPanel.SetActive(true); //게임오버 판넬
            
            playerHealth[0].SetActive(false);
            playerHealth[1].SetActive(false);
            playerHealth[2].SetActive(false);
        }
    }

    //Game Over Panel Button && Sound Button
    public void HomeButton()
    {
        SceneManager.LoadScene("FirstScene");
        uiSound.Play();
    }
   
    public void SoundOpenButton()
    {
        
        if (SoundOpen.activeSelf)
        {
            backgroundSound.Pause();
            SoundOpen.SetActive(false);
            SoundClose.SetActive(true);
            uiSound.Play();
        }
    }
    public void SoundCloseButton()
    {
        if (!SoundOpen.activeSelf)
        {
            backgroundSound.Play();
            SoundOpen.SetActive(true);
            SoundClose.SetActive(false);
            uiSound.Play();
        }
    }
    public void settingOpen()
    {
        settingPanel.SetActive(true);
        uiSound.Play();
    }
    public void settingExit()
    {
        settingPanel.SetActive(false);
        uiSound.Play();
    }

    //setting 조이스틱 on/off
    public void JoystickOn()
    {
        onButton.SetActive(false);
        offButton.SetActive(true);
        joystickKey.SetActive(false);
        moveKey.SetActive(true);
        uiSound.Play();
    }
    public void JoystickOff()
    {
        onButton.SetActive(true);
        offButton.SetActive(false);
        joystickKey.SetActive(true);
        moveKey.SetActive(false);
        uiSound.Play();
    }
    
    
}
