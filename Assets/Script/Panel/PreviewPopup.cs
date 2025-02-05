using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PreviewPopup : MonoBehaviour
{
    [SerializeField] Image pictureImg;

public void Assgin(Sprite sprite) {
    pictureImg.sprite = sprite;
    
}
    public void Show(){
        gameObject.SetActive(true);
    }
    public void Hide(){
        gameObject.SetActive(false);
    }
}
