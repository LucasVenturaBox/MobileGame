using UnityEngine;
using TMPro;
using MobileGame.Input;
using UnityEngine.SceneManagement;
using System;
using MobileGame.Move;

public class InGameUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMeshPro;
    private string newText;

    [SerializeField] private GameObject pauseMenu;
    private int _score = 0;

    [SerializeField] private GameObject winningScreen;

    public static Action QuitGameHandler;


    private void OnEnable() 
    {
        PlayerBrain.PauseHandler += ChangePauseState;
        ObstacleSpawner.SuccessfulWaveHandler += IncrementScore;
        Movement.FailHandler += LoseScore;
        ObstacleSpawner.VictoryHandler += WinningScreen;
    }

    private void OnDisable() 
    {
        PlayerBrain.PauseHandler -= ChangePauseState;
        ObstacleSpawner.SuccessfulWaveHandler -= IncrementScore;
        Movement.FailHandler -= LoseScore;
        ObstacleSpawner.VictoryHandler -= WinningScreen;
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

    private void WinningScreen()
    {
        winningScreen.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ChangePauseState()
    {
        Debug.Log("Did Something");
        if(pauseMenu == null) return;
        Debug.Log("It is not null");
        if(pauseMenu.activeInHierarchy)
        {
            pauseMenu.SetActive(false);
        }
        else
        {
            pauseMenu.SetActive(true);
        }
    }

    public void Quit()
    {
        QuitGameHandler?.Invoke();
    }
    
}
