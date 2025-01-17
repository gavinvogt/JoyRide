using System.Collections.Generic;
[System.Serializable]
public class SaveData
{
    public Dictionary<string, float> volumeValues;
    public float highScore;
    public string longestTimeAlive;

    public void SetVolumeValues(Dictionary<string, float> volumes)
    {
        volumeValues = volumes;
        Save.SaveFile();
    }

    public Dictionary<string, float> GetVolumeValues()
    {
        return volumeValues;
    }

    public void AttemptSetHighScore(float score)
    {
        highScore = score;
    }

    public void AttemptSetLongestTimeAlive(string timeAlive)
    {
        longestTimeAlive = timeAlive;
    }
}
