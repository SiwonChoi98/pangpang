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

    GameObject[] targetPool; //������ƮǮ Ȱ�뿡�� �����Լ��� ������ ���� �����Ƽ� ������ �ϳ� ������ش�.
    //tip : ù �ε� �ð� = ����ġ + ������Ʈ Ǯ ����
    void Awake()
    {
        //�ѹ��� ������ ������ ����Ͽ� �迭�� ���̸� �Ҵ�
        enemyA = new GameObject[20];
        enemyB = new GameObject[20];
        enemyC = new GameObject[20];
        enemyD = new GameObject[20];


        Generate();
    }

    void Generate()
    {  
        //���� �迭 ����
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
    //������Ʈ Ǯ Ȱ��
    public GameObject MakeObj(string type) //���ӿ�����Ʈ�� ��ȯ������ �����ؾ���
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
        return null; //if�ɸ��� return targetPool[i]; ���ְ� �Ȱɸ��� �׳� null�� return���ش�.
    }    
    
    void Update()
    {

    }

}