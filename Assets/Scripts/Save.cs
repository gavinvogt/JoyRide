using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class Save : MonoBehaviour
{
    private static string saveFilePath = Application.persistentDataPath + "/saveFile.dat";

    public static void SaveFile(SaveData data)
    {
        FileStream file;

        if (File.Exists(saveFilePath)) file = File.OpenWrite(saveFilePath);
        else file = File.Create(saveFilePath);

        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(file, data);
        file.Close();
    }

    public static SaveData LoadFile()
    {
        FileStream file;

        if (File.Exists(saveFilePath)) file = File.OpenRead(saveFilePath);
        else
            return null;

        BinaryFormatter bf = new BinaryFormatter();
        SaveData data = (SaveData)bf.Deserialize(file);
        file.Close();

        return data;
    }

}