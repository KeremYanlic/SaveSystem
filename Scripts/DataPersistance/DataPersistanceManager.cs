using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class DataPersistanceManager : MonoBehaviour
{
    public static DataPersistanceManager Instance { get; private set; }

    [Header("File Storage Config")]
    [SerializeField] private string fileName;
    private static readonly string dataPath = @"path";

    private List<IDataPersistance> dataPersistanceObjects;
    private FileDataHandler fileDataHandler;
    private GameData gameData;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Found more than one Data Persistance Manager in the scene.");
        }
        Instance = this;
    }
    private void Start()
    {
        this.fileDataHandler = new FileDataHandler(dataPath,fileName,true);
        this.dataPersistanceObjects = FindAllDataPersistanceObjects();
        LoadGame();
    }
    public void NewGame()
    {
        this.gameData = new GameData();
    }
    public void LoadGame()
    {
        // Load any saved data from a file using the data handler
        this.gameData = fileDataHandler.Load();

        // if no data can be loaded, initialize to a new game
        if(this.gameData == null)
        {
            Debug.Log("No data was found. Initializing data to defaults.");
            NewGame();
        }

        // push the loaded data to all other scripts that need it
        foreach(IDataPersistance dataPersistanceObj in dataPersistanceObjects)
        {
            dataPersistanceObj.LoadData(gameData);
        }
    
    }
    public void SaveGame()
    {
        // pass the data to other scripts so they can update it
        foreach(IDataPersistance dataPersistance in dataPersistanceObjects)
        {
            dataPersistance.SaveData(ref gameData);
        }
   
        // save that data to a file using the data handler
        fileDataHandler.Save(gameData);
    }

    private List<IDataPersistance> FindAllDataPersistanceObjects()
    {
        IEnumerable<IDataPersistance> dataPersistanceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistance>();
        return new List<IDataPersistance>(dataPersistanceObjects);
    }
    private void OnApplicationQuit()
    {
        SaveGame();
    }
}
