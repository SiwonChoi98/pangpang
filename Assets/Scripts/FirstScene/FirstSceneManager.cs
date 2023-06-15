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
        score = PlayerPrefs.GetInt("BestScore"); //게임끝난 후 넘긴 점수
        PlayerPrefs.SetInt("BestScore2", score); //점수를 다시 게임씬에 키를 넘겨준다.
    }
    // Update is called once per frame
    void Update()
    {
       

    }
    void LateUpdate()
    {

        bestScoreTxt.text = "" + score; //최고점수 출력
    }
    public void BestScoreOpen() //최고점수 키고 끄고
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

    public void SettingOpen() //설정 키고 끄고
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
