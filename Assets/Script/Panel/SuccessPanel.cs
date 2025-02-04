using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
// using SCN.Ads;

public class SuccessPanel : MonoBehaviour
{
    [SerializeField] GameObject mainPage;
    [SerializeField] Image background;
    [SerializeField] Image picture;
    [SerializeField] Button backBtn;
    [SerializeField] Button nextBtn;
    [SerializeField] ParticleSystem confetti;
    [SerializeField] ParticleSystem sparkerRainbow;

    private void Start()
    {
        backBtn.onClick.AddListener(OnBack);
        nextBtn.onClick.AddListener(OnNextLevel);
    }

    private void OnEnable()
    {
       // SoundManager.Instance.PlaySFX(SFXType.Success);
        confetti.Play();
        sparkerRainbow.Play();
        //AdsManager.Instance.HideBanner();
    }

    private void OnDestroy()
    {
        backBtn.onClick.RemoveAllListeners();
        nextBtn.onClick.RemoveAllListeners();
     //   AdsManager.Instance.ShowBanner();
    }

    void RenderPage()
    {
    }

    private void OnNextLevel()
    {
        gameObject.SetActive(false);
        // if (AdsManager.Instance.HasInters)
        // {
        //     AdsManager.Instance.ShowInterstitial((a) =>
        //     {
        //         FirebaseManager.Instance.ShowInter("NextLevel");
        //         InModeContent.instance.gameObject.SetActive(true);
        //         InModeContent.instance.GetNextLevel();
        //         gameObject.SetActive(false);
        //     });
        // }
        // else
        // {
        //     InModeContent.instance.gameObject.SetActive(true);
        //     InModeContent.instance.GetNextLevel();
        //     gameObject.SetActive(false);
        // }
        GameManager.GetNextLevel?.Invoke();
        GameManager.OnPlayIngame?.Invoke();
    }
    void OnBack()
    {
        SoundManager.Instance.PlaySFX(SFXType.Touch);
        gameObject.SetActive(false);
            GUIManager.instance.BacktoSelectPicturePanel();

        // if (AdsManager.Instance.HasInters)
        // {
        //     AdsManager.Instance.ShowInterstitial((a) =>
        //     {
        //         if (InModeContent.instance != null
        //             && GUIManager.instance.Current.name == InModeContent.instance.gameObject.name)
        //         {
        //             GUIManager.instance.ChangeModeScreen(TopicMenuContent.instance.gameObject, InModeContent.instance.gameObject);
        //             InModeContent.instance.gameObject.SetActive(true);
        //         }
        //     });
        // }
        // else
        // {
        //     if (InModeContent.instance != null
        //         && GUIManager.instance.Current.name == InModeContent.instance.gameObject.name)
        //     {
        //         GUIManager.instance.ChangeModeScreen(TopicMenuContent.instance.gameObject, InModeContent.instance.gameObject);
        //         InModeContent.instance.gameObject.SetActive(true);
        //     }
        // }
        
    }

    public void AssignItem(Image item)
    {
        picture.sprite = item.sprite;
        picture.SetNativeSize();
        if (picture.sprite.name == "khung day leo" || picture.sprite.name == "khung hoa")
        {
            GUIManager.instance.ScaleImage(picture, 800, 777);
        }
        else
        {
            GUIManager.instance.ScaleImage(picture, 922, 700);
        }
        gameObject.transform.SetAsLastSibling();
        gameObject.SetActive(true);
    }
    public void EnableNextBtn(bool state)
    {
        nextBtn.gameObject.SetActive(state);
    }
}
