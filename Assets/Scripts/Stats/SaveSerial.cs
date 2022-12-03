using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveSerial : MonoBehaviour
{
    private SaveData data;
    private StatsRecorder stats;

    void Start ()
    {
        //ResetData();
        if (!LoadGame())
        {
            data = new SaveData();
        }
        Debug.Log(data.highScore);
        Debug.Log(Application.persistentDataPath);
        stats = GameObject.Find("StatsManager").GetComponent<StatsRecorder>();
    }
    void OnApplicationQuit()
    {
        if (stats.getScore() > data.highScore)
        {
            data.highScore = stats.getScore();
            data.ghostAccuracy = stats.getAccuracy();
        }
        SaveGame();
    }
    public void SaveGame()
    {
        BinaryFormatter bf = new BinaryFormatter(); 
        FileStream file = File.Create(Application.persistentDataPath 
                    + "/MySaveData.dat"); 
        /*SaveData data = new SaveData();
        data.savedInt = intToSave;
        data.savedFloat = floatToSave;
        data.savedBool = boolToSave;*/
        bf.Serialize(file, data);
        file.Close();
        Debug.Log("Game data saved!");
    }
    public bool LoadGame()
    {
        if (File.Exists(Application.persistentDataPath 
                    + "/MySaveData.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = 
                    File.Open(Application.persistentDataPath 
                    + "/MySaveData.dat", FileMode.Open);
            data = (SaveData)bf.Deserialize(file);
            file.Close();
            /*intToSave = data.savedInt;
            floatToSave = data.savedFloat;
            boolToSave = data.savedBool;*/
            Debug.Log("Game data loaded!");
            return true;
        }
        else
        {
            Debug.LogError("There is no save data!");
            return false;
        }
    }
    public void ResetData()
    {
        if (File.Exists(Application.persistentDataPath 
                    + "/MySaveData.dat"))
        {
            File.Delete(Application.persistentDataPath 
                            + "/MySaveData.dat");
            //data = new SaveData();
            Debug.Log("Data reset complete!");
        }
        else
            Debug.LogError("No save data to delete.");
    }
    public int getHighScore()
    {
        return data.highScore;
    }
}

[Serializable]
class SaveData
{
    public int highScore;
    public float ghostAccuracy;

    public SaveData()
    {
        highScore = 0;
        ghostAccuracy = 0f;
    }
}