using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class GameManager : MonoBehaviour
{
    public bool IsPaused = false; // Pause variable

    // ItemManager Variables
    public static ItemManager Instance;

    private InteractableObject currentActiveItem;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() 
    {
    
    }

    // Update is called once per frame
    void Update() 
    { 
    
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
}
}
