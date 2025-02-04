using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TopicMenuContent : MonoBehaviour
{
    [SerializeField] BackgroundUI backgroundUI;
    [SerializeField] TopicBtn topicPb;
    [SerializeField] GameObject content;
    [SerializeField] InModeContent inModePb;
    [SerializeField] int maxTopic = 5;

    List<TopicSt> listTopic = new List<TopicSt>();
    List<TopicBtn> m_listButtonTopic = new List<TopicBtn>();

    public static TopicMenuContent instance;

    private void Start()
    {
        InitTopic();
        RenderUI();
        RenderTopic();
        GameManager.OnTopicClick += GetOnTopicClick;
        GameManager.OpenLockPanel += GetLockPanel;
    }
    private void OnEnable()
    {
        if(m_listButtonTopic.Count == 0)
        {
            return;
        }
        RenderUI();
        RenderTopic();

        GUIManager.instance.BackgroundImg.sprite = GUIManager.instance.ListBg[0]; // Normal
        GameManager.OnTopicClick += GetOnTopicClick;
    }
    private void OnDisable()
    {
        GameManager.OnTopicClick -= GetOnTopicClick;
        GameManager.OpenLockPanel -= GetLockPanel;
    }
    void GetLockPanel()
    {
        GUIManager.instance.LockPanel.gameObject.SetActive(true);
    }

    void RenderUI()
    {
        switch (GameManager.Ins.Mode)
        {
            case Const.MODE_SBS:
                backgroundUI.title.text = "Shape by Shape";
                listTopic = DataManager.instance.ListTopicSbsSt;
                break;
            case Const.MODE_HS:
                backgroundUI.title.text = "Hidden Shape";
                listTopic = DataManager.instance.ListTopicHsSt;
                break;
            case Const.MODE_SPECIAL:
                backgroundUI.title.text = "Special Mode";
                listTopic = DataManager.instance.ListTopicSpecialSt;
                break;
        }
    }

    public void RenderTopic()
    {
        int i;
        for ( i = 0; i < maxTopic; i++)
        {
            if(i >= listTopic.Count)
            {
                m_listButtonTopic[i].gameObject.SetActive(false);
                continue;
            }

            m_listButtonTopic[i].button.image.sprite = listTopic[i].picture;
            m_listButtonTopic[i].gameObject.SetActive(true);
        }
    }

    void InitTopic()
    {
        for (int i = 0; i < maxTopic; i++)
        {
            var item = Instantiate(topicPb, content.transform);
            item.gameObject.SetActive(false);
            item.id = i;
            m_listButtonTopic.Add(item);
        }
    }

    void GetOnTopicClick(int id)
    {
        SoundManager.Instance.PlaySFX(SFXType.Touch);
        DataManager.instance.CurTopicSt = listTopic[id];
        if (InModeContent.instance == null)
        {
            var item = Instantiate(inModePb, GUIManager.instance.Canvas.transform);
            InModeContent.instance = item;
        }

        GUIManager.instance.ChangeModeScreen(gameObject, InModeContent.instance.gameObject);

    //    FirebaseManager.Instance.ChooseTopic(DataManager.instance.CurTopicSt.title);
    }
}