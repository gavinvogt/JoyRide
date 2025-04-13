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

        //Uncomment this line when we want to add the default data to the save file
        //SaveFile();
    }


    //Updates the save file with default values if certain values aren't present
    //This is useful if we add fields to the save file later and people have existing save files without those fields
    private static void ValidateGlobalSaveData()
    {
        SaveData defaultData = new SaveData();

        if(globalSaveData.volumeValues == null)
            globalSaveData.volumeValues = defaultData.volumeValues;
        if (globalSaveData.carsUnlocked == null)
        {
            globalSaveData.carsUnlocked = defaultData.carsUnlocked;
            Debug.Log("Loading in default cars");
        }
    }
}