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
    public GameObject Quit;
    public GameObject Play;
    public GameObject Settings;
    public bool SettingsOpen = false;
   

    //NOTES
    public GameObject Note1;
    public GameObject Note2;
    public GameObject Note3;
    public Transform Player;
    public float NoteRange = 3;

    public GameObject NoteText;
    public GameObject NoteText1;
    public GameObject NoteText2;

    private GameObject activeNoteText;

    // Singleton Instance
    public static GameManager Instance;

    // Player Data
    Player playerData;
    PlayerInventory playerInventory;

    //Quality Settings
    public TMP_Dropdown Quality;


    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex > 0)
        {
            playerData = GameObject.Find("player").GetComponent<Player>();
            playerInventory = GameObject.Find("player").GetComponent<PlayerInventory>();
        }

        if (NoteText != null)
        {
            NoteText.SetActive(false);
        }
        if (NoteText1 != null)
        {
            NoteText1.SetActive(false);
        }
        if (NoteText2 != null)
        {
            NoteText2.SetActive(false);
        }

        int savedQualityLevel = PlayerPrefs.GetInt("QualityLevel", QualitySettings.GetQualityLevel());
        SetQuality(savedQualityLevel);

        Quality.value = savedQualityLevel;
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

        if(IsPaused)
        {
            Paused();
        }

        NoteInteract();
    }

    public void SetQuality(int QualityLvl)
    {
        QualitySettings.SetQualityLevel(QualityLvl);

        PlayerPrefs.SetInt("QualityLevel", QualityLvl);
        PlayerPrefs.Save();
    }

    private void NoteInteract()
    {
        
        HandleNoteInteraction(Note1, NoteText, ref activeNoteText);
        HandleNoteInteraction(Note2, NoteText1, ref activeNoteText);
        HandleNoteInteraction(Note3, NoteText2, ref activeNoteText);
    }

    private void HandleNoteInteraction(GameObject note, GameObject noteText, ref GameObject currentActiveNoteText)
    {
        float distanceToNote = Vector3.Distance(Player.position, note.transform.position);

        if (distanceToNote <= NoteRange)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                
                if (currentActiveNoteText != null && currentActiveNoteText != noteText)
                {
                    currentActiveNoteText.SetActive(false);
                }

                
                bool isActive = !noteText.activeSelf;
                noteText.SetActive(isActive);

                
                currentActiveNoteText = isActive ? noteText : null;
            }
        }
        else if (noteText.activeSelf)
        {
            
            noteText.SetActive(false);
            if (currentActiveNoteText == noteText)
            {
                currentActiveNoteText = null;
            }
        }
    }



    public void PauseSettingOpen()
    {
        Settings.SetActive(true);
        PauseMenu.SetActive(false);
    }

    public void PauseSettingClose()
    {
        Settings.SetActive(false);
        PauseMenu.SetActive(true);
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
        Application.Quit();
    }

    public void Settings1()
    {
        if (SettingsOpen)
        {
            Settings.SetActive(false);
            MainMenu.SetActive(true);
            SettingsOpen = false;
            Time.timeScale = 1;
        }
        else
        {
            Settings.SetActive(true);
            MainMenu.SetActive(false);
            SettingsOpen = true;
            Time.timeScale = 0;
        }
    }

    public void LoadNextLevel()
    {
        if (playerInventory.keyPickup)
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            int nextSceneIndex = currentSceneIndex + 1;

            if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
            {
                SceneManager.LoadScene(nextSceneIndex);
            }
        }
    }

    public void ReturnTomenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Paused()
    {
        Time.timeScale = 0;
    }
}
