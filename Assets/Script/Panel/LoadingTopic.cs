using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Video;

public class LoadingTopic : MonoBehaviour
{
    [SerializeField] private LoadingPanel.LoadingType myTopic;
    [SerializeField] VideoPlayer videoPlayer;
    [SerializeField] private CanvasGroup canvasGroup;

    public double Duration => videoPlayer.length + 1;
    public LoadingPanel.LoadingType Type => myTopic;

    public Action OnCompleted;
    private Sequence playTween;

[NaughtyAttributes.Button]
    public void Play()
    {
     //   gameObject.SetActive(true);

        videoPlayer.Prepare();
        videoPlayer.Play();

        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    //    videoPlayer.
    }
[NaughtyAttributes.Button]
    public void Stop()
    {
     //   gameObject.SetActive(false);
        videoPlayer.Stop();
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }
    public void PlayOnTime(Action OnCompleted = null)
    {
        var duration = myTopic == LoadingPanel.LoadingType.Intro ? (float)Duration : (float) Duration + 1;
        this.OnCompleted = OnCompleted;
        

        playTween?.Kill();
        playTween = DOTween.Sequence()
            .AppendInterval(0.1f)
            .AppendCallback(() => Play())
            .AppendInterval((float)Duration)
            .AppendCallback(() =>
            {
                Stop();
                this.OnCompleted?.Invoke();
            });
    }

}
