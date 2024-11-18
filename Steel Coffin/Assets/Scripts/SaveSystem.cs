using UnityEngine;
using System.IO;

public static class SaveSystem
{
    private static string saveDirectory = Application.persistentDataPath + "/Saves/";

    static SaveSystem()
    {
        if (!Directory.Exists(saveDirectory))
        {
            Directory.CreateDirectory(saveDirectory);
        }
    }

    public static void SaveProfile(SaveProfile profile)
    {
        string json = JsonUtility.ToJson(profile);
        File.WriteAllText(saveDirectory + profile.profileName + ".json", json);
    }

    public static SaveProfile LoadProfile(string profileName)
    {
        string path = saveDirectory + profileName + ".json";
        if (File.Exists(path)) 
        {
            string json = File.ReadAllText(path);
            return JsonUtility.FromJson<SaveProfile>(json);
        }
        return null;
    }

    public static void DeleteProfile(string profileName)
    {
        string path = saveDirectory + profileName + ".json";
        if (File.Exists(path)) 
        {
            File.Delete(path);
        }
    }
}

