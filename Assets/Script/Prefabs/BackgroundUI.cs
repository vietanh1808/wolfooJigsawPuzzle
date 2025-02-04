using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BackgroundUI : MonoBehaviour
{
    public Text title;
    public Button backBtn;
    public Button settingBtn;
    public Button shopBtn;
    public Button frameBtn;
    public GameObject banner;
    [SerializeField] Image backgroundImg;

    public Image BackgroundImg { get => backgroundImg; set => backgroundImg = value; }
    void Start()
    {
        settingBtn.onClick.AddListener(OnSettingClick);
        
        shopBtn.gameObject.SetActive(false);
        if(shopBtn.gameObject.activeSelf)
        {
            shopBtn.onClick.AddListener(OnShopClick);
        }
        frameBtn.onClick.AddListener(OnFrameClick);
        backBtn.onClick.AddListener(OnButtonBack);
    }

    private void OnShopClick()
    {
        SoundManager.Instance.PlaySFX(SFXType.Touch2);
        GameManager.OnOpenShop?.Invoke();
    }

    private void OnFrameClick()
    {
        SoundManager.Instance.PlaySFX(SFXType.Touch2);
         GameManager.OnOpenFrame?.Invoke();
       // GUIManager.instance.OpenPanel(Const.FRAME_PANEL);
    }

    private void OnButtonBack()
    {
        SoundManager.Instance.PlaySFX(SFXType.Touch2);
        GameManager.OnBack?.Invoke();
    }

    void OnSettingClick()
    {
        SoundManager.Instance.PlaySFX(SFXType.Touch2);
        GUIManager.instance.OpenPanel(Const.SETTING_PANEL);
    }

}
