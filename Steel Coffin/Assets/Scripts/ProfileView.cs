using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ProfileView : MonoBehaviour
{
    [SerializeField] private GameObject profileEntryPrefab;
    [SerializeField] private Transform contentParent;

    private List<string> profiles = new List<string>();
    
    private void Start()
    {
        LoadProfiles();
    }

    public void LoadProfiles()
    {
        foreach (Transform child in contentParent)
        {
            Destroy(child.gameObject);
        }

        profiles = SaveSystem.ListProfiles();

        foreach (string profileName in profiles)
        {
            GameObject entry = Instantiate(profileEntryPrefab, contentParent);
            entry.GetComponentInChildren<Text>().text = profileName;    

            Button button = entry.GetComponent<Button>();
            if (button != null)
            {
                button.onClick.AddListener(() => OnProfileSelected(profileName));
            }
        }
    }

    private void OnProfileSelected(string profileName)
    {
        Debug.Log($"Profile selected: {profileName}");
    }

}
