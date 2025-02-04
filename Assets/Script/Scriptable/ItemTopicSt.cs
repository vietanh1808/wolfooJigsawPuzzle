using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Topic", menuName = "Item Topic")]
public class ItemTopicSt : ScriptableObject
{
    public Sprite picture;
    public bool status;
    public string title;
    public List<Sprite> puzzles;
}
