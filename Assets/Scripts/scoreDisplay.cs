using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scoreDisplay : MonoBehaviour
{
    public Text scoreText;
    [SerializeField] private Text hiScoreText;
    public float transitionSpeed = 100;
    float score;
    float displayScore;
    static float hiScore = 0;

    public static scoreDisplay Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }
    private void Update()
    {
        displayScore = Mathf.MoveTowards(displayScore, score, transitionSpeed * Time.deltaTime);
        UpdateScoreDisplay();
    }

    public void IncreaseScore(float amount)
    {
        score += amount;
    }

    public void UpdateScoreDisplay()
    {
        scoreText.text = string.Format("Score: {0:00000}", displayScore);
    }

    public void UpdateHiScore()
    {
        if (displayScore > hiScore)
        {
            hiScore = displayScore;
        }
        // Debug.Log(hiScore);
        hiScoreText.text = string.Format("Hi-Score: {0:00000}", hiScore);
    }

}
