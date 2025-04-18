using System.Collections.Generic;
using System.Linq;
using static Car;

[System.Serializable]
public class SaveData
{
    public Dictionary<string, float> volumeValues;
    public List<CarSaveData> carsUnlocked;
    public float highScore;
    public string longestTimeAlive;
    public int numberOfCoins;

    //Default constructor, this is what will load if the user does not have any save data
    public SaveData()
    {
        volumeValues = new(){
            { "Master_Volume" , 1.0f },
            { "SoundFX_Volume" , 1.0f },
            { "Music_Volume" , 1.0f },
        };
        carsUnlocked = new(){
            new CarSaveData(CarType.SPORTS_CAR),
            new CarSaveData(CarType.SHOTGUN_TRUCK),
            new CarSaveData(CarType.TANK),
        };
        highScore = 0.0f;
        longestTimeAlive = "00:00.0";
        numberOfCoins = 0;
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

    public int GetAmountOfCoins()
    {
        return numberOfCoins;
    }

    public bool AttemptToSpendCoins(int amount)
    {
        if (numberOfCoins >= amount)
        {
            numberOfCoins -= amount;
            return true;
        }
        return false;
    }

    public CarSaveData GetSaveDataByCarType(CarType carType)
    {
        return carsUnlocked.FirstOrDefault(save => carType == save.GetCarType());
    }


    [System.Serializable]
    public class CarSaveData
    {
        private CarType carType;
        private bool abilityUnlocked;
        private bool isSelected;
        private int speedUpgradeLevel;
        private int healthUpgradeLevel;
        private int ammoUpgradeLevel;
        private int damageUpgradeLevel;
        private int attackRateUpgradeLevel;
        private int attackSpreadUpgradeLevel;

        public CarSaveData(CarType carType)
        {
            this.carType = carType;
            isSelected = true;
            abilityUnlocked = true;
            speedUpgradeLevel = 0;
            healthUpgradeLevel = 0;
            ammoUpgradeLevel = 0;
            damageUpgradeLevel = 0;
            attackRateUpgradeLevel = 0;
            attackSpreadUpgradeLevel = 0;
        }

        public CarType GetCarType()
        {
            return carType;
        }
        public bool GetAbilityUnlocked()
        {
            return abilityUnlocked;
        }
        public void SetAbilityUnlocked(bool unlocked)
        {
            abilityUnlocked = unlocked;
        }
        public bool GetIsSelected()
        {
            return isSelected;
        }
        public void SetIsSelected(bool selected)
        {
            isSelected = selected;
        }
        public int GetSpeedUpgradeLevel()
        {
            return speedUpgradeLevel;
        }
        public int GetHealthUpgradeLevel()
        {
            return healthUpgradeLevel;
        }
        public int GetAmmoUpgradeLevel()
        {
            return ammoUpgradeLevel;
        }
        public int GetDamageUpgradeLevel()
        {
            return damageUpgradeLevel;
        }
        public int GetAttackRateUpgradeLevel()
        {
            return attackRateUpgradeLevel;
        }
        public int GetAttackSpreadUpgradeLevel()
        {
            return attackSpreadUpgradeLevel;
        }
    }
}
