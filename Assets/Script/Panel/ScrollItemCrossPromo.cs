using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollItemCrossPromo : MonoBehaviour
{
    [SerializeField] private string id;
    [SerializeField] private Image iconImg;
    [SerializeField] private Button btn;

    private const string chplayLink = "https://play.google.com/store/apps/details?id=";
    private const string appstoreLink = "https://apps.apple.com/app/";

    public void Assign(Sprite icon, string id){
        this.id = id;
        iconImg.sprite = icon;
        btn.onClick.AddListener(OnClickMe);
    }

    private void OnClickMe()
    {
        #if UNITY_IOS
        OpenAppStoreForiOS();
        #elif UNITY_ANDROID
        OpenAppStoreForAndroid();
        #endif
    }

    void OpenAppStoreForiOS()
    {
        string url = appstoreLink + id;
        Application.OpenURL(url);
    }

    void OpenAppStoreForAndroid()
    {
        string url = chplayLink + id;
        Application.OpenURL(url);
    }
}
