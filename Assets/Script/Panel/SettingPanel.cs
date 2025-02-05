// using SCN.Ads;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingPanel : MonoBehaviour
{
    [SerializeField] Button exitBtn;
    [SerializeField] Sprite[] soundImg = new Sprite[2];
    [SerializeField] Button soundBtn;
    [SerializeField] Button homeBtn;
    [SerializeField] Button FrameBtn;
    [SerializeField] Button shopBtn;
    [SerializeField] Button privacyBtn;
    [SerializeField] Button termBtn;

    bool mute;
    private void OnEnable()
    {
        //AdsManager.Instance.HideBanner();
    }
    private void OnDisable()
    {
       // AdsManager.Instance.ShowBanner();
    }

    private void Start()
    {
        soundBtn.onClick.AddListener(OnSoundClick);
        homeBtn.onClick.AddListener(OnHomeClick);
        FrameBtn.onClick.AddListener(OnFrameClick);
        exitBtn.onClick.AddListener(OnExit);

        shopBtn.gameObject.SetActive(false);
        if(shopBtn.gameObject.activeSelf)
        {
            shopBtn.onClick.AddListener(OnShopClick);
        }
        GetSound();
    }
    public void DisableHome(bool state)
    {
        if(homeBtn != null)
            homeBtn.gameObject.SetActive(!state);
    }
    public void DisableFrame(bool state)
    {
        if(FrameBtn != null)
            FrameBtn.gameObject.SetActive(!state);
    }
    private void OnExit()
    {
        gameObject.SetActive(false);
    }

    private void OnShopClick()
    {
        GameManager.OnOpenShop?.Invoke();
    }

    private void OnFrameClick()
    {
        // GUIManager.instance.OpenPanel(Const.FRAME_PANEL);
        gameObject.SetActive(false);
        GameManager.OnOpenFrame?.Invoke();
    }

    private void OnHomeClick()
    {
        // if (AdsManager.Instance.HasInters)
        // {
        //     AdsManager.Instance.ShowInterstitial((a) =>
        //     {
        //         GameManager.OnReturnHome?.Invoke();
        //     });
        // }
        // else
        // {
        //     GameManager.OnReturnHome?.Invoke();
        // }
            GameManager.OnReturnHome?.Invoke();
    }

    private void OnSoundClick()
    {
        mute = DataManager.instance.SettingStorage.MuteSound;
        DataManager.instance.SettingStorage.MuteSound = !mute;
        DataManager.instance.SaveItem(Const.SETTING_STORAGE);

        GetSound();
    }
    void GetSound()
    {
        mute = DataManager.instance.SettingStorage.MuteSound;

        if (mute)
        {
            soundBtn.image.sprite = soundImg[1]; // Disable Sound
        }
        else
        {
            soundBtn.image.sprite = soundImg[0]; // Enable Sound
        }

        SoundManager.Instance.gameObject.SetActive(!mute);
    }
}
