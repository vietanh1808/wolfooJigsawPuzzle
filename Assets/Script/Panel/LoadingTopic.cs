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

    public double Duration => videoPlayer.length;
    public LoadingPanel.LoadingType Type => myTopic;

    public Action OnCompleted;
    private Sequence playTween;

    public void Play()
    {
        videoPlayer.Play();
        gameObject.SetActive(true);

    }
    public void Stop()
    {
        gameObject.SetActive(false);
    }
    public void PlayOnTime(Action OnCompleted = null)
    {
        var duration = myTopic == LoadingPanel.LoadingType.Intro ? (float)Duration : (float) Duration + 1;
        this.OnCompleted = OnCompleted;
        
        videoPlayer.Prepare();

        playTween?.Kill();
        playTween = DOTween.Sequence()
            .AppendInterval(0.1f)
            .AppendCallback(() => Play())
            .AppendInterval(duration)
            .AppendCallback(() =>
            {
                Stop();
                this.OnCompleted?.Invoke();
            });
    }

}
