using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject menuUI;
    public void LoadGame()
    {
        SceneManager.LoadScene("ProlougePhase1");
        Debug.Log("load");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("quit");
    }

}