using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HandPoint : MonoBehaviour
{
    [SerializeField] Image picture;
    [SerializeField] RectTransform puzzleScrollIngame;
    [SerializeField] RectTransform mainPictureIngame;

    public static HandPoint instance;
    Tween clicked;

    public Image Picture { get => picture; set => picture = value; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }


    public void MoveHandIngame(bool enable)
    {
        if (!enable)
        {
            picture.gameObject.SetActive(false);
            picture.transform?.DOKill();
            clicked?.Kill();
            return;
        }

        clicked = DOVirtual.DelayedCall(7, () =>
        {
            OnMoveHand();
        });
    }

    void OnMoveHand()
    {
        picture.gameObject.SetActive(true);
        picture.transform.localPosition = puzzleScrollIngame.localPosition;
        DOVirtual.DelayedCall(0.5f, () =>
        {
            picture.DOFade(0, 0);
            picture.DOFade(1, 0.2f).OnComplete(() =>
            {
                picture.transform.DOLocalMove(mainPictureIngame.localPosition, 1f)
                .OnComplete(() =>
                {
                    picture.DOFade(0, 0.2f).OnComplete(() =>
                    {
                        OnMoveHand();
                    });
                });
            });
        });
    }
}
