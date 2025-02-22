using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class Save : MonoBehaviour
{
    private static string saveFilePath = Application.persistentDataPath + "/saveFile.dat";
    public static SaveData globalSaveData;

    public static void SaveFile()
    {
        FileStream file;

        if (File.Exists(saveFilePath)) file = File.OpenWrite(saveFilePath);
        else file = File.Create(saveFilePath);

        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(file, globalSaveData);
        file.Close();
    }

    public static void LoadFile()
    {
        FileStream file;

        if (File.Exists(saveFilePath)) file = File.OpenRead(saveFilePath);
        else
        {
            globalSaveData = new SaveData();
            return;
        }

        BinaryFormatter bf = new BinaryFormatter();
        globalSaveData = (SaveData)bf.Deserialize(file);
        ValidateGlobalSaveData();
        file.Close();
    }

    private static void ValidateGlobalSaveData()
    {
        SaveData defaultData = new SaveData();

        if(globalSaveData.volumeValues == null)
            globalSaveData.volumeValues = defaultData.volumeValues;
        if (globalSaveData.carsUnlocked == null)
        {
            Debug.Log("true");
            globalSaveData.carsUnlocked = defaultData.carsUnlocked;
            Debug.Log(globalSaveData.carsUnlocked.ToString());
        }
    }
}