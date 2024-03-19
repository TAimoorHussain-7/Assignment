using UnityEngine;

namespace ProjectCore.Variables
{
    public class SpinWheelJson 
    {
        public SpinWheelData ParseJsonFile(TextAsset jsonFile)
        {
            SpinWheelData spData = JsonUtility.FromJson<SpinWheelData>(jsonFile.text);
            SpinWheelData jsonObj = new SpinWheelData();
            jsonObj.coins= spData.coins;
            if (spData.rewards != null)
            {
                jsonObj.rewards = new RewardData[spData.rewards.Length];
                for (int d = 0; d < spData.rewards.Length; d++)
                {
                    RewardData rewardItem = new RewardData();
                    rewardItem.multiplier = spData.rewards[d].multiplier;
                    rewardItem.probability = spData.rewards[d].probability;
                    rewardItem.color = spData.rewards[d].color;
                    jsonObj.rewards[d] = rewardItem;
                }
            }
            return jsonObj;
        }
    }

    [System.Serializable]
    public class SpinWheelData
    {
        public int coins;
        public RewardData[] rewards;
    }

    [System.Serializable]
    public class RewardData
    {
        public int multiplier;
        public float probability;
        public string color;
    }

    public enum RotateType
    {
        Normal,
        StartFast,
        StartSlow
    }
}
