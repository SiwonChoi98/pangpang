using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public GameObject enemyAPrefab;
    public GameObject enemyBPrefab;
    public GameObject enemyCPrefab;
    public GameObject enemyDPrefab;
    GameObject[] enemyA;
    GameObject[] enemyB;
    GameObject[] enemyC;
    GameObject[] enemyD;

    GameObject[] targetPool; //오브젝트풀 활용에서 같은함수를 여러개 쓰기 귀찮아서 변수를 하나 만들어준다.
    //tip : 첫 로딩 시간 = 장면배치 + 오브젝트 풀 생성
    void Awake()
    {
        //한번에 등장할 개수를 고려하여 배열의 길이를 할당
        enemyA = new GameObject[20];
        enemyB = new GameObject[20];
        enemyC = new GameObject[20];
        enemyD = new GameObject[20];


        Generate();
    }

    void Generate()
    {  
        //몬스터 배열 저장
        for(int i=0; i<enemyA.Length; i++)
        {
            enemyA[i] = Instantiate(enemyAPrefab);
            enemyA[i].SetActive(false);
        }
        for (int i = 0; i < enemyB.Length; i++)
        {
            enemyB[i] = Instantiate(enemyBPrefab);
            enemyB[i].SetActive(false);
        }
        for (int i = 0; i < enemyC.Length; i++)
        {
            enemyC[i] = Instantiate(enemyCPrefab);
            enemyC[i].SetActive(false);
        }
        for (int i = 0; i < enemyD.Length; i++)
        {
            enemyD[i] = Instantiate(enemyDPrefab);
            enemyD[i].SetActive(false);
        }

    }
    //오브젝트 풀 활용
    public GameObject MakeObj(string type) //게임오브젝트를 반환값으로 지정해야함
    {
        
        switch (type)
        {
            case "enemyA":
                targetPool = enemyA;
                break;
            case "enemyB":
                targetPool = enemyB;
                break;
            case "enemyC":
                targetPool = enemyC;
                break;
            case "enemyD":
                targetPool = enemyD;
                break;
        }

        for (int i = 0; i < targetPool.Length; i++)
        {
            if (!targetPool[i].activeSelf)
            {
                targetPool[i].SetActive(true);
                return targetPool[i];
            }
        }
        return null; //if걸리면 return targetPool[i]; 해주고 안걸리면 그냥 null로 return해준다.
    }    
    
    void Update()
    {

    }

}