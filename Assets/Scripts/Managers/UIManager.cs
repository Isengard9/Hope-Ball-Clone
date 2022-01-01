using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField] Text HighScoreText;//End Panel High Score Text Variable
    [SerializeField] Text CurrentScoreText;//End Panel Current Score Text Variable
    [SerializeField] Text CurrentScoreUI;//UI Panel Current Score Text Variable
    [SerializeField] Text CurrentPointText;//UI Panel Collected Point Text Variable;
    [SerializeField] GameObject EndPanel;
    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void ChangeHighScore(int HighScore) {
        HighScoreText.text = "High Score " + HighScore + "m";
    }

    public void ChangeCurrentScore(int CurrentScore)
    {
        CurrentScoreUI.text = "" + CurrentScore + "m";
        CurrentScoreText.text = "Score " + CurrentScore + "m";
    }

    public void ChangeCurrentPoint(int point)
    {
        CurrentPointText.text = "" + point;
    }

    public void RestartGame()
    {
        GameManager.isGameStarted = GameManager.isGameEnded = false;
        GameManager.instance.RestartGame();
    }

    public void OpenEndPanel() { EndPanel.SetActive(true); }
}
