using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{

    public string menuName;
    public bool open;
    public int characterSelect;
    public GameObject[] playerText;
    //public GameObject[] swag;
    int prev = -999;

    public void Open()
    {
        open = true;
        gameObject.SetActive(true);
    }


    public void Close()
    {
        open = false;
        gameObject.SetActive(false);
    }

    public void click1()
    {
        characterSelect = 0;
        setPlayer();
        PlayerPrefs.SetInt("character", characterSelect);
    }

    public void click2()
    {
        characterSelect = 1;
        setPlayer();
        PlayerPrefs.SetInt("character", characterSelect);
    }

    public void click3()
    {
        characterSelect = 2;
        setPlayer();
        PlayerPrefs.SetInt("character", characterSelect);
    }

    void setPlayer()
    {
        if (prev != -999)
        {
            playerText[prev].SetActive(false);
        }
        prev = characterSelect;
        playerText[characterSelect].SetActive(true);
    }



}
