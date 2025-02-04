using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Frame", menuName = "Frame")]
public class FrameSt : ScriptableObject
{
    [SerializeField] string title;
    [SerializeField] Sprite picture;

    public string Title { get => title; }
    public Sprite Picture { get => picture; }
}
