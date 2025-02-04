using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ScrollItemPicture : MonoBehaviour
{
    private ModeType modeType;
    private Button btn;
    private InGameContent1.Data ingameData;

    private void OnClickMe()
    {
        Debug.Log("On Click Me");
        SoundManager.Instance.PlaySFX(SFXType.Touch);

        GameManager.Ins.ingameData = ingameData;
        GameManager.OnPlayIngame?.Invoke();
    }

    public void AssignItem(InGameContent1.Data data)
    {
        ingameData = data;
        AssignData();
    }
    public void AssignData()
    {
        if (btn == null)
        {
            btn = GetComponentInChildren<Button>();
            btn.onClick.AddListener(OnClickMe);
        }
        btn.image.sprite = ingameData.picture;

        var totalSbs = DataManager.instance.LocalStorage.SbsItems;
        var totalHs = DataManager.instance.LocalStorage.HsItems;
        var totalSpecial = DataManager.instance.LocalStorage.SpecialItems;

        switch (ingameData.mode)
        {
            case ModeType.HS:
                if(totalHs.Contains(ingameData.picture.name)) {
                    Unlock();
                } 
                else {
                    Lock();
                };
                break;
            case ModeType.SBS:
                if(totalSbs.Contains(ingameData.picture.name)) {
                    Unlock();
                } 
                else {
                    Lock();
                };
                break;
            case ModeType.SPECIAL:
                if(totalSpecial.Contains(ingameData.picture.name)) {
                    Unlock();
                } 
                else {
                    Lock();
                };
                break;
        }
    }
    private void Unlock()
    {
        btn.image.DOColor(Color.white, 0);
        btn.image.DOFade(01, 0);
    }
    private void Lock()
    {
        btn.image.DOColor(Color.black, 0);
        btn.image.DOFade(0.7f, 0);
    }
}
