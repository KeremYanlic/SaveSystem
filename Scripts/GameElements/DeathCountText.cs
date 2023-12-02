using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DeathCountText : MonoBehaviour,IDataPersistance
{
    private int deathCount = 0;

    private TextMeshProUGUI deathCountText;

    private void Awake()
    {
        deathCountText = this.GetComponent<TextMeshProUGUI>();
    }
    private void Start()
    {
        GameEventsManager.Instance.OnPlayerDeath += OnPlayerDeath;
    }
    private void OnDestroy()
    {
        GameEventsManager.Instance.OnPlayerDeath -= OnPlayerDeath;

    }
    private void Update()
    {
        deathCountText.text = deathCount.ToString();
    }
    private void OnPlayerDeath(GameEventsManager obj)
    {
        deathCount++;
    }
    public void SaveData(ref GameData data)
    {
        data.deathCount = this.deathCount;
    }
    public void LoadData(GameData data)
    {
        this.deathCount = data.deathCount;
    }

  
}
