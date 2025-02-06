using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Video;

public class LoadingPanel : MonoBehaviour
{
    public enum LoadingType
    {
        Intro,
        Ingame
    }
    [SerializeField] private LoadingTopic[] topics;
    [SerializeField] private VideoClip[] clips;

    private static LoadingPanel instance;
    public static Action<LoadingType, Action> Play, Stop;

    private void Start()
    {
        LoadingPanel.Play += OnPlay;
        LoadingPanel.Stop += OnStop;

        Init();
    }
    private void OnDestroy()
    {
        LoadingPanel.Play -= OnPlay;
        LoadingPanel.Stop -= OnStop;
    }


    private void OnPlay(LoadingType type, Action OnCompleted)
    {
        var id = (int)type;
        foreach (var topic in topics)
        {
            if (topic.Type == type)
            {
                break;
            }
        }
        topics[0].Assign(clips[id]);
        topics[0].PlayOnTime(OnCompleted);
    }

    private void OnStop(LoadingType type, Action OnCompleted)
    {
        foreach (var topic in topics)
        {
            topic.Stop();
        }
        OnCompleted?.Invoke();
    }

    private void Init()
    {
        // if(instance == null) {
        //     var obj = Resources.Load<LoadingPanel>("Panel - Loading");
        //     instance = Instantiate(obj);
        // }

        foreach (var topic in topics)
        {
            topic.Stop();
        }
    }
}
