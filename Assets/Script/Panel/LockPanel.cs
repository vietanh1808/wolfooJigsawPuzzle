using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LockPanel : MonoBehaviour
{
    [SerializeField] Button exitBtn;

    private void Start()
    {
        exitBtn.onClick.AddListener(OnExit);
    }
    void OnExit()
    {
        GameManager.OnBack?.Invoke();
    }
}
