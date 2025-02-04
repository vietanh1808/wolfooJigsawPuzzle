using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnswerBtn : MonoBehaviour
{
    [SerializeField] Button button;
    [SerializeField] Text answer;

    int id;
    bool m_confirmReult;
    string question;

    public bool ConfirmReult { get => m_confirmReult; }

    private void Start()
    {
        button.onClick.AddListener(OnButtonClick);
    }
    void OnButtonClick()
    {
        CheckAnswer();
        GUIManager.instance.QuestionPanel.OnChooseAnswer(id, answer.text);
    }
    public void AssignAnswer(int _id, string _answer)
    {
        answer.text = _answer;
        id = _id;
    }
    public void AssignQuestion(string _question)
    {
        question = _question;
    }
    void CheckAnswer()
    {
        if (answer.text == question)
        {
            m_confirmReult = true;
        }
        else
        {
            m_confirmReult = false;
        }
    }
}
