using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HomePanel : MonoBehaviour
{
    [SerializeField] Button playBtn;
    public BackgroundUI backgroundUI;


    private void Start()
    {
        playBtn.onClick.AddListener(OnPlaygame);

        ScalePlayBtn();
        
    }
    void ScalePlayBtn()
    {
        playBtn.transform.DOScale(1.2f, 0.8f)
       .OnComplete(() =>
       {
           playBtn.transform.DOScale(0.8f, 0.8f)
           .OnComplete(() =>
           {
                   ScalePlayBtn();
           });
       });
    }
    private void OnEnable()
    {
        if(GUIManager.instance == null)
        {
            return;
        }
        backgroundUI.BackgroundImg.sprite = GUIManager.instance.ListBg[3];
    }
    private void OnPlaygame()
    {
        //if (AdsManager.Instance.HasInters)
        //{
        //    AdsManager.Instance.ShowInterstitial(() => GameManager.OnPlaygame?.Invoke());
        //}
        //else
        //{
        //    GameManager.OnPlaygame?.Invoke();
        //}
        //if (AdsManager.Instance.HasRewardVideo)
        //{
        //    AdsManager.Instance.ShowRewardVideo(() =>
        //    {
        //        // gift
        //    });
        //}
        SoundManager.Instance.PlaySFX(SFXType.Touch);
        GameManager.OnPlaygame?.Invoke();
    }
}
