using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public int id;
    public Image image;
    public Button button;
    public Sprite backSprite;
    public Sprite frontSprite;

    private void Awake()
    {
        if (image == null)
        {
            image = GetComponent<Image>();
        }

        if (button == null)
        {
            button = GetComponent<Button>();
        }
    }

    private void Start()
    {
        image.sprite = backSprite;
        button.onClick.AddListener(OnCardClicked);
    }

    public void OnCardClicked()
    {
        GameManager.Instance.CardSelected(this);
    }

    public void ShowCard()  
    {
        image.sprite = frontSprite;
        button.interactable = false;

        Color color = image.color;
        color.a = 1f;
        image.color = color;
    }

    public void HideCard()
    {
        image.sprite = backSprite;
        button.interactable = true;

        Color color = image.color;
        color.a = 1f;
        image.color = color;
    }
}