using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
    [Header("Questions")]
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] List<QuestionSO> questions = new List<QuestionSO>();
    QuestionSO currentQuestion;

    [Header("Answers")]
    // array to store all answers buttons gameObjects 
    [SerializeField] GameObject[] answerButtons;
    int correctAnswerIndex;
    bool hasAnsweredEarly;

    [Header("Button Colors")]
    [SerializeField] Sprite defaultAnswerSprite;
    [SerializeField] Sprite correctAnswerSprite;
    [SerializeField] Sprite wrongAnswerSprite;
    Image buttonImage;

    [Header("Timer")]
    [SerializeField] Image timerImage;
    // call the Timer.cs script 
    Timer timer;
    private int index;

    void Start()
    {
        timer = FindObjectOfType<Timer>();
    }

    void Update()
    {
        timerImage.fillAmount = timer.fillFraction;
        if (timer.loadNextQuestion)
        {
            hasAnsweredEarly = false;
            GetNextQuestion();
            // to prevent from getting a new question every frame
            timer.loadNextQuestion = false;
        }
        // when times reachs 0
        else if (!hasAnsweredEarly && !timer.isAnsweringQuestion)
        {
            CorrectAnswer(index);
            SetButtonsState(false);
        }
    }

    // control when a button is selected
    public void OnAnswerSelected(int index)
    {
        if (index == currentQuestion.GetCorrectAnswerIndex())
        {
            questionText.text = "Correct!";
            CorrectAnswer(index);
        }
        else
        {
            WrongAnswer(index);
        }

        hasAnsweredEarly = true;
        SetButtonsState(false);
        timer.CancelTimer();
    }

    private void CorrectAnswer(int index)
    {
        buttonImage = answerButtons[index].GetComponent<Image>();
        buttonImage.sprite = correctAnswerSprite;
    }

    private void WrongAnswer(int index)
    {
        correctAnswerIndex = currentQuestion.GetCorrectAnswerIndex();
        string correctAnswer = currentQuestion.GetAnswer(correctAnswerIndex);
        questionText.text = "Wrong answer! The correct one is:\n " + correctAnswer;

        buttonImage = answerButtons[index].GetComponent<Image>();
        buttonImage.sprite = wrongAnswerSprite;

        buttonImage = answerButtons[correctAnswerIndex].GetComponent<Image>();
        buttonImage.sprite = correctAnswerSprite;
    }


    private void DisplayQuestion()
    {
        questionText.text = currentQuestion.GetQuestion();

        // run the loop while i is less than the lenght of our answerButtons array
        for (int i = 0; i < answerButtons.Length; i++)
        {
            // get the children component of answer button gameObject and find the first textMeshPro component 
            TextMeshProUGUI buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            // get the answer at the array position
            buttonText.text = currentQuestion.GetAnswer(i);
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
        if (questions.Count > 0)
        {
            SetButtonsState(true);
            SetDefaultButtonSprites();
            GetRandomQuestion();
            DisplayQuestion();
        }
    }

    private void GetRandomQuestion()
    {
        int index = Random.Range(0, questions.Count);
        currentQuestion = questions[index];

        if (questions.Contains(currentQuestion))
        {
            questions.Remove(currentQuestion);
        }
        questions.Remove(currentQuestion);
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
