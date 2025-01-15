using System.Collections.Generic;
[System.Serializable]
public class SaveData
{
    public Dictionary<string, float> volumeValues;

    public SaveData(Dictionary<string, float> volumes)
    {
        volumeValues = volumes;
    }
}
