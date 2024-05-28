using UnityEngine;
using TMPro;
using MobileGame.Input;
using UnityEngine.SceneManagement;

public class InGameUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMeshPro;
    private string newText;

    [SerializeField] private GameObject pauseMenu;
    private int _score = 0;

    [SerializeField] private GameObject winningScreen;


    private void OnEnable() 
    {
        PlayerBrain.PauseHandler += ChangePauseState;
        PlayerBrain.JumpHandler += IncrementScore;
        PlayerBrain.FailHandler += LoseScore;
        PlayerBrain.VictoryHandler += WinningScreen;
    }

    private void OnDisable() 
    {
       PlayerBrain.PauseHandler -= ChangePauseState;
       PlayerBrain.JumpHandler -= IncrementScore;
        PlayerBrain.FailHandler -= LoseScore;
        PlayerBrain.VictoryHandler -= WinningScreen;
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

    public void Quit()
    {
        Application.Quit();
    }
}
