using System;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;


public class GameManager : MonoBehaviour
{
    private DateTime raceStart;
    private TimeSpan raceTime;
    private TimeSpan penaltyTime;
    private bool racing = false;
    private TimeSpan bestTime;
    
    [SerializeField] private TMP_Text timerText, bestTimeText;
    [SerializeField] private string bestTimeKey = "BestTimeLVL1";

        
    
    private void OnEnable()
    {
        StartGate.StartRace += StartRace;
        FinishGate.FinishRace += FinishRace;
        SlalomFlag.RacePenalty += AddRacePenalty;
    }

    private void OnDisable()
    {
        StartGate.StartRace -= StartRace;
        FinishGate.FinishRace -= FinishRace;
        SlalomFlag.RacePenalty -= AddRacePenalty;
    }

    private void Start()
    {
        int bestTimeInt = PlayerPrefs.GetInt(bestTimeKey,int.MaxValue);
        bestTime = new TimeSpan((long)bestTimeInt);
        bestTimeText.text = "BEST TIME: " + bestTime.ToString("mm\\:ss");
    }
    void AddRacePenalty()
    {
        penaltyTime += new TimeSpan(0, 0, 1);
    }

    void StartRace()
    {
        racing = true;
        raceStart = DateTime.Now;
        Debug.Log("starting race from game manager");
    }
    void FinishRace()
    {
        Debug.Log("finishing race from game manager");
        racing = false;
        float finalTime = (float)raceTime.TotalSeconds;
        GameData.Instance.AddLevelTime(finalTime);
        FindObjectOfType<LeaderboardUI>()?.UpdateLeaderboard();
        if (raceTime < bestTime)
        {
            bestTimeText.text = "BEST TIME: " + raceTime.ToString("mm\\:ss");
            PlayerPrefs.SetInt(bestTimeKey, (int)raceTime.Ticks);
            PlayerPrefs.Save();
            UIManager.Instance.ShowFinalTime((float)raceTime.TotalSeconds);
        }
    } 
   
    void Update()
    {
        if (racing)
            raceTime = DateTime.Now - raceStart + penaltyTime;
        timerText.text = "TIME: " + raceTime.ToString("mm\\:ss");

    }
}