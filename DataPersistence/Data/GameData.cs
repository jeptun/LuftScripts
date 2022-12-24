using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class GameData 
{
    public long lastUpdated;
    public int deathCount;
    public Vector3 playerPosition;
    public float saveGas;
    public SerializableDictionary<string, bool> coinsCollected;
    public string currentSceneName;

    public GameData()
    {
        this.deathCount = 0;
        playerPosition = Vector3.zero;
        this.saveGas = 10;
        currentSceneName = "";
        coinsCollected = new SerializableDictionary<string, bool>();
    }

    public int GetPercentageComplete()
    {
        // figure out how many coins we've collected
        int totalCollected = 0;
        foreach (bool collected in coinsCollected.Values)
        {
            if (collected)
            {
                totalCollected++;
            }
        }

        // ensure we don't divide by 0 when calculating the percentage
        int percentageCompleted = -1;
        if (coinsCollected.Count != 0)
        {
            percentageCompleted = (totalCollected * 100 / coinsCollected.Count);
        }
        return percentageCompleted;
    }
}
