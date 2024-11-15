using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEditor.SearchService;
using TMPro;

public class GameManager : MonoBehaviour
{
    public bool IsPaused = false; // Pause variable

    public bool ismenu;

    public GameObject PauseMenu;

    public GameObject Quit;

    public GameObject Play;

    public GameObject Settings;
    public bool SettingsOpen = false;

    public GameObject NewGame;

    public GameObject LoadGame;

    // ItemManager Variables
    public static GameManager Instance;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() 
    {
    
        

    }

    // Update is called once per frame
    void Update() 
    { 
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!ismenu)
            {
                PauseScreen();
            }
        }
    }

    // ItemManager Script Pieces


    public void PauseScreen()
    {
        //PlayerInterface.SetActive(false);   //Activate whan Interface is Attached
        PauseMenu.SetActive(true);
        IsPaused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0;

    }

    public void LoadLevel(int sceneID)
    {
        SceneManager.LoadScene(sceneID);
    }

    public void Resume()
    {
        IsPaused = false;
        PauseMenu.SetActive(false);
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void Restart()
    {
        LoadLevel(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Newgame(int sceneID)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(sceneID);
    }

    public void Settings1()
    {
        if (SettingsOpen)
        {
           Settings.SetActive(false);
           SettingsOpen = false;
           Cursor.visible = false;
           Cursor.lockState = CursorLockMode.Locked;
           Time.timeScale = 1;
        }
        else
        {
            Settings.SetActive(true);
            SettingsOpen = true;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
        }
    }

    public void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
    }
}

