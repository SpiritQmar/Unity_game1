using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class Web : MonoBehaviour
{
    public IEnumerator Register(string username, string password, System.Action onSuccess, System.Action<string> onFailure)
    {
        WWWForm form = new WWWForm();
        form.AddField("registerUser", username);
        form.AddField("registerPassword", password);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/unitybackend/Register.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                if (www.downloadHandler.text == "Registration Successful")
                {
                    onSuccess?.Invoke();
                }
                else
                {
                    onFailure?.Invoke(www.downloadHandler.text);
                }
            }
            else
            {
                onFailure?.Invoke("Request failed: " + www.error);
            }
        }
    }

    public IEnumerator Login(string username, string password, System.Action onSuccess, System.Action<string> onFailure)
    {
        WWWForm form = new WWWForm();
        form.AddField("loginUser", username);
        form.AddField("loginPassword", password);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/unitybackend/Login.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                if (www.downloadHandler.text == "Login Successful")
                {
                    onSuccess?.Invoke();
                    SceneManager.LoadScene("StartMenu");
                }
                else
                {
                    onFailure?.Invoke("Неверные данные");
                }
            }
            else
            {
                onFailure?.Invoke("Запрос провален: " + www.error);
            }
        }
    }

    public IEnumerator SaveScore(string username, int score, System.Action onSuccess, System.Action<string> onFailure)
{
    WWWForm form = new WWWForm();
    form.AddField("username", username);
    form.AddField("score", score);

    using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/unitybackend/SaveScore.php", form))
    {
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            if (www.downloadHandler.text == "Score saved" || www.downloadHandler.text == "Score updated")
            {
                onSuccess?.Invoke();
            }
            else
            {
                onFailure?.Invoke(www.downloadHandler.text);
            }
        }
        else
        {
            onFailure?.Invoke("Request failed: " + www.error);
        }
    }
}


    public IEnumerator GetLeaderboard(System.Action<string> onSuccess, System.Action<string> onFailure)
    {
        using (UnityWebRequest www = UnityWebRequest.Get("http://localhost/unitybackend/GetLeaderboard.php"))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                onSuccess?.Invoke(www.downloadHandler.text);
            }
            else
            {
                onFailure?.Invoke("Request failed: " + www.error);
            }
        }
    }

    public IEnumerator UpdateProfile(string username, string newInfo, System.Action onSuccess, System.Action<string> onFailure)
    {
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("newInfo", newInfo);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/unitybackend/UpdateProfile.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                if (www.downloadHandler.text == "Profile updated")
                {
                    onSuccess?.Invoke();
                }
                else
                {
                    onFailure?.Invoke(www.downloadHandler.text);
                }
            }
            else
            {
                onFailure?.Invoke("Request failed: " + www.error);
            }
        }
    }

    public IEnumerator GetProfile(string username, System.Action<string> onSuccess, System.Action<string> onFailure)
    {
        WWWForm form = new WWWForm();
        form.AddField("username", username);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/unitybackend/GetProfile.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                onSuccess?.Invoke(www.downloadHandler.text);
            }
            else
            {
                onFailure?.Invoke("Request failed: " + www.error);
            }
        }
    }
}
