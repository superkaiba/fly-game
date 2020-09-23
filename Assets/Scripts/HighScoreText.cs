using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighScoreText : MonoBehaviour
{
    TextMeshProUGUI highScoreText;
    int highScore;
    // Start is called before the first frame update
    void Start()
    {
        highScoreText = GetComponent<TextMeshProUGUI>();
        UpdateHighScoreText();
    }

    public void UpdateHighScore(int potentialHighScore)
    {
        if (potentialHighScore > highScore)
        {
            highScore = potentialHighScore;
            PlayerPrefsController.SetHighScore(potentialHighScore);
            UpdateHighScoreText();
        }
    }

    private void UpdateHighScoreText()

    {
        highScore = PlayerPrefsController.GetHighScore();
        highScoreText.text = "Farthest Flown: " + highScore.ToString() + "m";
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
