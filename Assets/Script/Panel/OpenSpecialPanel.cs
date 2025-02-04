// using SCN.Ads;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenSpecialPanel : MonoBehaviour
{
    [SerializeField] Button backBtn;
    [SerializeField] Button watchAdsBtn;

    Image m_picture;

    private void Start()
    {
        backBtn.onClick.AddListener(OnBack);
        watchAdsBtn.onClick.AddListener(OnWatchAds);
    }
    void OnBack()
    {
        GameManager.OnBack?.Invoke();
    }
    void OnWatchAds()
    {
        // if(AdsManager.Instance.HasRewardVideo)
        // {
        //     AdsManager.Instance.ShowRewardVideo(() =>
        //     {
        //         GUIManager.instance.RewardPanel.AssignPicture(m_picture);
        //         GUIManager.instance.OpenPanel(Const.REWARD_PANEL);

        //         // Update Local Storage
        //         DataManager.instance.LocalStorage.SpecialItems.Add(m_picture.sprite.name);
        //         DataManager.instance.SaveItem(Const.ITEM_STORAGE);

        //         // Hide Panel
        //         OnBack();

        //         FirebaseManager.Instance.GetSpecialPicture(m_picture.sprite.name);
        //     });
        // } else
        // {
        //     // Show Panel No Ads Available
        //     GUIManager.instance.OpenPanel(Const.ADS_INVALID_PANEL);
        // }
        GUIManager.instance.OpenPanel(Const.ADS_INVALID_PANEL);
    }
    public void AssignItem(Image picture)
    {
        m_picture = picture;
    }
}
