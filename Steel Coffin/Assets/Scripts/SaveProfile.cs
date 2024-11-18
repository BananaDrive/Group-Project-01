using UnityEngine;
using System;

[System.Serializable]
public class SaveProfile
{
    public string profileName;
    public int level;
    public Vector3 playerPosition;

    public SaveProfile(string name)
    {
        profileName = name;
        level = 1;
        playerPosition = Vector3.zero;
    }
}
