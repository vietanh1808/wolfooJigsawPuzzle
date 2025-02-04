using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "ItemSt", menuName = "ScriptableObjects/GameDataSO")]
public class ItemSt : ScriptableObject
{
    public List<DataSpecialLevel> DataSpecials;
    [Serializable]
    public struct DataSpecialLevel
    {
    }
}
