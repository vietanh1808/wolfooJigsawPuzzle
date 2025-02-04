using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SelectPicturePanel : MonoBehaviour
{
    [SerializeField] private ScrollItemPicture itemTopicPb;
    [SerializeField] private CustomLayout sbsContent;
    [SerializeField] private CustomLayout hsContent;
    [SerializeField] private CustomLayout specialContent;
    [SerializeField] private TopicSt[] sbsTopics;
    [SerializeField] private TopicSt[] hsTopics;
    [SerializeField] private TopicSt[] specialTopics;

    private List<ScrollItemPicture> allPictures = new List<ScrollItemPicture>();

    private void Start()
    {
        GameManager.GetNextLevel += OrderNextLevel;
        InitData();
    }
    void OnEnable(){
        if(allPictures.Count == 0) return;

        foreach (var item in allPictures)
        {
            item.AssignData();
        }
    }
    void OnDestroy()
    {
        GameManager.GetNextLevel -= OrderNextLevel;
    }

    public void OrderNextLevel()
    {
        var gameData = GameManager.Ins.ingameData;
        var nextPictureId = gameData.pictureId + 1;
        var nextTopicId = gameData.topicId;

        switch (gameData.mode)
        {
            case ModeType.HS:
                if (nextPictureId >= hsTopics[gameData.topicId].puzzleItem.Count)
                {
                    nextPictureId = 0;
                    nextTopicId += 1;
                }
                if (nextTopicId >= hsTopics.Length)
                {
                    nextTopicId = 0;
                    gameData.mode = ModeType.SPECIAL;
                    gameData.pb = specialTopics[nextTopicId].puzzleItem[nextPictureId];
                }
                else
                {
                    gameData.pb = hsTopics[nextTopicId].puzzleItem[nextPictureId];
                    gameData.picture = hsTopics[nextTopicId].HiddenItem[nextPictureId];
                }
                break;
            case ModeType.SBS:
                if (nextPictureId >= sbsTopics[gameData.topicId].puzzleItem.Count)
                {
                    nextPictureId = 0;
                    nextTopicId += 1;
                }
                if (nextTopicId >= sbsTopics.Length)
                {
                    nextTopicId = 0;
                    gameData.mode = ModeType.SPECIAL;
                    gameData.pb = hsTopics[nextTopicId].puzzleItem[nextPictureId];
                }
                else
                {
                    gameData.pb = sbsTopics[nextTopicId].puzzleItem[nextPictureId];
                }
                break;
            case ModeType.SPECIAL:
                if (nextPictureId >= specialTopics[gameData.topicId].puzzleItem.Count)
                {
                    nextPictureId = 0;
                    nextTopicId += 1;
                }
                if (nextTopicId >= specialTopics.Length)
                {
                    nextTopicId = 0;
                    gameData.mode = ModeType.SPECIAL;
                    gameData.pb = sbsTopics[nextTopicId].puzzleItem[nextPictureId];
                }
                else
                {
                    gameData.pb = specialTopics[nextTopicId].puzzleItem[nextPictureId];
                    //   gameData.picture = hsTopics[nextTopicId].[nextPictureId];
                }
                break;
        }
        gameData.topicId = nextTopicId;
        gameData.pictureId = nextPictureId;
        gameData.picture = gameData.pb.sprite;
    }

    private void InitData()
    {
        var mainContent = GetComponentInChildren<ContentSizeFitter>();
        mainContent.enabled = false;

        for (int i = 0; i < sbsTopics.Length; i++)
        {
            for (int j = 0; j < sbsTopics[i].puzzleItem.Count; j++)
            {
                var newItem = Instantiate(itemTopicPb, sbsContent.transform);
                newItem.AssignItem(new InGameContent1.Data()
                {
                    pictureId = j,
                    topicId = i,
                    pb = sbsTopics[i].puzzleItem[j],
                    picture = sbsTopics[i].puzzleItem[j].sprite,
                    mode = ModeType.SBS
                });
                allPictures.Add(newItem);
            }
        }
        sbsContent.Resize();

        for (int i = 0; i < hsTopics.Length; i++)
        {
            for (int j = 0; j < hsTopics[i].puzzleItem.Count; j++)
            {
                var newItem = Instantiate(itemTopicPb, hsContent.transform);
                newItem.AssignItem(new InGameContent1.Data()
                {
                    pictureId = j,
                    topicId = i,
                    pb = hsTopics[i].puzzleItem[j],
                    picture = hsTopics[i].HiddenItem[j],
                    mode = ModeType.HS
                });
                allPictures.Add(newItem);
            }
        }
        hsContent.Resize();

        for (int i = 0; i < specialTopics.Length; i++)
        {
            for (int j = 0; j < specialTopics[i].puzzleItem.Count; j++)
            {
                var newItem = Instantiate(itemTopicPb, specialContent.transform);
                newItem.AssignItem(new InGameContent1.Data()
                {
                    pictureId = j,
                    topicId = i,
                    pb = specialTopics[i].puzzleItem[j],
                    picture = specialTopics[i].puzzleItem[j].sprite,
                    mode = ModeType.SPECIAL
                });
                allPictures.Add(newItem);
            }
        }
        specialContent.Resize();


        mainContent.enabled = true;
    }
}
