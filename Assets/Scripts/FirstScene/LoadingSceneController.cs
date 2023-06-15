using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LoadingSceneController : MonoBehaviour
{
    static string nextScene;
    public Image progressBar;
    public GameObject[] loadingImage;
    public GameObject[] tipTxt;
    public static void LoadScene(string sceneName)
    {
        nextScene = sceneName;
        SceneManager.LoadScene("LoadingScene"); // LoadScene은 동기방식이라 씬을 불러오기전에는 다른작업을 할수가없다.
    }
    void Start()
    {
        int ranTip = Random.Range(0, 5);
        tipTxt[ranTip].SetActive(true); //랜덤으로 팁 알려주기

        StartCoroutine(LoadSceneProcess());
    }
   
    IEnumerator LoadSceneProcess()
    {
        AsyncOperation op =  SceneManager.LoadSceneAsync(nextScene); //LoadSceneAsync은 비동기방식이라 씬을 불러오면서 다른작업이 가능하다.
        op.allowSceneActivation = false; //씬을 비동기로 불러올때 씬의 로딩이 끝나면 자동으로 불러온 씬으로 이동할것인지를 설정하는것 //이값을 false로 설정하면 씬을 90퍼센트까지만 설정하고 다음씬으로 넘어가는걸 기다리고 true로 변경하면 남은 10퍼를 채워서 넘어가는 방식

        float timer = 0f; 
        while (!op.isDone) //씬로딩이 끝나지 않은 상태일때
        {
            yield return null; //반복문이 한번반복할때마다 유니티엔진에 제어권을 넘기는 것 제어권을 넘겨주지않으면 반복문이 끝나지않으면 화면이 갱신되지 않아서 진행바가 차오르는게 안된다.
            IEnumerator WaitandStart()
            {
                yield return new WaitForSeconds(2f);
                op.allowSceneActivation = true;
            }
            if (op.progress < 0.1f)
            {
                progressBar.fillAmount = op.progress;
               
            }
            else
            {
                timer += Time.unscaledDeltaTime;
                progressBar.fillAmount = Mathf.Lerp(0.1f, 1f, timer);
                if (timer > 0.1f)
                {
                    loadingImage[0].SetActive(true);
                }
                if (timer > 0.3f)
                {
                    loadingImage[1].SetActive(true);
                }
                if (timer > 0.5f)
                {
                    loadingImage[2].SetActive(true);
                }
                if (timer > 0.7f)
                {
                    loadingImage[3].SetActive(true);
                }
                if (timer > 0.9f)
                {
                    loadingImage[4].SetActive(true);
                }
                if (progressBar.fillAmount >= 1f)
                {
                    StartCoroutine(WaitandStart()); //잠시 기다렸다가 실행해줘라 //tip 같은거 보여주려고 살짝기다려준다.

                    yield break;
                }
            }
        }
       
    }
}
