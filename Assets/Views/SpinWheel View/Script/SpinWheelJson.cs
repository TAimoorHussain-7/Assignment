using UnityEngine;

namespace ProjectCore.Variables
{
    public class SpinWheelJson 
    {
        public void ParseJsonFile(TextAsset jsonFile, SpinWheelData jsonObj)
        {
            SpinWheelData spData = JsonUtility.FromJson<SpinWheelData>(jsonFile.text);

            jsonObj.coins= spData.coins;

            for (int d =0; d<spData.rewards.Length; d++)
            {
                jsonObj.rewards[d].multiplier = spData.rewards[d].multiplier;
                jsonObj.rewards[d].probability = spData.rewards[d].probability;
                jsonObj.rewards[d].color = spData.rewards[d].color;
            }
        }
    }
    public class SpinWheelData
    {
        public int coins;
        public RewardData[] rewards;
    }

    public class RewardData
    {
        public int multiplier;
        public float probability;
        public string color;
    }
}
