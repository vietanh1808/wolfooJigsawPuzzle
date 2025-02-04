using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Topic", menuName = "Topic")]
public class TopicSt : ScriptableObject
{
    public string title;
    public bool status;
    public Sprite picture;
    public List<Image> puzzleItem;
    [SerializeField] List<Sprite> m_hiddenItem;
    [SerializeField] List<SpecicalTopicSt> listPuzzleItemPb;

    public List<SpecicalTopicSt> ListPuzzleItemPb { get => listPuzzleItemPb; }
    public List<Sprite> HiddenItem { get => m_hiddenItem; }

}
