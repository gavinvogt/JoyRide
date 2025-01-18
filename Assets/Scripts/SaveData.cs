using System.Collections.Generic;
using UnityEngine;

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

    public float AttemptSetHighScore(float score)
    {
        if(score > highScore)
        {
            highScore = score;
            Save.SaveFile();
        }
        return highScore;
    }

    public string AttemptSetLongestTimeAlive(string timeAlive)
    {
        if (string.Compare(longestTimeAlive, timeAlive) < 0)
        {
            longestTimeAlive = timeAlive;
            Save.SaveFile();
        }
        return longestTimeAlive;
    }
}
