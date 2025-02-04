using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
// using SCN.Ads;

public class RewardPanel : MonoBehaviour
{
    [SerializeField] Image background;
    [SerializeField] Image picture;
    [SerializeField] Button claimBtn;
    [SerializeField] ParticleSystem confetti;
    [SerializeField] ParticleSystem sparkerRainbow;

    bool unlockMission;
    int totalSbs, totalHs, totalSpecial;

    private void Start()
    {
        claimBtn.onClick.AddListener(OnClaim);

       // RenderRewardPanel();
        // Set to Popup Screen
    }

    private void OnEnable()
    {
        if(gameObject == null)
        {
            return;
        }
        confetti.Play();
        sparkerRainbow.Play();
        transform.SetAsLastSibling();

        //AdsManager.Instance.HideBanner();
    }

    private void OnDisable()
    {
     //   AdsManager.Instance.ShowBanner();
    }

    void OnClaim()
    {
        gameObject.SetActive(false);

    }
    public void AssignPicture(Image _picture)
    {
        picture.sprite = _picture.sprite;
        if (picture.sprite.name == "khung day leo")
        {
            GUIManager.instance.ScaleImage(picture, 800, 777);
        }
        else
        {
            GUIManager.instance.ScaleImage(picture, 922, 700);
        }
        gameObject.SetActive(true);
    }
    public void RenderRewardPanel()
    {
        Debug.Log("Render Reward Panel");
        var listFrame = DataManager.instance.LocalStorage.Frames;
        var listMission = DataManager.instance.ListMissionSt;

        for (int i = 0; i < listMission.Count; i++)
        {
            OnCheckTotalPicture(listMission[i].NumberPicture);
            if (unlockMission && !listFrame[i])
            {
                SoundManager.Instance.PlaySFX(SFXType.OpenNewFrame); ;

                picture.sprite = listMission[i].PictureFrame;
                GUIManager.instance.OpenPanel(Const.REWARD_PANEL);

             //   var curFrame = FramePanel.instance.Content.content.transform.GetChild(i).GetComponent<FrameItem>();
            //    curFrame.UnLockedItem();
                UpdateStorage(i);

             //   FirebaseManager.Instance.MissionFrame(picture.sprite.name);
                return;
            }
        }
    }
    void OnCheckTotalPicture(int number)
    {
        totalSbs = DataManager.instance.LocalStorage.SbsItems.Count;
        totalHs = DataManager.instance.LocalStorage.HsItems.Count;
        totalSpecial = DataManager.instance.LocalStorage.SpecialItems.Count;

        int total = totalHs + totalSbs + totalSpecial;
        if (total < number)
        {
            unlockMission = false;
        }
        else
        {
            unlockMission = true;
        }
    }
    void UpdateStorage(int id)
    {
        Debug.Log("UpdateStorage");
        /// Update Frame Local Storage
        DataManager.instance.LocalStorage.Frames[id] = true;
        DataManager.instance.SaveItem(Const.ITEM_STORAGE);
    }
}
