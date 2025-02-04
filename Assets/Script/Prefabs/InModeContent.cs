using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InModeContent : MonoBehaviour
{
    public BackgroundUI backgroundUI;
    [SerializeField] GridLayoutGroup content;
    [SerializeField] ItemTopicBtn itemTopic;
    [SerializeField] InGameContent1 ingameContent;
    [SerializeField] int max_item = 15;
    [SerializeField] List<Sprite> specialFrames; // Special Khung ngang

    public static InModeContent  instance ;

    TopicSt topic;
    FrameSt frame;
    List<ItemTopicBtn> m_itemTopics = new List<ItemTopicBtn>();
    List<string> m_existItems = new List<string>();
    int idCurItem;

    public int IdCurItem { get => idCurItem; }

    private void Start()
    {
        topic = DataManager.instance.CurTopicSt;
        backgroundUI.title.text = topic.title;
        backgroundUI.BackgroundImg = GUIManager.instance.BackgroundImg;
        backgroundUI.BackgroundImg.sprite = GUIManager.instance.ListBg[1];

        InitItem();
        RenderData(); // Frame, Resize Content ScrollView, Item is Played
        RenderItem();
        GameManager.OnItemClick += GetItemClick;
    }
    private void OnEnable()
    {
        topic = DataManager.instance.CurTopicSt;
        if (m_itemTopics.Count <= 0)
        {
            return;
        }

        RenderData(); // Frame, Resize Content ScrollView, Item is Played
        RenderItem();

        backgroundUI.title.text = topic.title;
        backgroundUI.BackgroundImg.sprite = GUIManager.instance.ListBg[1];
        GameManager.OnItemClick += GetItemClick;
    }

    private void OnDisable()
    {
        GameManager.OnItemClick -= GetItemClick;
    }

    void InitItem()
    {
        for (int i = 0; i < max_item; i++)
        {
            var new_item = Instantiate(itemTopic, content.transform);
            new_item.gameObject.SetActive(false);
            new_item.id = i;
            m_itemTopics.Add(new_item);
        }
    }
    void RenderData() 
    {
        int idx = DataManager.instance.SettingStorage.IdxFrameChoosed;

        content.spacing = new Vector2(50, 50);
        switch (GameManager.Ins.Mode)
        {
            case Const.MODE_SBS:
                content.cellSize = new Vector2(600, 550);
                frame = DataManager.instance.ListSquareFrameSt[idx];
                m_existItems = DataManager.instance.LocalStorage.SbsItems;
                break;
            case Const.MODE_HS:
                content.cellSize = new Vector2(500, 350);
                frame = DataManager.instance.ListRectangleFrameSt[idx];
                m_existItems = DataManager.instance.LocalStorage.HsItems;
                break;
            case Const.MODE_SPECIAL:
                content.cellSize = new Vector2(500, 350);
                frame = DataManager.instance.ListRectangleFrameSt[idx];
                m_existItems = DataManager.instance.LocalStorage.SpecialItems;

                if(frame.Picture.name == "khung ngang rung xanh")
                {
                    content.cellSize = new Vector2(550, 300);
                }
                break;
        }
    }
    public void RenderItem()
    {
        int lengthItemTopic;

        if (specialFrames.Contains(frame.Picture) && GameManager.Ins.Mode == Const.MODE_SBS)
        {
            content.cellSize = new Vector2(700, 550);
            content.spacing = new Vector2(0, 80);
        }

        for (int i = 0; i < max_item; i++)
        {
            lengthItemTopic = topic.puzzleItem.Count;
            if (i >= lengthItemTopic) // Pooling Item
            {
                m_itemTopics[i].gameObject.SetActive(false);
                continue;
            }
         
            // Render Item && Scale to Fit Frame Image 
            m_itemTopics[i].Picture.sprite = topic.puzzleItem[i].sprite;
            m_itemTopics[i].Picture.SetNativeSize();
            GUIManager.instance.ScaleImage(m_itemTopics[i].Picture, 350, 360);
            m_itemTopics[i].Picture.transform.localPosition = new Vector3(10, 0, 0);

            m_itemTopics[i].Frame.sprite = frame.Picture; // Render Frame
            GUIManager.instance.ScaleImage(m_itemTopics[i].Frame, 600, 350);
            m_itemTopics[i].SetDefault();

            // Check item is Played
            switch (GameManager.Ins.Mode)
            {
                case Const.MODE_SBS:
                    // Check in Local Storage and Pack is used
                    if (!m_existItems.Contains(topic.puzzleItem[i].sprite.name))
                        /*
                    if (!m_existItems.Contains(topic.puzzleItem[i].sprite.name) 
                        && (!DataManager.instance.LocalStorage.NormalPack || !DataManager.instance.LocalStorage.PremiumPack))
                        */
                    {
                        m_itemTopics[i].Picture.color = new Color32(0, 0, 0, 150); // Black and Blur
                    }

                    m_itemTopics[i].Picture.transform.localPosition = new Vector2(-10, 0);
                    switch (frame.Picture.name)
                    {
                        case "khung hoa":
                        case "khung ngang hoa":
                            m_itemTopics[i].Picture.transform.localPosition = new Vector2(5, 0);
                            break;
                        case "khung vu tru":
                        case "khung ngang vu tru":
                            m_itemTopics[i].Picture.transform.localPosition = new Vector2(0, -10);
                            break;
                        case "khung rung ram":
                        case "khung ngang rung ram":
                            m_itemTopics[i].Picture.transform.localPosition = new Vector2(10, 0);
                            break;
                        case "khung day leo":
                        case "khung ngang day leo":
                            m_itemTopics[i].Picture.transform.localPosition = new Vector2(20, 0);
                            break;
                    }
                    break;
                case Const.MODE_HS:
                    // Check in Local Storage and Pack is used
                    if (!m_existItems.Contains(topic.puzzleItem[i].sprite.name))
/*                    if (!m_existItems.Contains(topic.puzzleItem[i].sprite.name)
                        && (!DataManager.instance.LocalStorage.NormalPack || !DataManager.instance.LocalStorage.PremiumPack))*/
                    {
                        m_itemTopics[i].Picture.sprite = topic.HiddenItem[i];
                    } else
                    {
                        m_itemTopics[i].Picture.sprite = topic.puzzleItem[i].sprite;
                    }
                    break;
                case Const.MODE_SPECIAL:
                    // Check in Local Storage and Pack is used
                    if (m_existItems.Contains(topic.puzzleItem[i].sprite.name))
/*                    if (m_existItems.Contains(topic.puzzleItem[i].sprite.name)
                        && (DataManager.instance.LocalStorage.NormalPack || DataManager.instance.LocalStorage.PremiumPack))*/
                    {
                        m_itemTopics[i].LockBtn.gameObject.SetActive(false);
                        break;
                    }

                    // Check in Item is Played
                    int count = 0;
                    foreach (var item in topic.ListPuzzleItemPb[i].items)
                    {
                        // Count Item exist in sbsMode and Pack is used
                        if(DataManager.instance.LocalStorage.SbsItems.Contains(item.sprite.name))
                        {
                            count++;
                        }
                    }
                    if(count == topic.ListPuzzleItemPb[i].items.Count)
                    {
                        m_itemTopics[i].LockBtn.gameObject.SetActive(false);
                        m_existItems.Add(topic.ListPuzzleItemPb[i].name);
                        break;
                    }

                    m_itemTopics[i].LockBtn.gameObject.SetActive(true);
                    break;
            }

            m_itemTopics[i].gameObject.SetActive(true);
        }
    }

    void GetItemClick(int id)
    {
        SoundManager.Instance.PlaySFX(SFXType.Touch);
        idCurItem = id;
        if (InGameContent1.instance == null)
        {
            InGameContent1.instance = Instantiate(ingameContent, GUIManager.instance.Canvas.transform);
        }
    //    InGameContent1.instance.PicturePuzzlePb = topic.puzzleItem[id];
        InGameContent1.instance.gameObject.SetActive(true);
        gameObject.SetActive(false);

     //   FirebaseManager.Instance.ChooseTopic(InGameContent1.instance.PicturePuzzlePb.name);
    }

    public void GetNextLevel()
    {
        int idNextItem = idCurItem + 1;
        if (idNextItem >= topic.puzzleItem.Count)
        {
            GUIManager.instance.ChangeModeScreen(TopicMenuContent.instance.gameObject, gameObject);
            return;
        }
        
        // If Special Mode and Next Picture is Locked
        if(GameManager.Ins.Mode == Const.MODE_SPECIAL 
            && !DataManager.instance.LocalStorage.SpecialItems.Contains(topic.puzzleItem[idNextItem].sprite.name))
        {
            GUIManager.instance.ChangeModeScreen(TopicMenuContent.instance.gameObject, gameObject);
            return;
        }

        GetItemClick(idNextItem);
    }
}
