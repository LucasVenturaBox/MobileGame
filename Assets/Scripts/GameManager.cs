using System;
using MobileGame.Input;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum SceneNames
{
    MainMenu,
    Level1,
}
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

   

    private bool _isPaused;

    private void OnEnable() {
        PlayerBrain.PauseHandler -= Pause;
        InGameUI.QuitGameHandler -= Quit;
        //Make sure the event is clean with no listeners

        //Subscribe the events needed
        PlayerBrain.PauseHandler += Pause;
        InGameUI.QuitGameHandler+= Quit;

    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(this);
    }

    private void OnDisable() {
        PlayerBrain.PauseHandler -= Pause;
        InGameUI.QuitGameHandler-= Quit;
    }

    private void Pause()
    {
        if(_isPaused)
        {
            Time.timeScale = 1;
            _isPaused = false;
        }
        else
        {
            Time.timeScale = 0;
            _isPaused = true;
        }
    }

    

    private void Quit()
    {
        Application.Quit();
    }

#region UI Calls
    public void OnPause()
    {
        PlayerBrain.PauseHandler?.Invoke();
    }

    public void OnQuit()
    {
        InGameUI.QuitGameHandler?.Invoke();
    }

    public void LoadScene(string sceneName)
    {
        SceneNames enumSceneName;

        if (!Enum.TryParse<SceneNames>(sceneName, out enumSceneName))
        {
            Debug.LogError("That scene name is invalid");
            return;
        }

        SceneManager.LoadScene(enumSceneName.ToString());
    }
    #endregion
}
