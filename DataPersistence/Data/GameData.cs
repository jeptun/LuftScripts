using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData 
{

    public int deathCount;
    public Vector3 playerPosition;
    public float saveGas;
    public SerializableDictionary<string, bool> coinsCollected;

    public GameData()
    {
        this.deathCount = 0;
        playerPosition = Vector3.zero;
        this.saveGas = 10;
        coinsCollected = new SerializableDictionary<string, bool>();
    }

}
