// using SCN.Ads;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FrameItem : MonoBehaviour
{
    [SerializeField] Button changeBtn;
    [SerializeField] Button watchAdsBtn;
    [SerializeField] Button missionBtn;
    [SerializeField] Button lockBtn;
    [SerializeField] Image frame;
    [SerializeField] List<Sprite> claimImg;  

    int id;
    bool unlockMission;
    MissionSt mission;

    private void Start()
    {
        changeBtn.onClick.AddListener(OnChooseFrame);
        watchAdsBtn.onClick.AddListener(OnWatchAdsClick);
        missionBtn.onClick.AddListener(OnMissionClick);
        lockBtn.onClick.AddListener(OnLockClick);
    }

    public void AssignItem(Sprite _frame, int _id, MissionSt _mission)
    {
        frame.sprite = _frame;
        frame.SetNativeSize();
        GUIManager.instance.ScaleImage(frame, 513, 513);
        id = _id;
        mission = _mission;
    }
    private void OnWatchAdsClick()
    {
        // if(AdsManager.Instance.HasRewardVideo)
        // {
        //     AdsManager.Instance.ShowRewardVideo(() =>
        //     {
        //         SoundManager.Instance.PlaySFX(SFXType.OpenNewFrame);
        //         if (!DataManager.instance.SettingStorage.MuteSound)
        //         {
        //             SoundManager.Instance.PlaySFX(SFXType.Main);
        //         }

        //         GUIManager.instance.RewardPanel.AssignPicture(frame);
        //         UnLockedItem();
        //         OnChooseFrame();

        //         UpdateStorage();
        //         FirebaseManager.Instance.WatchAdsFrame(frame.sprite.name);
        //     });
        // } else
        // {
        //     // Show Panel No Ads Available
        //     GUIManager.instance.OpenPanel(Const.ADS_INVALID_PANEL);
        // }
        GUIManager.instance.OpenPanel(Const.ADS_INVALID_PANEL);
    }

    private void OnMissionClick()
    {
        int remainPicture = GetRemainPicture(mission.NumberPicture);
        if(unlockMission)
        {
            GUIManager.instance.RewardPanel.AssignPicture(frame);

            UpdateStorage();
        }
        else
        {
            GUIManager.instance.MissionPanel.SetContent(remainPicture);
            GUIManager.instance.OpenPanel(Const.MISSION_PANEL);
        }
    }

    void UpdateStorage()
    {
        /// Update Frame Local Storage
        DataManager.instance.LocalStorage.Frames[id] = true;
        DataManager.instance.SaveItem(Const.ITEM_STORAGE);
    }

    int GetRemainPicture(int number)
    {
        int totalSbs = DataManager.instance.LocalStorage.SbsItems.Count;
        int totalHs = DataManager.instance.LocalStorage.HsItems.Count;
        int totalSpecial = DataManager.instance.LocalStorage.SpecialItems.Count;

        int total = totalHs + totalSbs + totalSpecial;
        if (total < number)
        {
            unlockMission = false;
            return number - total;
        } else
        {
            unlockMission = true;
            return 0;
        }
    }

    private void OnLockClick()
    {
        Debug.Log("Lock Click!");
    }

    public void OnChooseFrame()
    {
        int idPrevFrame = DataManager.instance.SettingStorage.IdxFrameChoosed;
        if(id == idPrevFrame && !FramePanel.instance.Start1)
        {
            return;
        }
        var prevFrame = FramePanel.instance.Content.content.transform.GetChild(idPrevFrame).GetComponent<FrameItem>();
        prevFrame.changeBtn.image.sprite = claimImg[0];
        changeBtn.image.sprite = claimImg[1];

        DataManager.instance.ChangeFrame(id);

    //    FirebaseManager.Instance.ChooseFrame(frame.sprite.name);
    }
    public void UnLockedItem()
    {
        Debug.Log("Frame: " + frame.sprite.name);
        changeBtn.image.sprite = claimImg[0];
        changeBtn.gameObject.SetActive(true);
        frame.color = Color.white;

        lockBtn.gameObject.SetActive(false);
        watchAdsBtn.gameObject.SetActive(false);
        missionBtn.gameObject.SetActive(false);
    }

    public void LockedItem()
    {
        lockBtn.gameObject.SetActive(true);
        watchAdsBtn.gameObject.SetActive(false);
        changeBtn.gameObject.SetActive(false);
        frame.color = new Color32(0, 0, 0, 120);

        if (FramePanel.instance.TestMission.Contains(mission)) {
            missionBtn.gameObject.SetActive(true);
        } 
        else
        {
            missionBtn.gameObject.SetActive(false);
        }
    }
}
