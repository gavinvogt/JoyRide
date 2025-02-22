using System.Collections.Generic;

[System.Serializable]
public class SaveData
{
    public Dictionary<string, float> volumeValues;
    public List<CarSaveData> carsUnlocked;
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
        carsUnlocked = new(){
            new CarSaveData("sportsCar"),
            new CarSaveData("truck"),
            new CarSaveData("tank"),
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

    public class CarSaveData
    {
        private string carName;
        private bool unlocked;
        private bool abilityUnlocked;
        private int speedUpgradeLevel;
        private int healthUpgradeLevel;

        public CarSaveData(string carType)
        {
            carName = carType;
            unlocked = true;
            abilityUnlocked = false;
            speedUpgradeLevel = 0;
            healthUpgradeLevel = 0;
        }

        public string GetName()
        {
            return carName;
        }
    }
}
