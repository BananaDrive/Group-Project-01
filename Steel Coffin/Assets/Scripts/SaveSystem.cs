using System.IO;
using System.Collections.Generic;
using UnityEngine;

public static class SaveSystem
{
    private static string saveDirectory = Application.persistentDataPath + "/saves/";

    // Ensure the save directory exists
    static SaveSystem()
    {
        if (!Directory.Exists(saveDirectory))
        {
            Directory.CreateDirectory(saveDirectory);
        }
    }

    // Save a profile to a JSON file
    public static void SaveProfile(SaveProfile profile)
    {
        string json = JsonUtility.ToJson(profile);
        string filePath = saveDirectory + profile.profileName + ".json";
        File.WriteAllText(filePath, json);
        Debug.Log($"Profile saved: {filePath}");
    }

    // Load a profile from a JSON file
    public static SaveProfile LoadProfile(string profileName)
    {
        string filePath = saveDirectory + profileName + ".json";

        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            return JsonUtility.FromJson<SaveProfile>(json);
        }
        else
        {
            Debug.LogError($"Profile not found: {filePath}");
            return null;
        }
    }

    // Delete a profile
    public static void DeleteProfile(string profileName)
    {
        string filePath = saveDirectory + profileName + ".json";

        if (File.Exists(filePath))
        {
            File.Delete(filePath);
            Debug.Log($"Profile deleted: {filePath}");
        }
        else
        {
            Debug.LogError($"Profile not found for deletion: {filePath}");
        }
    }

    // List all saved profiles
    public static List<string> ListProfiles()
    {
        List<string> profileNames = new List<string>();
        DirectoryInfo directoryInfo = new DirectoryInfo(saveDirectory);
        FileInfo[] files = directoryInfo.GetFiles("*.json");

        foreach (FileInfo file in files)
        {
            string profileName = Path.GetFileNameWithoutExtension(file.Name);
            profileNames.Add(profileName);
        }

        return profileNames;
    }
}
