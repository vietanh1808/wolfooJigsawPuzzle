using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CrossPromo", menuName = "ScriptableObjects/CrossPromo Data")]
public class CrossPromoSO : ScriptableObject
{
    public string[] ids;
    public Sprite[] icons;
}
