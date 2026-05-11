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

    public delegate void TimerEvent();
    [SerializeField] private TMP_Text timerText, besTimetext;
    [SerializeField] private string bestTimeKey = "BestTimeLVL1";

        
    private void OnEnable()
    {
        StartGate.StartRace += StartRace;
        FinishGate.FinishRace += FinishRace;
        SlalomFlag.RacePenalty += AddRacePenalty;
    }
    private void Start()
    {
        int bestTimeInt = PlayerPrefs.GetInt(bestTimeKey,int.MaxValue);
        bestTime = new TimeSpan((long)bestTimeInt);
        bestTimetext.text = "BEST TIME" + raceTime.ToString("mm\\:ss");
    }
    void AddRacePenalty()
    {
        PenaltyTime += new TimeSpan(0, 0, 3);
    }

    void StartRace()
    {
        racing = true;
        raceStart = DateTime.Now;
        Debug.Log("starting race from game manager");
    }

    void FinishRace()
    {
        racing = false;
        if (raceTime < bestTime)
        {
            PlayerPrefs.SetInt(bestTimeKey, (int)raceTime.Ticks);
            PlayerPrefs.Save();
        }
    }
    void Update()
    {
        if (racing)
            raceTime = DateTime.Now - raceStart + penaltyTime;
        timerText.text = "TIME" + raceTime.ToString("mm\\:ss");
        raceTime

    }
}