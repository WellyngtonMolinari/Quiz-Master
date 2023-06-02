using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quiz Question", fileName = "New Question")]
public class QuestionSO : ScriptableObject
{
    // allow us to control the size of the box in the inspector
    [TextArea(2, 6)]
    [SerializeField] string question = "Enter new question text here";


}
