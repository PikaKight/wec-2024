using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class LeaderboardManager : MonoBehaviour
{
    public int maxEntries = 10;
    public Transform leaderboardContainer;
    public GameObject leaderboard;

    List<LeaderboardEntry> entries = new List<LeaderboardEntry>();

    private void Start()
    {
        LoadLeaderboard();

        if (PlayerPrefs.HasKey("CurrentPlayerName") && PlayerPrefs.HasKey("CurrentPlayerScore"))
        {
            string playerName = PlayerPrefs.GetString("CurrentPlayerName");
            float score = PlayerPrefs.GetFloat("CurrentPlayerScore");

            AddEntry(playerName, score);

            PlayerPrefs.DeleteKey("CurrentPlayerName");
            PlayerPrefs.DeleteKey("CurrentPlayerScore");
        }

        DisplayLeaderboard();
    }

    void AddEntry(string playerName, float score)
    {
        LeaderboardEntry newEntry = new LeaderboardEntry(playerName, score);
        entries.Add(newEntry);

        entries.OrderBy(entry => entry.score).ToList();

        if (entries.Count > maxEntries) {
            entries.RemoveAt(entries.Count - 1);
        }

        SaveLeaderboard();
        DisplayLeaderboard();
    }

    void SaveLeaderboard()
    {
        for (int i = 0; i < entries.Count; i++)
        {
            PlayerPrefs.SetString($"Leaderboard_Name_{i}", entries[i].playerName);
            PlayerPrefs.SetFloat($"Leaderboard_Score_{i}", entries[i].score);
        }

        PlayerPrefs.SetInt("Leaderboard_Count", entries.Count);
    }

    void LoadLeaderboard()
    {
        entries.Clear();

        int count = PlayerPrefs.GetInt("Leaderboard_Count", 0);

        for (int i = 0; i < count; i++) {
            string name = PlayerPrefs.GetString($"Leaderboard_Name_{i}", "Unknown");
            float score = PlayerPrefs.GetFloat($"Leaderboard_Score_{i}", 0.0f);
            entries.Add(new LeaderboardEntry(name, score));
        }
    }

    void DisplayLeaderboard()
    {
        foreach (Transform child in leaderboardContainer)
        {
            Destroy(child.gameObject);
        }

        foreach (var entry in entries) {
            GameObject playerEntry = Instantiate(leaderboard, leaderboardContainer);
            playerEntry.GetComponentInChildren<TextMeshProUGUI>().text = $"{entry.playerName}: {entry.score}";
        }
    } 
}

public class LeaderboardEntry 
{
    public string playerName;
    public float score;

    public LeaderboardEntry(string playerName, float score)
    {
        this.playerName = playerName;
        this.score = score;
    }
}
