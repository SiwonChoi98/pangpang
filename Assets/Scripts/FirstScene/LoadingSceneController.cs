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
        SceneManager.LoadScene("LoadingScene"); // LoadScene�� �������̶� ���� �ҷ����������� �ٸ��۾��� �Ҽ�������.
    }
    void Start()
    {
        int ranTip = Random.Range(0, 5);
        tipTxt[ranTip].SetActive(true); //�������� �� �˷��ֱ�

        StartCoroutine(LoadSceneProcess());
    }
   
    IEnumerator LoadSceneProcess()
    {
        AsyncOperation op =  SceneManager.LoadSceneAsync(nextScene); //LoadSceneAsync�� �񵿱����̶� ���� �ҷ����鼭 �ٸ��۾��� �����ϴ�.
        op.allowSceneActivation = false; //���� �񵿱�� �ҷ��ö� ���� �ε��� ������ �ڵ����� �ҷ��� ������ �̵��Ұ������� �����ϴ°� //�̰��� false�� �����ϸ� ���� 90�ۼ�Ʈ������ �����ϰ� ���������� �Ѿ�°� ��ٸ��� true�� �����ϸ� ���� 10�۸� ä���� �Ѿ�� ���

        float timer = 0f; 
        while (!op.isDone) //���ε��� ������ ���� �����϶�
        {
            yield return null; //�ݺ����� �ѹ��ݺ��Ҷ����� ����Ƽ������ ������� �ѱ�� �� ������� �Ѱ����������� �ݺ����� ������������ ȭ���� ���ŵ��� �ʾƼ� ����ٰ� �������°� �ȵȴ�.
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
                    StartCoroutine(WaitandStart()); //��� ��ٷȴٰ� ��������� //tip ������ �����ַ��� ��¦��ٷ��ش�.

                    yield break;
                }
            }
        }
       
    }
}
