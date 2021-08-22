using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UiManager : MonoBehaviour
{
    [SerializeField] GameData gameData;

    [SerializeField] GameObject uiLoseMenu;
    [SerializeField] GameObject uiWinMenu;
    [SerializeField] GameObject uiInGameMenu;
    [SerializeField] GameObject uiStartGameMenu;

    [SerializeField] TMP_InputField enemyAmoutText;
    [SerializeField] TMP_InputField bombAmoutText;
    [SerializeField] TMP_Text warningText;

    bool isValidEnemy, isValidBomb, validInputValue;
    int amountOfEnemy, timeForBomb;

    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            uiLoseMenu.SetActive(false);
            uiWinMenu.SetActive(false);
            uiInGameMenu.SetActive(false);
            uiStartGameMenu.SetActive(true);
            warningText.gameObject.SetActive(false);
            validInputValue = false;
            Time.timeScale = 0;
        }
    }


    public void OnWinGame()
    {
        uiWinMenu.SetActive(true);
        Time.timeScale = 0;
        
    }

    public void OnLoseGame()
    {
        uiLoseMenu.SetActive(true);
        Time.timeScale = 0;
        FindObjectOfType<AudioManager>().Play("Lose");
    }
    public void OnTryAgain()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
    }
    public void OnNextToPlay()
    {
        ValidateInput();
        if (validInputValue)
        {
            uiInGameMenu.SetActive(true);
            uiStartGameMenu.SetActive(false);
            Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        }

        else
            warningText.gameObject.SetActive(true);

    }

    private void ValidateInput()
    {
        isValidEnemy = int.TryParse(enemyAmoutText.text, out int n);

        amountOfEnemy = (isValidEnemy == true) ? n : 0;
        isValidBomb = int.TryParse(bombAmoutText.text, out int m);
        timeForBomb = (isValidBomb == true) ? m : 0;
        Debug.Log(amountOfEnemy + " " + timeForBomb);
        if (amountOfEnemy >4 && amountOfEnemy<60 && timeForBomb>4 &&timeForBomb<60)
        {
            validInputValue = true;
            gameData.timeForBomb = timeForBomb;
            gameData.amountOfEnemy = amountOfEnemy;
        }
        
    }
}
