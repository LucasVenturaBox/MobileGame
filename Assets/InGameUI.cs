using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InGameUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMeshPro;
    [SerializeField] private string newText;

    [SerializeField] private GameObject pauseMenu;


    private void OnEnable() 
    {
        Movement.KeyInputDelegate += NewText;
        Movement.PauseHandler += ChangePauseState;
    }

    private void OnDisable() 
    {
        Movement.KeyInputDelegate -= NewText;
        Movement.PauseHandler -= ChangePauseState;
    }

    private void NewText()
    {
        textMeshPro.text = newText;
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
