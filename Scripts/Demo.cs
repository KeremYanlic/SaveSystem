using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;
public class Demo : MonoBehaviour
{
    private PlayerStats playerStats = new PlayerStats();
    private IDataService dataService = new JsonDataService();

    private long SaveTime;
    private void Start()
    {
        SerializeJson();
    }
    public void SerializeJson()
    {
        long startTime = DateTime.Now.Ticks;
        if (dataService.SaveData("/player-stats.json", playerStats, false))
        {
            SaveTime = DateTime.Now.Ticks - startTime;
            Debug.Log(SaveTime / 10000f);
        }
    }
    
}

public class PlayerStats
{
    public string Name = "Name";
    public string LastName = "LastName";
    public string Nickname = "Nickname";
    public int age = 21;
    public float height = 1.80f;

    public Race race = Race.Dwarf;
    public CombatType combatType = CombatType.Shooter;
}

public enum Race
{
    Elves,
    Dwarf,
    Troll,
    Human,
    Dragon,
    Giant,
    Ork,
    Goblin
}
public enum CombatType
{
    Wizard,
    SwordUser,
    AxeUser,
    DaggerUser,
    Shooter,
    Sniper
}
