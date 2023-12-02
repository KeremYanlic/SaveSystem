using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class GameEventsManager : MonoBehaviour
{
    public static GameEventsManager Instance { get; private set; }

    public event Action<GameEventsManager> OnPlayerDeath;
    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            OnPlayerDeath?.Invoke(this);
        }
    }
}
