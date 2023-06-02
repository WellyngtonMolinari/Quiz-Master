using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Quiz : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] QuestionSO question;
    // array to store all answers buttons gameObjects 
    [SerializeField] GameObject[] answerButtons;

    void Start()
    {
        questionText.text = question.GetQuestion();

        // run the loop while i is less than the lenght of our answerButtons array
        for (int i = 0; i < answerButtons.Length; i++)
        {
            // get the children component of answer button gameObject and find the first textMeshPro component 
            TextMeshProUGUI buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            // get the answer at the array position
            buttonText.text = question.GetAnswer(i);
        }
    }
}
