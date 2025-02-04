using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Mission", menuName = "Sciptable Object/Misson")]
public class MissionSt : ScriptableObject
{
    [SerializeField] int numberPicture;
    [SerializeField] Sprite pictureFrame;

    public int NumberPicture { get => numberPicture; }
    public Sprite PictureFrame { get => pictureFrame; }
}

