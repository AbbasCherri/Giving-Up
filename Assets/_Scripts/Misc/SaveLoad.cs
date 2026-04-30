using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.IO;
using UnityEngine;
[System.Serializable]
public class SaveLoad
{
    private static string path = Application.persistentDataPath + "/save.json";

    public static void Save(Player player)
    {
        PlayerData data = new PlayerData();
        data.health = player.currhealth;
        data.position = player.transform.position;
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(path, json);
        Debug.Log("Saved PlayerData" + path);
    }

    public static PlayerData Load()
    {
        if (!File.Exists(path))
        {
            Debug.LogWarning("No save file found");
            return null;
        }

        string json = File.ReadAllText(path);
        return JsonUtility.FromJson<PlayerData>(json);
    }
}
