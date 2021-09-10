using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{

    public static bool isGameOver = false;
    public void Setup()
    {

        isGameOver = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        gameObject.SetActive(true);
    }


    public void returnToMenu()
    {
        isGameOver = false;
        SceneManager.LoadScene("Menu");
    }

    public void restart()
    {
        isGameOver = false;
        SceneManager.LoadScene("Arena");
    }

    public void characterSelect()
    {
        isGameOver = false;
        SceneManager.LoadScene("LevelSelect");
    }


    public void quit()
    {
        Application.Quit();
    }
}
