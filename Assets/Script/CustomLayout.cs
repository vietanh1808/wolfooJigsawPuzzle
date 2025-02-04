using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CustomLayout : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }
    [NaughtyAttributes.Button]
    public void Resize()
    {
        Debug.Log("Resizing Layout.....");
        var myLayout = GetComponent<GridLayoutGroup>();
        var myRect = GetComponent<RectTransform>();
        var totalRow = myRect.rect.width / (myLayout.cellSize.x + myLayout.spacing.x * 2);
        totalRow = myLayout.cellSize.x *  Mathf.Ceil(totalRow)  + myLayout.spacing.x *  Mathf.Ceil(totalRow)  - myLayout.spacing.x * 2 < myRect.rect.width ? Mathf.Ceil(totalRow) : Mathf.Floor(totalRow);
        Debug.Log("Total Row: " + totalRow);
        var height = (myLayout.cellSize.y + (myLayout.spacing.y)) * transform.childCount / totalRow + myLayout.spacing.y * 2;

        myRect.sizeDelta = new Vector2(myRect.sizeDelta.x, height);
    }

}
