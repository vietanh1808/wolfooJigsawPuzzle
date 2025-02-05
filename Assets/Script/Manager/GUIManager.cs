// using SCN.Ads;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour
{
    public static GUIManager instance;

    public SelectPicturePanel selectPicturePanel;
    [SerializeField] GameObject canvas;
    [SerializeField] Button playBtn;
    [SerializeField] BackgroundUI backgroundUI;
    [SerializeField] ModeContent modePb;
    [SerializeField] InModeContent inModePb;
    [SerializeField] InGameContent1 inGamePb;
    [SerializeField] TopicMenuContent itemMenuPb;

    [SerializeField] HomePanel homePanel;
    [SerializeField] SettingPanel settingPanel;
    [SerializeField] ShopPanel shopPanel;
    [SerializeField] FramePanel framePanel;
    [SerializeField] SuccessPanel succesPanel;
    [SerializeField] LockPanel lockPanel;
    [SerializeField] ModeContent modeContent;
    [SerializeField] RewardPanel rewardPanel;
    [SerializeField] MissionPanel missionPanel;
    // [SerializeField] AdsInvalidPanel adsInvalidPanel;
    [SerializeField] OpenSpecialPanel openSpecialPanel;
    [SerializeField] QuestionPanel questionPanel;
    [SerializeField] PreviewPopup previewPopup;

    public ModeContent inBasicMode, mode;
    [SerializeField] Sprite questionImg;
    [SerializeField] Image backgroundImg;
    [SerializeField] List<Sprite> listBg;
    [SerializeField] Image HandPointer;

    public GameObject Canvas { get => canvas;}
    public Sprite QuestionImg { get => questionImg; }
    public List<Sprite> ListBg { get => listBg; }
    public Image BackgroundImg { get => backgroundImg; }
    public LockPanel LockPanel { get => lockPanel; }
    public SuccessPanel SuccesPanel { get => succesPanel; }
    public FramePanel FramePanel { get => framePanel; }
    public HomePanel HomePanel { get => homePanel; }
    public GameObject Current { get => current; }
    public RewardPanel RewardPanel { get => rewardPanel; }
    public MissionPanel MissionPanel { get => missionPanel; }
    // public AdsInvalidPanel AdsInvalidPanel { get => adsInvalidPanel;  }
    public OpenSpecialPanel OpenSpecialPanel { get => openSpecialPanel; }
    public QuestionPanel QuestionPanel { get => questionPanel; }
    public ShopPanel ShopPanel { get => shopPanel; }
    public SettingPanel SettingPanel { get => settingPanel; }

    private GameObject previous;
    private GameObject current;
    private List<GameObject> listPanel = new List<GameObject>();
    float ratio;
    GameObject currentPanel; // To Change Panel
    private InGameContent1 ingame;

    private void Awake()
    {
        if(instance == null)
        {
           instance = this;
        }
    }
    
    public void DisableHomeBtn(bool state)
    {
        settingPanel.DisableHome(state);
    }
    public void DisableFrameBtn(bool state)
    {
        settingPanel.DisableFrame(state);
    }
    private void Start()
    {
        //AdsManager.Instance.ShowBanner();
        ChangeModeScreen(homePanel.gameObject, homePanel.gameObject);

        GameManager.OnPlaygame += OnPlaygame;
        GameManager.OnPlayIngame += PlayIngame;
        GameManager.OnBack += BackPanel;
        GameManager.OnReturnHome += GetReturnHome;
        GameManager.OnOpenFrame += GetFramePanel;
        GameManager.OnOpenShop += GetShopPanel;
    }

    private void OnDestroy()
    {
        GameManager.OnPlaygame -= OnPlaygame;
        GameManager.OnPlayIngame -= PlayIngame;
        GameManager.OnBack -= BackPanel;
        GameManager.OnReturnHome -= GetReturnHome;
        GameManager.OnOpenFrame -= GetFramePanel;
        GameManager.OnOpenShop -= GetShopPanel;
    }

    void OnPlaygame()
    {
        ChangeModeScreen(homePanel.gameObject, selectPicturePanel.gameObject);
// modeContent.backgroundUI.BackgroundImg.sprite = listBg[0];
    }

    public void BacktoSelectPicturePanel(){
        LoadingPanel.Play(LoadingPanel.LoadingType.Ingame, null);

        if(ingame != null) Destroy(ingame.gameObject);
        selectPicturePanel.gameObject.SetActive(true);
        current = selectPicturePanel.gameObject;
    }
    public void PlayIngame(){
        LoadingPanel.Play(LoadingPanel.LoadingType.Ingame, null);
        
        if(ingame != null) Destroy(ingame.gameObject);
        ingame = Instantiate(inGamePb, canvas.transform);
        selectPicturePanel.gameObject.SetActive(false);
        current = ingame.gameObject;
    }
    public void BackToHome(){
        LoadingPanel.Play(LoadingPanel.LoadingType.Ingame, null);

        homePanel.gameObject.SetActive(true);
        selectPicturePanel.gameObject.SetActive(false);
        current = homePanel.gameObject;
    }


    public void ChangeModeScreen(GameObject previous_, GameObject current_)
    {
        if (!listPanel.Contains(previous_))
        {
            listPanel.Add(previous_);
        }
        previous = previous_;
        current = current_;

        if (previous != null)
        {
            previous.SetActive(false);
        }
        current.transform.SetAsLastSibling();
        current.SetActive(true);
    }
    public void BackPanel()
    {
        if (listPanel.Count <= 0)
        {
            return;
        } else if(listPanel.Count == 1)
        {
            GetReturnHome();
            return;
        }
        var tempCurrent = current;
        current = previous;

        listPanel.Remove(current);

        previous = listPanel[listPanel.Count - 1];

        tempCurrent.SetActive(false);
        current.SetActive(true);
    }
    void GetFramePanel()
    {
        framePanel.transform.SetAsLastSibling();
        framePanel.gameObject.SetActive(true);
        ChangeModeScreen(current, framePanel.gameObject);
    }
    void GetShopPanel()
    {
        ChangeModeScreen(current, shopPanel.gameObject);
    }

    public void OpenPanel(int panel)
    {
        switch (panel)
        {
            case Const.SUCCESS_PANEL:
                succesPanel.gameObject.SetActive(true);
                succesPanel.transform.SetAsLastSibling();
                break;
            case Const.FRAME_PANEL:
                framePanel.gameObject.SetActive(true);
                framePanel.transform.SetAsLastSibling();
                break;
            case Const.REWARD_PANEL:
                rewardPanel.gameObject.SetActive(true);
                rewardPanel.transform.SetAsLastSibling();
                break;
            case Const.ADS_INVALID_PANEL:
                //adsInvalidPanel.gameObject.SetActive(true);
                //adsInvalidPanel.transform.SetAsLastSibling();
                break;
            case Const.MISSION_PANEL:
                missionPanel.gameObject.SetActive(true);
                missionPanel.transform.SetAsLastSibling();
                break;
            case Const.SETTING_PANEL:
                settingPanel.gameObject.SetActive(true);
                settingPanel.transform.SetAsLastSibling();
                break;
            case Const.OPEN_SPECIAL_PANEL:
                openSpecialPanel.gameObject.SetActive(true);
                openSpecialPanel.transform.SetAsLastSibling();
                break;
            case Const.QUESTION_PANEL:
                questionPanel.gameObject.SetActive(true);
                questionPanel.transform.SetAsLastSibling();
                break;
            case Const.SHOP_PANEL:
                shopPanel.gameObject.SetActive(true);
                shopPanel.transform.SetAsLastSibling();
                break;
            case Const.PREVIEW_POPUP:
                previewPopup.gameObject.SetActive(true);
                previewPopup.transform.SetAsLastSibling();
                break;
        }
    }

    private void GetReturnHome()
    {
        LoadingPanel.Play(LoadingPanel.LoadingType.Ingame, null);
        
        settingPanel.gameObject.SetActive(false);
        current.SetActive(false);
        homePanel.gameObject.SetActive(true);
        listPanel.Clear();
        backgroundImg.sprite = listBg[3];
    }
    public void ScaleImage(Image item, float width, float height)
    {
        item.SetNativeSize();
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
