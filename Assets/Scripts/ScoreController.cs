using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreController : MonoBehaviour
{
   private TextMeshProUGUI scoreText;
    private int score;

    private void Awake()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
    }
    private void Start()
    {
        refreshUI();
    }
    public void incrementScore(int increment)
    {
        score += increment;
        refreshUI();
    }
    public void refreshUI()
    {
        scoreText.text = "Score :" + score;
    }

}
