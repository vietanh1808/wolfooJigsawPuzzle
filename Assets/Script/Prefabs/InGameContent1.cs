using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class InGameContent1 : MonoBehaviour
{
    public static InGameContent1 instance;

    [System.Serializable]
    public class Data
    {
        public Image pb;
        public Sprite picture;
        public int pictureId;
        public int topicId;
        public ModeType mode;
    }

    [SerializeField] GameObject cloneZone;
    [SerializeField] RectTransform puzzleScrollZonePb;
    [SerializeField] ScrollRect scrollView;
    [SerializeField] BackgroundUI backgroundUI;
    [SerializeField] Image pictureSample;
    [SerializeField] Image frame;
    [SerializeField] ParticleSystem starConfetti;
    [SerializeField] GameObject blackLockAction;
    [SerializeField] Button previewBtn;

    List<Image> m_listPicturePuzzle = new List<Image>();
    int idxPuzzle;
    int countItem;
    List<int> m_idxRandoms = new List<int>();
    Image m_picturePuzzlePb;
    Image m_targetPuzzle;
    RectTransform m_scrollZone;
    List<string> m_curItemTopic = new List<string>();
    bool m_update = true;

    private Data myData;
    private PreviewPopup previewPopup;

    private void Start()
    {
        SoundManager.Instance.PlayMusic(SFXType.Ingame);

        previewBtn.onClick.AddListener(OnClickPreview);

        GUIManager.instance.DisableHomeBtn(true);
        GUIManager.instance.DisableFrameBtn(true);

        myData = GameManager.Ins.ingameData;
        m_picturePuzzlePb = myData.pb;

        backgroundUI.title.text = m_picturePuzzlePb.name;
        backgroundUI.BackgroundImg = GUIManager.instance.BackgroundImg;
        backgroundUI.BackgroundImg.sprite = GUIManager.instance.ListBg[2];

        GetEvent();
        GetInfoPuzzle();
        previewPopup = FindAnyObjectByType<PreviewPopup>(FindObjectsInactive.Include);
        previewPopup.Assgin(pictureSample.sprite);

        HandPoint.instance.MoveHandIngame(true);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            HandPoint.instance.MoveHandIngame(false);
            return;
        }

        if (Input.GetMouseButtonUp(0) && m_update)
        {
            HandPoint.instance.MoveHandIngame(true);
        }
    }
    private void OnDisable()
    {
        m_update = false;
    }
    private void OnDestroy()
    {
        GameManager.OnPuzzlEndDrag -= GetEndDragPos;
        GameManager.OnPuzzleDrag -= GetDragPuzzle;
        GameManager.OnPuzzleBeginDrag -= GetBeginDragPuzzle;

        SoundManager.Instance.PlayMusic(SFXType.Main);
        GUIManager.instance.DisableHomeBtn(false);
        GUIManager.instance.DisableFrameBtn(false);

        m_update = false;
    }

    private void OnClickPreview()
    {
        GUIManager.instance.OpenPanel(Const.PREVIEW_POPUP);
    }

    void GetEvent()
    {
        GameManager.OnPuzzlEndDrag += GetEndDragPos;
        GameManager.OnPuzzleDrag += GetDragPuzzle;
        GameManager.OnPuzzleBeginDrag += GetBeginDragPuzzle;
    }
    void GetFrame()
    {
        int idx = DataManager.instance.SettingStorage.IdxFrameChoosed;
        switch (myData.mode)
        {
            case ModeType.SBS:
                frame.sprite = DataManager.instance.ListSquareFrameSt[idx].Picture;
                // Set picture Special to Center Position
                switch (frame.sprite.name)
                {
                    case "khung rung xanh":
                        m_targetPuzzle.transform.localPosition = new Vector2(30, 0);
                        break;
                }
                break;
            case ModeType.HS:
            case ModeType.SPECIAL:
                frame.sprite = DataManager.instance.ListRectangleFrameSt[idx].Picture;

                // Set picture Special to Center Position
                switch (frame.sprite.name)
                {
                    case "khung ngang rom":
                        GUIManager.instance.ScaleImage(frame, 1413, 960);
                        m_targetPuzzle.transform.localPosition = new Vector2(15, -15);
                        break;
                    case "khung ngang day leo":
                        GUIManager.instance.ScaleImage(frame, 1332, 909);
                        m_targetPuzzle.transform.localPosition = new Vector2(40, -10);
                        break;
                    case "khung ngang hoa":
                        GUIManager.instance.ScaleImage(frame, 1357, 981);
                        m_targetPuzzle.transform.localPosition = new Vector2(15, -0);
                        break;
                    case "khung ngang rung xanh":
                        GUIManager.instance.ScaleImage(frame, 1643, 923);
                        m_targetPuzzle.transform.localPosition = new Vector2(33, -5);
                        break;
                    case "khung ngang vu tru":
                        GUIManager.instance.ScaleImage(frame, 1603, 1159);
                        m_targetPuzzle.transform.localPosition = new Vector2(15, -25);
                        break;
                    case "khung go ngang":
                        GUIManager.instance.ScaleImage(frame, 1373, 993);
                        m_targetPuzzle.transform.localPosition = new Vector2(0, -10);
                        break;
                }
                break;
        }
    }
    void GetInfoPuzzle()
    {
        m_targetPuzzle = Instantiate(m_picturePuzzlePb, frame.transform);
        m_scrollZone = Instantiate(puzzleScrollZonePb, scrollView.transform.GetChild(0));
        scrollView.content = m_scrollZone;
        pictureSample.sprite = m_targetPuzzle.sprite;
        GUIManager.instance.ScaleImage(pictureSample, 170, 170);

        // Set picture to Center Position
        m_targetPuzzle.transform.localPosition = new Vector2(0, 0);

        GetFrame();

        int length = m_targetPuzzle.transform.childCount;
        switch (myData.mode)
        {
            case ModeType.HS:
                m_curItemTopic = DataManager.instance.LocalStorage.HsItems;
                GetRandomPuzzle();
                for (int i = 0; i < length; i++)
                {
                    var item = m_targetPuzzle.transform.GetChild(i).GetComponent<Image>();
                    item.gameObject.SetActive(false);

                    var itemScroll = Instantiate(item, m_scrollZone.transform);
                    if (m_idxRandoms.Contains(i)) // Compare Index To Change Mask Image Puzzle
                    {
                        itemScroll.gameObject.SetActive(true);
                        itemScroll.gameObject.AddComponent<ItemPuzzle>();
                        itemScroll.gameObject.GetComponent<ItemPuzzle>().AssignTarget(item, i, true, false);
                    }
                    else
                    {
                        itemScroll.gameObject.SetActive(false);
                    }
                    m_listPicturePuzzle.Add(itemScroll);
                    item.gameObject.SetActive(true);
                }
                countItem = m_listPicturePuzzle.Count / 2;
                break;

            case ModeType.SBS:
                m_curItemTopic = DataManager.instance.LocalStorage.SbsItems;
                for (int i = 0; i < length; i++)
                {
                    var item = m_targetPuzzle.transform.GetChild(i).GetComponent<Image>();
                    var itemScroll = Instantiate(item, m_scrollZone.transform);

                    itemScroll.gameObject.SetActive(true);
                    itemScroll.gameObject.AddComponent<ItemPuzzle>();
                    itemScroll.gameObject.GetComponent<ItemPuzzle>().AssignTarget(item, i, false, false);

                    m_listPicturePuzzle.Add(itemScroll);

                    if (m_picturePuzzlePb.name == "Moon" && i > 0)
                    {
                        GUIManager.instance.ScaleImage(itemScroll, 213, 198);
                    }
                    if (m_picturePuzzlePb.name == "Owl" && i == length - 1)
                    {
                        GUIManager.instance.ScaleImage(itemScroll, 213, 198);
                    }
                    if (m_picturePuzzlePb.name == "Chicken" && (i == 0 || i == 1))
                    {
                        GUIManager.instance.ScaleImage(itemScroll, 213, 198);
                    }
                }
                countItem = m_listPicturePuzzle.Count;
                break;

            case ModeType.SPECIAL:
                m_curItemTopic = DataManager.instance.LocalStorage.SpecialItems;
                var bgItem = m_targetPuzzle.transform.GetChild(0);
                bgItem.gameObject.SetActive(true);
                var firstPicture = Instantiate(bgItem.GetComponent<Image>());
                m_listPicturePuzzle.Add(firstPicture);

                for (int i = 1; i < length; i++)
                {
                    var item = m_targetPuzzle.transform.GetChild(i).GetComponent<Image>();
                    var itemScroll = Instantiate(item, m_scrollZone.transform);
                    itemScroll.gameObject.SetActive(true);
                    itemScroll.gameObject.AddComponent<ItemPuzzle>();
                    itemScroll.gameObject.GetComponent<ItemPuzzle>().AssignTarget(item, i, true, true);

                    m_listPicturePuzzle.Add(itemScroll);
                }
                countItem = m_listPicturePuzzle.Count - 1;
                break;
        }
    }

    void GetRandomPuzzle()
    {
        for (int i = 0; i < 3; i++)
        {
            int rd = Random.Range(0, 6);
            while (m_idxRandoms.Contains(rd))
            {
                rd = Random.Range(0, 6);
            }
            m_idxRandoms.Add(rd);
        }
    }

    void GetBeginDragPuzzle(int id_)
    {
        m_listPicturePuzzle[id_].transform.SetParent(cloneZone.transform);
        var mousePos = Input.mousePosition;
        Vector3 curPosition = Camera.main.ScreenToWorldPoint(mousePos);
        m_listPicturePuzzle[id_].transform.position = new Vector3(curPosition.x, curPosition.y, transform.position.z);
        idxPuzzle = m_listPicturePuzzle[id_].transform.GetSiblingIndex();
    }
    void GetDragPuzzle(int id_, PointerEventData eventData)
    {
        var mousePos = Input.mousePosition;
        Vector3 curPosition = Camera.main.ScreenToWorldPoint(mousePos);
        m_listPicturePuzzle[id_].transform.position = new Vector3(curPosition.x, curPosition.y, transform.position.z);

    }
    void GetEndDragPos(float distance_, int id_, bool hidden, Image targetImg)
    {
        // On Success Jigsaw
        if (distance_ <= GameManager.Ins.FitZone)
        {
            SoundManager.Instance.PlaySFX(SFXType.Snap);

            starConfetti.transform.localPosition = targetImg.transform.localPosition;
            starConfetti.transform.SetAsLastSibling();
            starConfetti.Play();
            m_listPicturePuzzle[id_].gameObject.SetActive(false);

            targetImg.sprite = m_listPicturePuzzle[id_].sprite;
            targetImg.color = Color.white;
            targetImg.DOFade(1f, 0);
            targetImg.gameObject.SetActive(true);

            countItem--;
            if (countItem < 1) // Out of Puzzle List && On Completed Game
            {
                OnCompleteJigsaw(hidden);

            }
            GameManager.Ins.DragIngame = true;
        }
        else // On Failed Jigsaw
        {
            if (!hidden)
            {
                m_listPicturePuzzle[id_].transform.DOKill();
                m_listPicturePuzzle[id_].gameObject.transform.SetParent(transform);
                m_listPicturePuzzle[id_].transform.DOLocalMove(scrollView.transform.localPosition, 0.25f)
                    .OnComplete(() =>
                    {
                        m_listPicturePuzzle[id_].gameObject.transform.SetParent(m_scrollZone.transform);
                        m_listPicturePuzzle[id_].gameObject.transform.SetSiblingIndex(idxPuzzle);
                        m_listPicturePuzzle[id_].gameObject.SetActive(true);
                        GameManager.Ins.DragIngame = true;
                    });
            }
            else
            {
                m_listPicturePuzzle[id_].gameObject.SetActive(true);
                GameManager.Ins.DragIngame = true;
            }
        }
    }
    void OnCompleteJigsaw(bool hidden)
    {
        //   FirebaseManager.Instance.CompletePicture(m_picturePuzzlePb.name);
        SoundManager.Instance.PlaySFX(SFXType.Win);
        //    DataManager.instance.
        UpdateData();

        backgroundUI.backBtn.interactable = false;
        backgroundUI.settingBtn.interactable = false;
        DOVirtual.DelayedCall(0.5f, () =>
        {
            m_targetPuzzle.transform.SetParent(GUIManager.instance.Canvas.transform);
            blackLockAction.SetActive(true);

            m_targetPuzzle.transform.DOLocalMove(Vector2.zero, 1f)
                .OnComplete(() =>
                {
                    OpenSuccessPanel();
                    m_targetPuzzle.transform.SetParent(transform);

                    // Update Storage to Add List Game User Played
                    GUIManager.instance.RewardPanel.RenderRewardPanel();

                }).OnStart(() =>
                {
                    if (hidden)
                    {
                        m_targetPuzzle.transform.DOScale(1.4f, .5f).SetEase(Ease.OutBack).OnComplete(() =>
                        {
                            m_targetPuzzle.transform.DOScale(0.8f, .5f);
                        });
                    }
                    else
                    {
                        m_targetPuzzle.transform.DOScale(1.4f, .5f).SetEase(Ease.OutBack).OnComplete(() =>
                        {
                            m_targetPuzzle.transform.DOScale(1.2f, .5f);
                        });
                    }
                });
        });
    }
    void UpdateData()
    {
        Debug.Log("Update Data");
        // Change status completed
        if (m_curItemTopic.Contains(myData.picture.name))
        {
            return;
        }
        m_curItemTopic.Add(myData.picture.name);
        DataManager.instance.SaveItem(Const.ITEM_STORAGE);
    }
    void OpenSuccessPanel()
    {
        //     Destroy(gameObject);
        GUIManager.instance.SuccesPanel.AssignItem(m_targetPuzzle);
        GUIManager.instance.SuccesPanel.EnableNextBtn(true);

        //     DOTween.KillAll();
        HandPoint.instance.Picture.gameObject.SetActive(false);
    }
}
