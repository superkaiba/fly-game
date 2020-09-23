using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class ScoreText : MonoBehaviour
{
    GameController gameController;
    TextMeshProUGUI scoreText;
    // Start is called before the first frame update
    void Start()
    {
        gameController = FindObjectOfType<GameController>();
        scoreText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = gameController.GetScore().ToString();
    }
}
