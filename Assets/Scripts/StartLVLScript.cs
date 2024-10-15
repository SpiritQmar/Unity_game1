using UnityEngine;
using UnityEngine.SceneManagement; // Не забудьте добавить эту строку

public class MainMenuController : MonoBehaviour
{
    public void StartGame()
    {
        // Замените "GameScene" на название вашей сцены с игрой
        SceneManager.LoadScene("LEVEL 1");
    }
	public void Home()
    {
        // Замените "GameScene" на название вашей сцены с игрой
        SceneManager.LoadScene("StartMenu");
    }
}
