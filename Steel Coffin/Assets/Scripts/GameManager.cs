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

    public GameObject MainMenu;

    public GameObject GameData;

    public GameObject Quit;

    public GameObject Play;

    public GameObject StartOptions;
    public bool StartOptions1;

    public GameObject Settings;
    public bool SettingsOpen = false;

    public GameObject NewGame;

    public GameObject LoadGame;

    // ItemManager Variables
    public static GameManager Instance;


    //playerdata
    Player playerData;


    void Start() 
    {
        if (SceneManager.GetActiveScene().buildIndex > 0)
        {
            playerData = GameObject.Find("player").GetComponent<Player>();
        }

        StartOptions.SetActive(false);

    }


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

    public void start()
    {
        if (StartOptions1)
        {
            StartOptions.SetActive(false);
            StartOptions1 = false;
        }
        else
        {
            StartOptions.SetActive(true);
            StartOptions1 = true;
        }
    }

    public void LoadGameData()
    {
        GameData.SetActive(true);
        MainMenu.SetActive(false);
    }

    public void MainMenuReload()
    {
        MainMenu.SetActive(true);
        GameData.SetActive(false);
    }
}

