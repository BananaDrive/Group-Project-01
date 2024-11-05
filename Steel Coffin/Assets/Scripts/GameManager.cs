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

    public GameObject PauseMenu;

    // ItemManager Variables
    public static GameManager Instance;

    private InteractableObject currentActiveItem;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() 
    {
    
    }

    // Update is called once per frame
    void Update() 
    { 
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseScreen();
        }
    }

    // ItemManager Script Pieces
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void SetActiveItem(InteractableObject newItem)
    {
        if (currentActiveItem != null && currentActiveItem != newItem)
        {
            currentActiveItem.CheckAndDisable();
        }

        currentActiveItem = newItem;
    }

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

}

