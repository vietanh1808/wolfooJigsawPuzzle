using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModeContent: MonoBehaviour
{
    public BackgroundUI backgroundUI;
    [SerializeField] Button leftMode;
    [SerializeField] Button rightMode;

    [SerializeField] TopicMenuContent menuContent;

    public static ModeContent instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    private void Start()
    {
        switch (GameManager.Ins.Mode)
        {
            case Const.INSTANCE_MODE:
                leftMode.onClick.AddListener(OnBasicClick);
                rightMode.onClick.AddListener(OnSpecialClick);
                break;
            case Const.IN_BASIC_MODE:
                leftMode.onClick.AddListener(() => ChangeToTopicMenu(Const.MODE_HS));
                rightMode.onClick.AddListener(() => ChangeToTopicMenu(Const.MODE_SBS));
                break;
        }
    }

    void ChangeToTopicMenu(int mode)
    {
        SoundManager.Instance.PlaySFX(SFXType.Touch);
        if (TopicMenuContent.instance == null)
        {
            TopicMenuContent.instance = Instantiate(menuContent, GUIManager.instance.Canvas.transform);
        }
        GameManager.Ins.Mode = mode;
        GUIManager.instance.ChangeModeScreen(GUIManager.instance.inBasicMode.gameObject, TopicMenuContent.instance.gameObject);

        switch (mode)
        {
            case Const.MODE_HS:
             //   FirebaseManager.Instance.ChooseMode("Hidden_Shape");
                break;
            case Const.MODE_SBS:
             //   FirebaseManager.Instance.ChooseMode("ShapeByShape");
                break;
        }
    }

    void OnBasicClick()
    {
        SoundManager.Instance.PlaySFX(SFXType.Touch);
        GameManager.Ins.Mode = Const.IN_BASIC_MODE;
        GUIManager.instance.ChangeModeScreen(GUIManager.instance.mode.gameObject, GUIManager.instance.inBasicMode.gameObject);

    //    FirebaseManager.Instance.ChooseLevel("BasicMode");
    }
    void OnSpecialClick()
    {
        SoundManager.Instance.PlaySFX(SFXType.Touch);
        if (TopicMenuContent.instance == null)
        {
            TopicMenuContent.instance = Instantiate(menuContent, GUIManager.instance.Canvas.transform);
        }
        GameManager.Ins.Mode = Const.MODE_SPECIAL;
        GUIManager.instance.ChangeModeScreen(GUIManager.instance.mode.gameObject, TopicMenuContent.instance.gameObject);

      //  FirebaseManager.Instance.ChooseLevel("SpecialMode");
    }
}
