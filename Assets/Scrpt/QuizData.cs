using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuizData",menuName ="ScriptableObject/Question",order =1)]
public class QuizData : ScriptableObject
{
    public string question;
    [Tooltip("The correct answer should be in the first , they after will be rearragned")]
    public string[] answers;
}
