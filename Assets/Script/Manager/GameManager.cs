using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;
using Unity.VisualScripting;
using UnityEngine.Android;
// using GoogleMobileAds.Mediation.IronSource.Android;

public enum ModeType
{
    SBS, /// Shape by Shape
    HS, /// Hiden Shape
    SPECIAL /// Special Shape
}

public class GameManager : MonoBehaviour
{
    public static GameManager Ins;

    [SerializeField] float fitZone = 2f;
    public InGameContent1 inGamePb;
    public InGameContent1.Data ingameData = new InGameContent1.Data();
    public int totalPicture = 10;

    bool m_dragIngame = true;

    public List<TopicSt> sbsModeSts;
    public List<TopicSt> hsModeSts;

    public static Action<float,int, bool, Image> OnPuzzlEndDrag;
    public static Action<int> OnPuzzleBeginDrag;
    public static Action<int, PointerEventData> OnPuzzleDrag;
    public static Action OnBack;
    public static Action<int> OnTopicClick;
    public static Action<int> OnItemClick;
    public static Action OpenLockPanel;
    public static Action OnOpenShop;
    public static Action OnReturnHome;
    public static Action OnOpenFrame;
    public static Action OnPlaygame;
    public static Action OnPlayIngame;
    public static Action GetNextLevel;

    int mode = Const.INSTANCE_MODE;
    float ratio;

    public int Mode { get => mode; set => mode = value; }
    public float FitZone { get => fitZone; }
    public bool DragIngame { get => m_dragIngame; set => m_dragIngame = value; }
    private void Awake()
    {
        if(Ins == null)
        {
            Ins = this;
        }
        Application.targetFrameRate = 120;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        // IronSourceClient.Instance.SetMetaData("is_deviceid_optout", "true");
        // IronSourceClient.Instance.SetMetaData("is_child_directed", "true");
        if (!Permission.HasUserAuthorizedPermission(Permission.ExternalStorageRead))
        {
            // Request permission if not already granted
            Permission.RequestUserPermission(Permission.ExternalStorageRead);
        }

        if (!Permission.HasUserAuthorizedPermission(Permission.ExternalStorageWrite))
        {
            Permission.RequestUserPermission(Permission.ExternalStorageWrite);
        }

    }
    
    public void ScaleImage(Image item, float width, float height)
    {
        if (item.rectTransform.rect.height > item.rectTransform.rect.width)
        {
            ratio = item.rectTransform.rect.height / item.rectTransform.rect.width; // Scale with Max Height
            width = height / ratio;
            item.rectTransform.sizeDelta = new Vector2(width, height);
        }
        else
        {
            ratio = item.rectTransform.rect.width / item.rectTransform.rect.height;  // Scale with Max Width
            height = width / ratio;
            item.rectTransform.sizeDelta = new Vector2(width, height);
        }
    }
}
