using UnityEngine;
using TMPro;
using MobileGame.Input;
using System;

public class InGameUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMeshPro;
    [SerializeField] private string newText;

    [SerializeField] private GameObject pauseMenu;
    private int _score = 0;


    private void OnEnable() 
    {
        PlayerBrain.PauseHandler += ChangePauseState;
        PlayerBrain.JumpHandler += IncrementScore;
        PlayerBrain.FailHandler += LoseScore;
    }

    private void OnDisable() 
    {
       PlayerBrain.PauseHandler -= ChangePauseState;
       PlayerBrain.JumpHandler -= IncrementScore;
        PlayerBrain.FailHandler -= LoseScore;
    }

    private void IncrementScore()
    {
        _score++;
        UpdateScore();
    }

    private void LoseScore()
    {
        _score = 0;
        UpdateScore();
    }
    private void UpdateScore()
    {
        string scoreDefaulText = "Score: ";
        textMeshPro.text = scoreDefaulText + _score.ToString();
    }

    private void ChangePauseState()
    {
        if(pauseMenu.activeInHierarchy)
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1f;
        }
        else
        {
            Time.timeScale = 0f;
            pauseMenu.SetActive(true);
        }
    }
}
