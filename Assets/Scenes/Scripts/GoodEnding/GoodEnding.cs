using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoodEnding : MonoBehaviour
{
    public GameObject menuUI;
    public void Menu()
    {
        SceneManager.LoadScene("Menu");
        Debug.Log("load");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("quit");
    }

}