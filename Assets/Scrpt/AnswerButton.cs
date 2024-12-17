using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class AnswerButton : MonoBehaviour
{
    private bool isCorrect;
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
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else
        {
            Debug.Log("Wrong answer!");
            SceneManager.LoadScene("HomePage");
        }
    }
}

