using UnityEngine;
using System.Collections.Generic;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using TMPro;

public class GameManager : MonoBehaviour
{
    public bool IsPaused = false;
    public bool ismenu;

    // UI Elements
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

    // Singleton Instance
    public static GameManager Instance;

    // Player Data
    Player playerData;

    // Save Profile Management
    private SaveProfile currentProfile;
    private string currentProfileName;
    private static string saveDirectory = Application.persistentDataPath + "/saves/";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

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

    // Save the Game
    public void SaveGame()
    {
        if (currentProfile != null)
        {
            currentProfile.level = SceneManager.GetActiveScene().buildIndex;
            currentProfile.playerPosition = playerData.transform.position;
            SaveSystem.SaveProfile(currentProfile);
            Debug.Log("Game Saved: " + currentProfileName);
        }
    }

    // Load the Game
    public void LoadPreviousGame(string profileName)
    {
        currentProfile = SaveSystem.LoadProfile(profileName);
        if (currentProfile != null)
        {
            currentProfileName = profileName;
            SceneManager.LoadScene(currentProfile.level);
            Debug.Log("Game Loaded: " + currentProfileName);
        }
    }

    // New Game
    public void CreateNewGame(int sceneID)
    {
        string newProfileName = "Profile_" + System.Guid.NewGuid().ToString();
        currentProfile = new SaveProfile(newProfileName);
        currentProfileName = newProfileName;

        SaveSystem.SaveProfile(currentProfile);
        SceneManager.LoadScene(sceneID);
        Time.timeScale = 1;
    }

    // Delete Game Profile
    public void DeleteProfile(string profileName)
    {
        SaveSystem.DeleteProfile(profileName);
        Debug.Log("Profile Deleted: " + profileName);
    }

    public void PauseScreen()
    {
        PauseMenu.SetActive(true);
        IsPaused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0;
    }

    public void Resume()
    {
        IsPaused = false;
        PauseMenu.SetActive(false);
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void LoadLevel(int sceneID)
    {
        SceneManager.LoadScene(sceneID);
    }

    public void Restart()
    {
        LoadLevel(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }

    public void Exit()
    {
        SaveGame();
        Application.Quit();
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
