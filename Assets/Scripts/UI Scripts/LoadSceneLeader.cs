using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonSceneLoader : MonoBehaviour
{
    [Header("Настройки кнопки")]
    [SerializeField] private Button button;
    [SerializeField] private string sceneName;

    private void Awake()
    {
        if (button == null)
        {
            Debug.LogError("Кнопка не назначена в " + gameObject.name);
            return;
        }

        button.onClick.AddListener(LoadScene);
    }

    private void LoadScene()
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogError("Имя сцены не указано для кнопки в " + gameObject.name);
        }
    }

    private void OnDestroy()
    {
        if (button != null)
        {
            button.onClick.RemoveListener(LoadScene);
        }
    }
}