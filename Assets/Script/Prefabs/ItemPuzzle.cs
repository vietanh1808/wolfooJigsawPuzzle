using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class ItemPuzzle : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private Image targetImg;
    private int id;
    private bool hidden;
    private bool special;
    private Image img;

    public void AssignTarget(Image tareget_, int id_, bool _hidden, bool _special)
    {
        targetImg = tareget_;
        id = id_;
        hidden = _hidden;
        special = _special;

        img = GetComponent<Image>();
        img.preserveAspect = true;
        img.rectTransform.sizeDelta = new Vector2(200, 200);

        if (hidden && !special)
        {
            targetImg.sprite = GUIManager.instance.QuestionImg;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!GameManager.Ins.DragIngame)
        {
            return;
        }

        if (!hidden)
        {
        img.SetNativeSize();
            targetImg.transform.DOScale(1f, 0);
            targetImg.color = Color.black;
            targetImg.DOFade(0.7f, 0);
            targetImg.transform.DOScale(1.2f, 0.3f)
                .OnStart(() =>
                {
                    transform.DOScale(1.2f, 0.3f);
                });
        }
        else {
            img.rectTransform.sizeDelta = new Vector2(350, 350);
        }
        targetImg.gameObject.SetActive(true);
        GameManager.OnPuzzleBeginDrag(id);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!GameManager.Ins.DragIngame)
        {
            return;
        }
        if (special)
        {
            targetImg.DOFade(0.5f, 0);
        }
        GameManager.OnPuzzleDrag?.Invoke(id, eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        GameManager.Ins.DragIngame = false;

    
        CheckPos();

        if (special)
        {
            targetImg.DOFade(0, 0);
        }
        if (!hidden)
        {
            img.rectTransform.sizeDelta = new Vector2(200, 200);
            targetImg.DOKill();
            targetImg.transform.DOScale(1f, 0.2f)
                .OnStart(() =>
                {
                    gameObject.transform.DOScale(1f, 0.2f);
                })
                .OnComplete(() =>
                {
                    targetImg.DOFade(0, 0);
                    targetImg.gameObject.SetActive(false);
                    var distance = Vector2.Distance(transform.position, targetImg.transform.position);
                    GameManager.OnPuzzlEndDrag?.Invoke(distance, id, hidden, targetImg);
                });
        }
        else
        {
            var distance = Vector2.Distance(transform.position, targetImg.transform.position);
            GameManager.OnPuzzlEndDrag?.Invoke(distance, id, hidden, targetImg);
        }
    }

    void CheckPos()
    {
        var pos = Camera.main.WorldToScreenPoint(transform.position);
        if (pos.x > (Screen.safeArea.xMax))
        {
            Vector3 newpos = new Vector3(Screen.safeArea.xMax, pos.y, pos.z);
            transform.position = new Vector3(Camera.main.ScreenToWorldPoint(newpos).x, transform.position.y, transform.position.z);
        }
        if (pos.x < Screen.safeArea.xMin)
        {
            Vector3 newpos = new Vector3(Screen.safeArea.xMin, pos.y, pos.z);
            transform.position = new Vector3(Camera.main.ScreenToWorldPoint(newpos).x, transform.position.y, transform.position.z);
        }
        if (pos.y > Screen.safeArea.yMax)
        {
            Vector3 newpos = new Vector3(pos.x, Screen.safeArea.yMax, pos.z);
            transform.position = new Vector3(transform.position.x, Camera.main.ScreenToWorldPoint(newpos).y, transform.position.z);
        }
        if (pos.y < Screen.safeArea.yMin)
        {
            Vector3 newpos = new Vector3(pos.x, Screen.safeArea.yMin, pos.z);
            transform.position = new Vector3(transform.position.x, Camera.main.ScreenToWorldPoint(newpos).y, transform.position.z);
        }
    }

    private void OnBecameInvisible()
    {
        Debug.Log("Invisible");
    }

    private void OnBecameVisible()
    {
        Debug.Log("Visible");
    }
}
