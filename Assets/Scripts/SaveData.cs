using System.Collections.Generic;

[System.Serializable]
public class SaveData
{
    public Dictionary<string, float> volumeValues;
    public float highScore;
    public string longestTimeAlive;

    //Default constructor, this is what will load if the user does not have any save data
    public SaveData()
    {
        volumeValues = new(){
            { "Master_Volume" , 1.0f },
            { "SoundFX_Volume" , 1.0f },
            { "Music_Volume" , 1.0f },
        };
         highScore = 0.0f;
         longestTimeAlive = "00:00.0";
    }

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
        if (score > highScore)
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
