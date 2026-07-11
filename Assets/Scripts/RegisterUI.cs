using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RegisterUI : MonoBehaviour
{
    [SerializeField] private TMP_InputField usernameInput;
    [SerializeField] private TMP_InputField passwordInput;
    [SerializeField] private Button registerButton;
    [SerializeField] private TMP_Text statusText;

    private Web web;

    void Start()
    {
        web = FindObjectOfType<Web>(); 

        if (registerButton != null)
        {
            registerButton.onClick.AddListener(OnRegisterButtonClicked);
        }

        if (statusText != null)
        {
            statusText.text = "";
        }
    }

    void OnRegisterButtonClicked()
    {
        string username = usernameInput.text;
        string password = passwordInput.text;

        if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
        {
            StartCoroutine(web.Register(username, password, OnRegistrationSuccess, OnRegistrationFailure));
        }
        else
        {
            Debug.LogError("Пароль или пользователь пуст!");
            if (statusText != null)
            {
                statusText.text = "Пароль и логин пуст!";
            }
        }
    }

    private void OnRegistrationSuccess()
    {
        Debug.Log("Registration Successful!");
        if (statusText != null)
        {
            statusText.text = "Registration Successful!";
        }
        SceneManager.LoadScene("LoginMenu");
    }

    private void OnRegistrationFailure(string errorMessage)
    {
        Debug.LogError("Регистрация провалена: " + errorMessage);
        if (statusText != null)
        {
            statusText.text = errorMessage;
        }
    }
}
