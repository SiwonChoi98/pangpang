using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
public class TimeLineController : MonoBehaviour
{
    public PlayableDirector playerHealth1;
    public PlayableDirector playerHealth2;
    public PlayableDirector playerBarrier1;
    public PlayableDirector playerBarrier2;
    public PlayableDirector playerBarrier3;

    //---체력 타임라인(애니메이션)-----
    public void playerHealth1Anim() 
    {
        playerHealth1.gameObject.SetActive(true);
        playerHealth1.Play();
    }
    public void playerHealth2Anim()
    {
        playerHealth2.gameObject.SetActive(true);
        playerHealth2.Play();
    }
    //---방어막 타임라인(애니메이션)----
    public void playerBarrier1Anim()
    {
        playerBarrier1.gameObject.SetActive(true);
        playerBarrier1.Play();
    }
    
    public void playerBarrier2Anim()
    {
        playerBarrier2.gameObject.SetActive(true);
        playerBarrier2.Play();
    }
    public void playerBarrier3Anim()
    {
        playerBarrier3.gameObject.SetActive(true);
        playerBarrier3.Play();
    }
    

}
