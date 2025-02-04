using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ItemStorage
{
    [SerializeField] List<string> sbsItems = new List<string>();
    [SerializeField] List<string> hsItems = new List<string>();
    [SerializeField] List<string> specialItems = new List<string>();
    [SerializeField] List<bool> frames = new List<bool>();
    [SerializeField] bool removeAds;
    [SerializeField] bool normalPack;
    [SerializeField] bool premiumPack;

    public List<string> SbsItems { get => sbsItems; set => sbsItems = value; }
    public List<string> HsItems { get => hsItems; set => hsItems = value; }
    public List<string> SpecialItems { get => specialItems; set => specialItems = value; }
    public List<bool> Frames { get => frames; set => frames = value; }
    public bool RemoveAds { get => removeAds; set => removeAds = value; }
    public bool NormalPack { get => normalPack; set => normalPack = value; }
    public bool PremiumPack { get => premiumPack; set => premiumPack = value; }
}

[Serializable]
public class SettingStorage
{
    [SerializeField] int idxFrameChoosed = -1;
    [SerializeField] bool muteSound = false;
    [SerializeField] bool privacy;

    public int IdxFrameChoosed { get => idxFrameChoosed; set => idxFrameChoosed = value; }
    public bool MuteSound { get => muteSound; set => muteSound = value; }
    public bool Privacy { get => privacy; set => privacy = value; }
}
