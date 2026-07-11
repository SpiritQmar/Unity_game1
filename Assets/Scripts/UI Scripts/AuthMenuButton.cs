using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AuthenticationButtons : MonoBehaviour
{
    public Button loginButton;
    public Button registerButton;

    void Start()
    {
        if (loginButton != null)
        {
            loginButton.onClick.AddListener(OnLoginButtonClicked);
        }
        
        if (registerButton != null)
        {
            registerButton.onClick.AddListener(OnRegisterButtonClicked);
        }
    }

    void OnLoginButtonClicked()
    {
        SceneManager.LoadScene("LoginScene");
    }

    void OnRegisterButtonClicked()
    {
        SceneManager.LoadScene("RegistrationMenu");
    }
}