using UnityEngine;
using TMPro;

public class LeaderboardUI : MonoBehaviour
{
    [SerializeField] private TMP_Text[] entries;

    private void Start()
    {
        UpdateLeaderboard();
    }

    public void UpdateLeaderboard()
    {
        var times = GameData.Instance.bestTimes;

        for (int i = 0; i < entries.Length; i++)
        {
            if (i < times.Count)
                entries[i].text = (i + 1) + ". " + times[i].ToString("F2") + "s";
            else
                entries[i].text = (i + 1) + ". ---";
        }
    }
}
