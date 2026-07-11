using UnityEngine;
using UnityEngine.UI;

public class ProfileImageSelector : MonoBehaviour
{
    public Image profileIcon;
    public string[] availableIcons;

    public void SetProfileIcon(string iconName)
    {
        Sprite profileSprite = Resources.Load<Sprite>("ProfileIcons/" + iconName);

        if (profileSprite != null)
        {
            profileIcon.sprite = profileSprite;
            PlayerPrefs.SetString("ProfileIcon", iconName);
            PlayerPrefs.Save();
        }
        else
        {
            Debug.LogError("Icon not found: " + iconName);
        }
    }

    public void LoadSavedProfileIcon()
    {
        string iconName = PlayerPrefs.GetString("ProfileIcon", "default_icon");

        Sprite profileSprite = Resources.Load<Sprite>("ProfileIcons/" + iconName);

        if (profileSprite != null)
        {
            profileIcon.sprite = profileSprite;
        }
        else
        {
            Debug.LogError("Saved icon not found: " + iconName);
        }
    }

    void Start()
    {
        LoadSavedProfileIcon();
    }
}