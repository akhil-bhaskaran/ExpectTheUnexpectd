using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class AnswerButton : MonoBehaviour
{
    private bool isCorrect;
    private TimeSpan  duration =new TimeSpan(0,1,30);
    [SerializeField]
    private TextMeshProUGUI answerText;
    //public GameObject questionPannel;
    public void SetAnswerText(string newText)
    {
        answerText.text = newText;
    }
    public void SetIsCorrect(bool newBool)
    {
        isCorrect = newBool;
    }
    public void OnClick()
    {
        if (isCorrect)
        {
            Time.timeScale = 1.0f;
            Debug.Log("Correct Answer");
            DataManager.Instance.LivesRemaining = 4;
            StartCoroutine(DataManager.Instance.SaveDataToFirebase());
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        }
        else
        {
            Debug.Log("Wrong answer!");
            DataManager.Instance.endtime = DateTime.UtcNow.Add(duration).ToString();
            Debug.Log(DateTime.UtcNow);
            DataManager.Instance.TimeBreak=true;
            StartCoroutine(DataManager.Instance.SaveDataToFirebase());
            SceneManager.LoadScene("HomePage");
        }
    }
}

