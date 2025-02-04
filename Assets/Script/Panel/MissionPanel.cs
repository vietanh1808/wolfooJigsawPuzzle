// using SCN.Ads;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionPanel : MonoBehaviour
{
    [SerializeField] Text textContent;
    [SerializeField] Button claim;

    public static MissionPanel instance;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    private void OnEnable()
    {
        //AdsManager.Instance.HideBanner();
    }
    private void OnDisable()
    {
       // AdsManager.Instance.ShowBanner();
    }
    private void Start()
    {
        claim.onClick.AddListener(OnClaimReward);
    }
    void OnClaimReward()
    {
        gameObject.SetActive(false);
    }
    public void SetContent(int number)
    {
        textContent.text = $"You need to complete {number} more  pictures";
    }
    public void SetContent(List<string> conditions)
    {
        textContent.text = "You need more pictures \n ";
        
        foreach(string condition in conditions)
        {
            textContent.text += $" - {condition} \n";
        }
    }
}
