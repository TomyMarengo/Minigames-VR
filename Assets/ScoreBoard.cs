using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Shows an ordered list of messages via a text mesh
/// </summary>
public class ScoreBoard : MonoBehaviour
{
    [Tooltip("The text mesh the message is output to")]
    public TextMeshProUGUI messageOutput = null;

    // What happens once the list is completed

    public int score = 0;


    private void Start()
    {
        ShowMessage();
    }

    public void ResetScore()
    {
        score = 0;
        UpdateScore();
    }

    public void UpdateScore()
    {
        ShowMessage();
    }


    private void ShowMessage()
    {
        messageOutput.text = "Score: " + score;
    }

 
}
