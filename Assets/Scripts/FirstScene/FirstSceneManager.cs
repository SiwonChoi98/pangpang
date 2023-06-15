using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FirstSceneManager : MonoBehaviour
{
    
    public GameObject bestScoreImage;
    public GameObject settingImage;
    
    public int score = 0;
    public int bestScore = 0;
    public Text bestScoreTxt;

    public AudioSource backGroundSound;
    public AudioSource uiSound;

    
    void Awake()
    {
        backGroundSound.Play();    
        score = PlayerPrefs.GetInt("BestScore"); //���ӳ��� �� �ѱ� ����
        PlayerPrefs.SetInt("BestScore2", score); //������ �ٽ� ���Ӿ��� Ű�� �Ѱ��ش�.
    }
    // Update is called once per frame
    void Update()
    {
       

    }
    void LateUpdate()
    {

        bestScoreTxt.text = "" + score; //�ְ����� ���
    }
    public void BestScoreOpen() //�ְ����� Ű�� ����
    {
        if(settingImage.activeSelf)
        {
            settingImage.SetActive(false);
        }
       
        bestScoreImage.SetActive(true);
        uiSound.Play();
    }
    public void BestScoreExit()
    {
        bestScoreImage.SetActive(false);
        uiSound.Play();
    }

    public void SettingOpen() //���� Ű�� ����
    {
        if (bestScoreImage.activeSelf)
        {
            bestScoreImage.SetActive(false);
        }
       
        settingImage.SetActive(true);
        uiSound.Play();
    }
    public void SettingExit()
    {
        settingImage.SetActive(false);
        uiSound.Play();
    }

 
}
