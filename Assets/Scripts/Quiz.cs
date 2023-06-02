using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] QuestionSO question;
    // array to store all answers buttons gameObjects 
    [SerializeField] GameObject[] answerButtons;
    int correctAnswerIndex;
    [SerializeField] Sprite defaultAnswerSprite;
    [SerializeField] Sprite correctAnswerSprite;
    [SerializeField] Sprite wrongAnswerSprite;
    Image buttonImage;

    void Start()
    {
        //DisplayQuestion();
        GetNextQuestion();
    }

    // control when a button is selected
    public void OnAnswerSelected(int index)
    {
        if (index == question.GetCorrectAnswerIndex())
        {
            questionText.text = "Correct!";
            CorrectAnswer(index);
        }
        else
        {
            WrongAnswer(index);
        }
        SetButtonsState(false);
    }

    private void CorrectAnswer(int index)
    {
        buttonImage = answerButtons[index].GetComponent<Image>();
        buttonImage.sprite = correctAnswerSprite;
    }

    private void WrongAnswer(int index)
    {
        correctAnswerIndex = question.GetCorrectAnswerIndex();
        string correctAnswer = question.GetAnswer(correctAnswerIndex);
        questionText.text = "Wrong answer! The correct one is:\n " + correctAnswer;
        buttonImage = answerButtons[index].GetComponent<Image>();
        buttonImage.sprite = wrongAnswerSprite;
    }

    private void DisplayQuestion()
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

    private void SetButtonsState(bool state)
    {
        for (int i = 0; i < answerButtons.Length; i++)
        {
            Button button = answerButtons[i].GetComponent<Button>();
            button.interactable = state;
        }
    }

    private void GetNextQuestion()
    {
        SetButtonsState(true);
        SetDefaultButtonSprites();
        DisplayQuestion();
    }

    private void SetDefaultButtonSprites()
    {
        for (int i = 0; i < answerButtons.Length; i++)
        {
            buttonImage = answerButtons[i].GetComponent<Image>();
            buttonImage.sprite = defaultAnswerSprite;
        }
    }

}
