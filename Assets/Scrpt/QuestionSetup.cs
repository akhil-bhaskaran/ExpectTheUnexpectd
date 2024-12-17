using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;  

public class QuestionSetup : MonoBehaviour
{
    public List<QuizData> questions;
    private QuizData currentQuestion;

    public TextMeshProUGUI questionText;
    public AnswerButton[] answerButtons;

    private int correctAnswerChoice;


    // Start is called before the first frame update
    void Awake()
    {
        GetQuestionAssets();
    }
    void Start()
    {
        SelectQuestion();
        SetQuestionValues();
        SetAnswerValues();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GetQuestionAssets()
    {
        questions = new List<QuizData>(Resources.LoadAll<QuizData>("Questions"));
        Debug.Log(questions);
    }
    public void SelectQuestion()
    {
        int randIndex = Random.Range(0, questions.Count);
        currentQuestion = questions[randIndex];
    }
    public void SetQuestionValues()
    {
        questionText.text = currentQuestion.question;

    }
    public void SetAnswerValues()
    {
        List<string> answers = RandomizeAnswers(new List<string>(currentQuestion.answers));
        for (int i = 0; i < answerButtons.Length; i++)
        {
            bool isCorrect = false;
            if (i == correctAnswerChoice)
            {
                isCorrect = true;
            }
            answerButtons[i].SetIsCorrect(isCorrect);
            answerButtons[i].SetAnswerText(answers[i]);
        }
    }
    private List<string> RandomizeAnswers(List<string> originalList)
    {
        bool correctAnswerChoosen = false;
        List<string> newList = new List<string>();

        for (int i = 0; i < answerButtons.Length; i++)
        {
            int random = Random.Range(0, originalList.Count);

            if (random == 0 && !correctAnswerChoosen)
            {
                correctAnswerChoice = i;
                correctAnswerChoosen = true;

            }
            newList.Add(originalList[random]);
            originalList.RemoveAt(random);
        }
        return newList;
    }

}
