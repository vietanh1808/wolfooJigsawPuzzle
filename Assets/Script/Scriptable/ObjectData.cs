using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "ObjectData", menuName = "Scriptable/Object Data")]
public class ObjectData : ScriptableObject
{
    public List<ItemCategory> itemCategories;
    [Serializable]
    public struct ItemCategory
    {
        public string Name;
        public TopicSt[] topicSt;
    }
}
