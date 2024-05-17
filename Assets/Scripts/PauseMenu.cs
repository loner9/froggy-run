using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject PauseMenuPanel;
    [SerializeField] private Button[] buttons;
    [SerializeField] private GameObject GameOverText;
    [SerializeField] private GameObject hiScoreText;
    //pause
    //restart
    //resume
    static bool isFirst = true;
    bool isOver;

    public static PauseMenu Instance { get; private set; }
    private void Start()
    {
        // Debug.Log(PauseMenuPanel.transform.GetChild(0).gameObject.);
        // PauseButton.interactable = false;
        Instance = this;

        if (isFirst)
        {
            buttons[0].interactable = false;
            Pause();
            hiScoreText.SetActive(false);
            buttons[1].interactable = false;
            isFirst = false;
        }
        else
        {
            hiScoreText.SetActive(true);
            scoreDisplay.Instance.UpdateHiScore();
        }
    }

    public void Pause()
    {
        if (!isFirst)
        {
            // buttons[0].interactable = true;
            buttons[1].interactable = true;
        }
        PauseMenuPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        if (!isFirst)
        {
            buttons[0].interactable = true;
        }
        PauseMenuPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("SampleScene");
    }

    public void GameOver()
    {
        scoreDisplay.Instance.UpdateHiScore();
        if (!isFirst)
        {
            hiScoreText.SetActive(true);
        }
        Pause();
        buttons[0].interactable = false;
        buttons[2].interactable = false;
        GameOverText.SetActive(true);
    }
}
