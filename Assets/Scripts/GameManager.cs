using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public ReplayButton replayButton;
    //���ǰ���
    public GameObject potion;
    public Transform[] potionPoints; //���� ������ġ
    
    //������
    public string[] enemyObjs; //�� ������ �迭
    public Transform[] spawnPoints;//�� ������ġ
    public float maxspawnDelay;//�� ���� ������
    public float curspawnDelay;
    public ObjectManager objectManager;
    SpriteRenderer spriteRenderer;
    [Header("UI")]
    //����
    public int savedScore;
    public int score = 0;
    public int bestScore = 0;
    public Text scoreTxt;
    public Text gameOverScoreTxt;
    public float time; //�������� ���� �ö󰡴°� ��ŸŸ������ �÷��ֱ�
    //�������� ����
    public GameObject[] stageNum;
    public int stage = 1;
    //�������� �ö� �� ���� �ʹٲ��ֱ�
    public GameObject[] map;
    //�÷��̾� ü��
    public Player player;
    public GameObject[] playerHealth;
    //���ӿ��� �ǳ�
    public GameObject gameOverPanel;
    //���� �޴� �ǳ�
    public GameObject settingPanel;
    //���� �̹���
    public GameObject SoundOpen; //������ �̹���
    public GameObject SoundClose; //������ �̹���
    public AudioSource backgroundSound; //���ӹ��Ҹ�
    public AudioSource uiSound; //ui Ŭ�� ����
    public AudioSource deadSound; //�׾����� ���Ҹ�
    //�ɼ� ���̽�ƽ on/off
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
        else if (instance != this) { Destroy(gameObject); }//gameObject�� ���� scripts�� ���Ե� GameObject�� �ǹ��Ѵ�. //�ߺ��ɰ�� ������Ʈ ����
        savedScore = PlayerPrefs.GetInt("BestScore2"); //Ű�� �޾Ƽ� savedScore�� ������ �׾����� ���ؼ� score�� ũ�� �Ѱ���
        gameOverPanel.SetActive(false); //���ӿ��� �ǳ� ���۽� �ٽ� ��Ȱ��ȭ
        enemyObjs = new string[]{"enemyA", "enemyB", "enemyC" , "enemyD"};
        spriteRenderer = GetComponent<SpriteRenderer>(); //�����ϰ� ������ؼ� �־���
      
    }

    void Update()
    { 
        
        curspawnDelay += Time.deltaTime;
        time += Time.deltaTime;

        if (player.curHealth > 0)
        {
            if (time > 1) //���� �ø���
            {
                score += 100;
                time = 0;
            }
            if (curspawnDelay > maxspawnDelay) //�� �����ֱ�
            {
                SpawnEnemy();       
                maxspawnDelay = Random.Range(0.5f, 2.7f); //�������ֱ� ������
                curspawnDelay = 0; //�� �����Ŀ��� �ٽ� �ʱ�ȭ
            }
            
        }
        else
        {
            if (score > savedScore) 
            {
                PlayerPrefs.SetInt("BestScore", score); //�۽�Ʈ���� ���ھ� ���� �ѱ��   
            }
            else
            {
                return;
            }
        }

    }
    
    //�������ڵ�
    void SpawnEnemy()
    {
        int potionRan = Random.Range(0, 25);
        
        int potionRanPoint = Random.Range(0, 8); //���� ������ġ
        int ranPoint = Random.Range(0, 8);//�Ʒ�������) ���θ��� ������ ��������ġ
        int ranPoint2 = Random.Range(8, 21); //��������) ���θ��� ������ ��������ġ
        int ranPoint3 = Random.Range(21, 32); //����������) ���θ��� ������ ��������ġ
        int ranPoint4 = Random.Range(32, 39); //��������) ���θ��� ������ ��������ġ

       
        
        if (potionRan > 23) //���ǻ��� 
        {
            Instantiate(potion, potionPoints[potionRanPoint].position, potion.transform.rotation);
            
        }
        

        GameObject enemy = objectManager.MakeObj(enemyObjs[0]);
        enemy.transform.position = spawnPoints[ranPoint].position;
        Rigidbody2D rigid = enemy.GetComponent<Rigidbody2D>();
        

        if (score > 50000) //7��������
        {
            stage++;
            stageNum[0].SetActive(false);
            stageNum[5].SetActive(false);
            stageNum[6].SetActive(true);
            
            map[5].SetActive(true);
            map[4].SetActive(false);
            
            spriteRenderer = enemy.GetComponent<SpriteRenderer>(); //������ ������ �� �� �ٲ��ֱ�
            spriteRenderer.color = Color.red;
            rigid.velocity = Vector2.down * 5f;

            GameObject enemy2 = objectManager.MakeObj(enemyObjs[1]);
            enemy2.transform.position = spawnPoints[ranPoint2].position;
            Rigidbody2D rigid2 = enemy2.GetComponent<Rigidbody2D>();
            spriteRenderer = enemy2.GetComponent<SpriteRenderer>(); //������ ������ �� �� �ٲ��ֱ�
            spriteRenderer.color = Color.red;
            rigid2.velocity = Vector2.right * 5f;

            GameObject enemy3 = objectManager.MakeObj(enemyObjs[2]);
            enemy3.transform.position = spawnPoints[ranPoint3].position;
            Rigidbody2D rigid3 = enemy3.GetComponent<Rigidbody2D>();
            spriteRenderer = enemy3.GetComponent<SpriteRenderer>(); //������ ������ �� �� �ٲ��ֱ�
            spriteRenderer.color = Color.red;
            rigid3.velocity = Vector2.left * 5f;

            GameObject enemy4 = objectManager.MakeObj(enemyObjs[3]);
            enemy4.transform.position = spawnPoints[ranPoint4].position;
            Rigidbody2D rigid4 = enemy4.GetComponent<Rigidbody2D>();
            spriteRenderer = enemy4.GetComponent<SpriteRenderer>(); //������ ������ �� �� �ٲ��ֱ�
            spriteRenderer.color = Color.red;
            rigid4.velocity = Vector2.up * 5f;
        }
        else if(score > 30000) //6��������
        {
            stage++;
            stageNum[0].SetActive(false);
            stageNum[4].SetActive(false);
            stageNum[5].SetActive(true);
            
            map[4].SetActive(true);
            map[3].SetActive(false);
            
            spriteRenderer = enemy.GetComponent<SpriteRenderer>(); //������ ������ �� �� �ٲ��ֱ�
            spriteRenderer.color = Color.yellow;
            rigid.velocity = Vector2.down * 4f;

            GameObject enemy2 = objectManager.MakeObj(enemyObjs[1]);
            enemy2.transform.position = spawnPoints[ranPoint2].position;
            Rigidbody2D rigid2 = enemy2.GetComponent<Rigidbody2D>();
            spriteRenderer = enemy2.GetComponent<SpriteRenderer>(); //������ ������ �� �� �ٲ��ֱ�
            spriteRenderer.color = Color.yellow;
            rigid2.velocity = Vector2.right * 4f;

            GameObject enemy3 = objectManager.MakeObj(enemyObjs[2]);
            enemy3.transform.position = spawnPoints[ranPoint3].position;
            Rigidbody2D rigid3 = enemy3.GetComponent<Rigidbody2D>();
            spriteRenderer = enemy3.GetComponent<SpriteRenderer>(); //������ ������ �� �� �ٲ��ֱ�
            spriteRenderer.color = Color.yellow;
            rigid3.velocity = Vector2.left * 4f;

            GameObject enemy4 = objectManager.MakeObj(enemyObjs[3]);
            enemy4.transform.position = spawnPoints[ranPoint4].position;
            Rigidbody2D rigid4 = enemy4.GetComponent<Rigidbody2D>();
            spriteRenderer = enemy4.GetComponent<SpriteRenderer>(); //������ ������ �� �� �ٲ��ֱ�
            spriteRenderer.color = Color.yellow;
            rigid4.velocity = Vector2.up * 4f;
        }
        else if(score > 20000) //5��������
        {
            stage++;
            stageNum[0].SetActive(false);
            stageNum[3].SetActive(false);
            stageNum[4].SetActive(true);
            
            map[3].SetActive(true);
            map[2].SetActive(false);
           
            spriteRenderer = enemy.GetComponent<SpriteRenderer>(); //������ ������ �� �� �ٲ��ֱ�
            spriteRenderer.color = Color.yellow;
            rigid.velocity = Vector2.down * 4f;

            GameObject enemy2 = objectManager.MakeObj(enemyObjs[1]);
            enemy2.transform.position = spawnPoints[ranPoint2].position;
            Rigidbody2D rigid2 = enemy2.GetComponent<Rigidbody2D>();
            spriteRenderer = enemy2.GetComponent<SpriteRenderer>(); //������ ������ �� �� �ٲ��ֱ�
            spriteRenderer.color = Color.yellow;
            rigid2.velocity = Vector2.right * 4f;

            GameObject enemy3 = objectManager.MakeObj(enemyObjs[2]);
            enemy3.transform.position = spawnPoints[ranPoint3].position;
            Rigidbody2D rigid3 = enemy3.GetComponent<Rigidbody2D>();
            spriteRenderer = enemy3.GetComponent<SpriteRenderer>(); //������ ������ �� �� �ٲ��ֱ�
            spriteRenderer.color = Color.green;
            rigid3.velocity = Vector2.left * 3f;

            GameObject enemy4 = objectManager.MakeObj(enemyObjs[3]);
            enemy4.transform.position = spawnPoints[ranPoint4].position;
            Rigidbody2D rigid4 = enemy4.GetComponent<Rigidbody2D>();
            spriteRenderer = enemy4.GetComponent<SpriteRenderer>(); //������ ������ �� �� �ٲ��ֱ�
            spriteRenderer.color = Color.green;
            rigid4.velocity = Vector2.up * 3f;
        }
        else if (score > 15000) // 4��������
        {
            stage++;
            stageNum[0].SetActive(false);
            stageNum[2].SetActive(false);
            stageNum[3].SetActive(true);
            
            map[2].SetActive(true);
            map[1].SetActive(false);
           
            spriteRenderer = enemy.GetComponent<SpriteRenderer>(); //������ ������ �� �� �ٲ��ֱ�
            spriteRenderer.color = Color.green;
            rigid.velocity = Vector2.down * 3f;

            GameObject enemy2 = objectManager.MakeObj(enemyObjs[1]);
            enemy2.transform.position = spawnPoints[ranPoint2].position;
            Rigidbody2D rigid2 = enemy2.GetComponent<Rigidbody2D>();
            spriteRenderer = enemy2.GetComponent<SpriteRenderer>(); //������ ������ �� �� �ٲ��ֱ�
            spriteRenderer.color = Color.green;
            rigid2.velocity = Vector2.right * 3f;

            GameObject enemy3 = objectManager.MakeObj(enemyObjs[2]);
            enemy3.transform.position = spawnPoints[ranPoint3].position;
            Rigidbody2D rigid3 = enemy3.GetComponent<Rigidbody2D>();
            spriteRenderer = enemy3.GetComponent<SpriteRenderer>(); //������ ������ �� �� �ٲ��ֱ�
            spriteRenderer.color = Color.green;
            rigid3.velocity = Vector2.left * 3f;

            GameObject enemy4 = objectManager.MakeObj(enemyObjs[3]);
            enemy4.transform.position = spawnPoints[ranPoint4].position;
            Rigidbody2D rigid4 = enemy4.GetComponent<Rigidbody2D>();
            rigid4.velocity = Vector2.up * 2f;
        }
        else if (score > 10000) //3��������
        {
            stage++;
            stageNum[1].SetActive(false);
            stageNum[2].SetActive(true);
            
            map[1].SetActive(true);
            map[0].SetActive(false);
            
            spriteRenderer = enemy.GetComponent<SpriteRenderer>(); //������ ������ �� �� �ٲ��ֱ�
            spriteRenderer.color = Color.green;
            rigid.velocity = Vector2.down * 3f;

            GameObject enemy2 = objectManager.MakeObj(enemyObjs[1]);
            enemy2.transform.position = spawnPoints[ranPoint2].position;
            Rigidbody2D rigid2 = enemy2.GetComponent<Rigidbody2D>();
            spriteRenderer = enemy2.GetComponent<SpriteRenderer>(); //������ ������ �� �� �ٲ��ֱ�
            spriteRenderer.color = Color.green;
            rigid2.velocity = Vector2.right * 3f;

            GameObject enemy3 = objectManager.MakeObj(enemyObjs[2]);
            enemy3.transform.position = spawnPoints[ranPoint3].position;
            Rigidbody2D rigid3 = enemy3.GetComponent<Rigidbody2D>();
            rigid3.velocity = Vector2.left * 2f;
        }
        else if (score > 5000) //2�������� 
        {
            stage++;
            stageNum[0].SetActive(false);
            stageNum[1].SetActive(true);
            map[0].SetActive(true);

            spriteRenderer = enemy.GetComponent<SpriteRenderer>(); //������ ������ �� �� �ٲ��ֱ�
            spriteRenderer.color = Color.green;
            rigid.velocity = Vector2.down * 3f;

            GameObject enemy2 = objectManager.MakeObj(enemyObjs[1]);
            enemy2.transform.position = spawnPoints[ranPoint2].position;
            Rigidbody2D rigid2 = enemy2.GetComponent<Rigidbody2D>();
            rigid2.velocity = Vector2.right * 2f;

        }
        else //1��������
        {
            rigid.velocity = Vector2.down * 2f;
        }
        
      
    }

   
    void LateUpdate()
    {
        //����
        scoreTxt.text = "" + score; //�����ǳڿ��� ������ ����
        gameOverScoreTxt.text = "" + score; //���ӿ����ǳڿ��� ������ ����
        //�÷��̾� ü�� ���̸� ��Ʈ �ϳ��� �������
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
            gameOverPanel.SetActive(true); //���ӿ��� �ǳ�
            
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

    //setting ���̽�ƽ on/off
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
