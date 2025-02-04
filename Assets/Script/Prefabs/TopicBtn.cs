using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemClick : MonoBehaviour
{
    public int id;
    public Button button;
}

public class TopicBtn: ItemClick
{
    private void Start()
    {
        button.onClick.AddListener(OnClick);
    }
    void OnClick()
    {
        GameManager.OnTopicClick?.Invoke(id);
    }
}