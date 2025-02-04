using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemTopicBtn : ItemClick
{
    [SerializeField] Image frame;
    [SerializeField] Image picture;
    [SerializeField] Button lockBtn;

    private ModeType modeType;

    public Button LockBtn { get => lockBtn; set => lockBtn = value; }
    public Image Frame { get => frame; set => frame = value; }
    public Image Picture { get => picture; set => picture = value; }
    List<string> m_conditions = new List<string>();

    private void Start()
    {
        button.onClick.AddListener(OnClick);
        lockBtn.onClick.AddListener(OpenLockPanel);
    }

    public void AssignItem(List<Image> conditions)
    {
        foreach (var condition in conditions)
        {
            m_conditions.Add(condition.name);
        }
    }
    public void AssignItem(int id, Sprite sprite, ModeType modeType)
    {
        this.id = id;
        picture.sprite = sprite;
        this.modeType = modeType;
    }
    void OnClick()
    {
        GameManager.OnItemClick?.Invoke(id);
    }
    void OpenLockPanel()
    {
        GUIManager.instance.OpenSpecialPanel.AssignItem(picture);
        GUIManager.instance.ChangeModeScreen(InModeContent.instance.gameObject, GUIManager.instance.OpenSpecialPanel.gameObject);
    }

    public void SetDefault()
    {
        // Set To Default
        Picture.color = new Color32(255, 255, 255, 255); // White
        lockBtn.gameObject.SetActive(false);
    }

}
