using TMPro;
using UnityEngine;

public class LeaderboardUI : MonoBehaviour
{
    [SerializeField] private TMP_Text leaderboardText;
    private Web web;

    void Start()
    {
        web = FindObjectOfType<Web>();
        LoadLeaderboard();
    }

    void LoadLeaderboard()
    {
        StartCoroutine(web.GetLeaderboard(
            onSuccess: (json) =>
            {
                var leaderboard = JsonUtility.FromJson<Leaderboard>(json);
                DisplayLeaderboard(leaderboard);
            },
            onFailure: (error) =>
            {
                Debug.LogError("Ошибка загрузки таблицы лидеров: " + error);
                leaderboardText.text = "Ошибка загрузки.";
            }
        ));
    }

    void DisplayLeaderboard(Leaderboard leaderboard)
    {
        leaderboardText.text = "Таблица лидеров:\n";
        foreach (var entry in leaderboard.entries)
        {
            leaderboardText.text += $"{entry.username}: {entry.score} очков\n";
        }
    }
}

[System.Serializable]
public class Leaderboard
{
    public LeaderboardEntry[] entries;
}

[System.Serializable]
public class LeaderboardEntry
{
    public string username;
    public int score;
}