using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public UserAuth userAuth;
    public InputField usernameInput;
    public InputField passwordInput;
    public InputField emailInput;

    public void OnRegisterButton()
    {
        string username = usernameInput.text;
        string password = passwordInput.text;
        string email = emailInput.text;

        bool isRegistered = userAuth.Register(username, password, email);
        Debug.Log(isRegistered ? "User registered successfully." : "Registration failed.");
    }

    public void OnLoginButton()
    {
        string username = usernameInput.text;
        string password = passwordInput.text;

        bool isLoggedIn = userAuth.Login(username, password);
        Debug.Log(isLoggedIn ? "Login successful." : "Login failed.");
    }
}