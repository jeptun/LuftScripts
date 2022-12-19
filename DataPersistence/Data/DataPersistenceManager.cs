using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataPersistenceManager : MonoBehaviour
{
    private GameData gameData;
    public static DataPersistenceManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Chyba v DataPersistenceManager");
        }
        instance = this;
    }

    private void Start()
    {
        LoadGame();
    }

    public void NewGame()
    {
        this.gameData = new GameData();

    }
    public void LoadGame()
    {
        Debug.Log("No data was found");
        NewGame();
    }
    // Todo posli ulozena data vsem scriptum co je potrebuji
    public void SaveGame()
    {
        //TODO  predat data dalsim scriptum aby je mohli updt. 

        //TODO ulozit data pomoci handleru
    }

    private void OnApplicationQuit()
    {
        SaveGame(); 
    }

}
