using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoginUI : MonoBehaviour
{
    [SerializeField] private TMP_InputField usernameInput;
    [SerializeField] private TMP_InputField passwordInput;
    [SerializeField] private Button loginButton;
    [SerializeField] private TMP_Text logText;
    private Web webScript;

    void Start()
    {
        webScript = GetComponent<Web>();
        loginButton.onClick.AddListener(OnLoginButtonClicked);
    }

    void OnLoginButtonClicked()
    {
        string username = usernameInput.text;
        string password = passwordInput.text;

        if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
        {
            logText.text = "Выполняется вход...";
            StartCoroutine(webScript.Login(username, password,
                onSuccess: () =>
                {
                    logText.text = "Логин успешный";
                    PlayerPrefs.SetString("CurrentPlayer", username);
                    PlayerPrefs.Save();
                },
                onFailure: (error) => logText.text = "Ошибка: " + error));
        }
        else
        {
            logText.text = "Заполните данные";
        }
    }
}