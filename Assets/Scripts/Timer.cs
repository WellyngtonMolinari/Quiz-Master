using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField ]float timeToCompleteQuestion = 30f;
    [SerializeField ]float timeToShowCorrectAnswer = 10f;
    public bool isAnswringQuestion = false;
    float timerValue;

    void Update()
    {
        UpdateTimer();
    }
    
    void UpdateTimer()
    {
        timerValue -= Time.deltaTime;

        if(isAnswringQuestion)
        {
            if(timerValue <=0)
            {
                isAnswringQuestion = false;
                timerValue = timeToShowCorrectAnswer;
            }
        }
        else
        {
            if(timerValue <= 0)
            {
                isAnswringQuestion = true;
                timerValue = timeToCompleteQuestion;
            }
        }

        Debug.Log(timerValue);
    }
}
