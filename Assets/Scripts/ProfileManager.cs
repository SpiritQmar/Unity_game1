using UnityEngine;

public class ProfileManager : MonoBehaviour
{
    private string currentPlayer;

    void Start()
    {
        currentPlayer = PlayerPrefs.GetString("CurrentPlayer", "");

        if (string.IsNullOrEmpty(currentPlayer))
        {
            Debug.LogError("Пользователь не авторизован или имя отсутствует.");
            return;
        }

        LoadProfileData();
    }

    void LoadProfileData()
    {
        if (string.IsNullOrEmpty(currentPlayer))
        {
            Debug.LogError("Не удалось загрузить профиль: имя пользователя отсутствует.");
            return;
        }

        string playerProfile = PlayerPrefs.GetString("Profile_" + currentPlayer, "");
        if (string.IsNullOrEmpty(playerProfile))
        {
            Debug.Log("Для нового пользователя создаём профиль...");
            CreateNewProfile();
        }
        else
        {
            Debug.Log("Профиль найден, данные загружены.");
        }
    }

    void CreateNewProfile()
    {
        PlayerPrefs.SetString("Profile_" + currentPlayer, "New Profile Data");
        PlayerPrefs.SetInt("Score_" + currentPlayer, 0);
        PlayerPrefs.Save();

        Debug.Log("Новый профиль для пользователя " + currentPlayer + " создан.");
    }
}