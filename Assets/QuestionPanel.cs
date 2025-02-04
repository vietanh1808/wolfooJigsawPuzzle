using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class QuestionPanel : MonoBehaviour
{
    [SerializeField] Button backBtn;
    [SerializeField] List<AnswerBtn> answerBtns;
    [SerializeField] Text questionTxt;
    [SerializeField] Text resultTxt;
    [SerializeField] Text notifycationTxt;
    [SerializeField] int level = 11;
    [SerializeField] int numberWrongQuestion = 3;

    int result;
    bool confirm;
    int count = 0;

    public bool Confirm { get => confirm; }

    private void Start()
    {
        backBtn.onClick.AddListener(OnBack);

        GetQuestion();
    }
    private void OnEnable()
    {
        if(answerBtns == null)
        {
            return;
        }
        count = 0;
        GetQuestion();
    }
    void OnBack()
    {
        gameObject.SetActive(false);
    }
    public void OnChooseAnswer(int idx, string answer)
    {
        resultTxt.text = answer;

        if (answerBtns[idx].ConfirmReult)
        {
            confirm = true;
            notifycationTxt.text = "Correct Answer";
            notifycationTxt.color = Color.green;

            OnBack();
            GUIManager.instance.ShopPanel.OnBuying();
        } else
        {
            confirm = false;
            notifycationTxt.text = "Incorrect Answer";
            notifycationTxt.color = Color.red;

            
            DOVirtual.DelayedCall(1.5f, () =>
            {
                count += 1;
                if (count >= numberWrongQuestion)
                {
                    OnBack();
                }
                GetQuestion();
            });
        }

        notifycationTxt.gameObject.SetActive(true);
        resultTxt.gameObject.SetActive(true);
    }
    void GetQuestion()
    {
        notifycationTxt.gameObject.SetActive(false);
        resultTxt.gameObject.SetActive(false);

        int a = Random.Range(0, level);
        int b = Random.Range(0, level);

        questionTxt.text = $"{a} x {b} = ";
        result = a * b;

        int resultPos = Random.Range(0, answerBtns.Count);
        for (int i = 0; i < answerBtns.Count; i++)
        {
            int answer = (a - i - 1) * (b + i + 1);
            if (answer == result)
            {
                answer -= 3;
            }
            answerBtns[i].AssignAnswer(i, (answer).ToString());
            answerBtns[i].AssignQuestion(result.ToString());
            if (i == resultPos)
            {
                answerBtns[i].AssignAnswer(i, result + "");
            }
        }
    }
}

