using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FramePanel : MonoBehaviour
{
    [SerializeField] Text title;
    [SerializeField] Button exitBtn;
    [SerializeField] FrameItem framePb;
    [SerializeField] ScrollRect scollRect;
    [SerializeField] List<MissionSt> testMission;

    public static FramePanel instance;

    List<FrameItem> frames = new List<FrameItem>();
    bool start = true;
    float scroll_pos;

    public ScrollRect Content { get => scollRect; }
    public List<MissionSt> TestMission { get => testMission; }
    public bool Start1 { get => start; set => start = value; }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        //  exitBtn.onClick.AddListener(() => gameObject.SetActive(false));
        exitBtn.onClick.AddListener(OnBack);

        RenderData(); 
     
    }

    private void OnEnable()
    {
        if (start) return;
        RenderData();
    }

    void RenderData()
    {
        var lisFrameSt = DataManager.instance.ListSquareFrameSt;
        var listMissionSt = DataManager.instance.ListMissionSt;
        int length = lisFrameSt.Count;

        for (int i = 0; i < length; i++)
        {
            if(start)
            {
                var frame = Instantiate(framePb, scollRect.content.transform);
                frame.AssignItem(lisFrameSt[i].Picture, i, listMissionSt[i]);
                frames.Add(frame);
            }
          //  if (DataManager.instance.LocalStorage.Frames[i] || DataManager.instance.LocalStorage.PremiumPack)
            if (DataManager.instance.LocalStorage.Frames[i] )
            {
                frames[i].UnLockedItem();
                if (i == DataManager.instance.SettingStorage.IdxFrameChoosed)
                {
                    frames[i].OnChooseFrame();
                }
            }
            else
            {
                frames[i].LockedItem();
            }
        }
        start = false;
    }
    void OnBack()
    {
        GameManager.OnBack?.Invoke();
    }

}
