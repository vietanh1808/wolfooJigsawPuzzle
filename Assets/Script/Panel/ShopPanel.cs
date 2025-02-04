// using SCN.Ads;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopPanel : MonoBehaviour
{
    [SerializeField] Button removeAds;
    [SerializeField] Button normalPack;
    [SerializeField] Button premium;
    [SerializeField] Button exitBtn;

    BuyingType curBuying;

    public enum BuyingType
    {
        RemoveAds,
        NormalPack,
        Premium,
    }
    private void OnEnable()
    {
     //   AdsManager.Instance.HideBanner();
    }
    private void OnDisable()
    {
     //   AdsManager.Instance.ShowBanner();
    }

    private void Start()
    {
        exitBtn.onClick.AddListener(OnBack);
        normalPack.onClick.AddListener(OnBuyNormalPack);
        premium.onClick.AddListener(OnBuyPremium);
        removeAds.onClick.AddListener(OnBuyRemoveAds);
    }
    void OnBack()
    {
        GameManager.OnBack?.Invoke();
    }
    void OnBuyNormalPack()
    {
        curBuying = BuyingType.NormalPack;
        GUIManager.instance.OpenPanel(Const.QUESTION_PANEL);
    }
    void OnBuyRemoveAds()
    {
        curBuying = BuyingType.RemoveAds;
        GUIManager.instance.OpenPanel(Const.QUESTION_PANEL);
    }
    void OnBuyPremium()
    {
        curBuying = BuyingType.Premium;
        GUIManager.instance.OpenPanel(Const.QUESTION_PANEL);
    }

    public void OnBuying()
    {
        switch (curBuying)
        {
            case BuyingType.RemoveAds:
                DataManager.instance.LocalStorage.RemoveAds = true;
                DataManager.instance.SaveItem(Const.ITEM_STORAGE);
                break;
            case BuyingType.NormalPack:
                DataManager.instance.LocalStorage.NormalPack = true;
                DataManager.instance.SaveItem(Const.ITEM_STORAGE);
                break;
            case BuyingType.Premium:
                DataManager.instance.LocalStorage.PremiumPack = true;
                DataManager.instance.SaveItem(Const.ITEM_STORAGE);
                break;
        }
    }
}
