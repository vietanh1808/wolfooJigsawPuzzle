using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{

    [SerializeField] List<TopicSt> listTopicHsSt;
    [SerializeField] List<TopicSt> listTopicSbsSt;
    [SerializeField] List<TopicSt> listTopicSpecialSt;
    [SerializeField] List<FrameSt> listSquareFrameSt;
    [SerializeField] List<FrameSt> listRectangleFrameSt;
    [SerializeField] List<MissionSt> listMissionSt;

    ObjectData objectData;
    public static DataManager instance;

    TopicSt m_curTopicSt;
    [ SerializeField] ItemStorage m_localStorage = new ItemStorage();
    SettingStorage m_settingStorage = new SettingStorage();

    public List<TopicSt> ListTopicHsSt { get => listTopicHsSt; }
    public List<TopicSt> ListTopicSbsSt { get => listTopicSbsSt; }
    public List<TopicSt> ListTopicSpecialSt { get => listTopicSpecialSt; }
    public TopicSt CurTopicSt { get => m_curTopicSt; set => m_curTopicSt = value; }
    public List<FrameSt> ListSquareFrameSt { get => listSquareFrameSt;}
    public List<FrameSt> ListRectangleFrameSt { get => listRectangleFrameSt; }
    public ItemStorage LocalStorage { get => m_localStorage; set => m_localStorage = value; }
    public SettingStorage SettingStorage { get => m_settingStorage; set => m_settingStorage = value; }
    public List<MissionSt> ListMissionSt { get => listMissionSt; }

    void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
         GetItem(Const.ITEM_STORAGE);
        GetItem(Const.SETTING_STORAGE);

        objectData = Resources.Load<ObjectData>("ObjectData");
        // Create new Array to Save Local Storage
        int length = ListSquareFrameSt.Count;
        if (LocalStorage.Frames.Count == 0)
        {
            for (int i = 0; i < length; i++)
            {
                LocalStorage.Frames.Add(false);
            }
            // Set item Default is Open
            LocalStorage.Frames[0] = true;
        }

        // First Time instantiate
        if (SettingStorage.IdxFrameChoosed == -1)
        {
            SettingStorage.IdxFrameChoosed = 0;
        }


        // SetMuteSound();
    }
    void OnDestroy()
    {
        SaveItem(Const.ITEM_STORAGE);
        SaveItem(Const.SETTING_STORAGE);
    }

    void GetItem(string key)
    {
        var dataJson = PlayerPrefs.GetString(key);
        Debug.Log("Get: " + dataJson.ToString());
        if (dataJson == null || dataJson == "")
        {
            SaveItem(key);
            GetItem(key);
            return;
        }
        switch (key)
        {
            case Const.ITEM_STORAGE:
                LocalStorage = JsonUtility.FromJson<ItemStorage>(dataJson);
                break;
            case Const.SETTING_STORAGE:
                SettingStorage = JsonUtility.FromJson<SettingStorage>(dataJson);
                break;
        }
    }
    public void SaveItem(string key)
    {
        string dataJson;
        switch (key)
        {
            case Const.ITEM_STORAGE:
                dataJson = JsonUtility.ToJson(LocalStorage);
                break;
            case Const.SETTING_STORAGE:
                dataJson = JsonUtility.ToJson(SettingStorage);
                break;
            default:
                dataJson = "";
                break;
        }
        Debug.Log("Save: " + dataJson.ToString());
        PlayerPrefs.SetString(key, dataJson);
        PlayerPrefs.Save();
    }

    public void ChangeFrame(int id)
    {
        SettingStorage.IdxFrameChoosed = id;
        SaveItem(Const.SETTING_STORAGE);
    }

    void SetMuteSound()
    {
        SoundManager.Instance.gameObject.SetActive(!SettingStorage.MuteSound);
    }
}
