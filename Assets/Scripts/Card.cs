using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public int id; // Уникальный идентификатор карты
    public Image image; // Ссылка на компонент Image
    public Button button; // Ссылка на компонент Button
    public Sprite backSprite; // Спрайт рубашки карты
    public Sprite frontSprite; // Спрайт, который нужно сопоставить

    private void Awake()
    {
        // Получаем компоненты Image и Button, если они не назначены в инспекторе
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
        // Установить начальный спрайт на рубашку
        image.sprite = backSprite;
        button.onClick.AddListener(OnCardClicked);
    }

    public void OnCardClicked()
    {
        GameManager.Instance.CardSelected(this);
    }

    public void ShowCard()
    {
        image.sprite = frontSprite; // Установите спрайт карты
        button.interactable = false; // Деактивируйте кнопку после открытия
        // Убедитесь, что цвет не изменен
        Color color = image.color;
        color.a = 1f; // Установите альфа-канал в 1 (непрозрачный)
        image.color = color;
    }

    public void HideCard()
    {
        image.sprite = backSprite; // Вернуть рубашку карты
        button.interactable = true; // Активируйте кнопку для выбора
        // Убедитесь, что цвет не изменен
        Color color = image.color;
        color.a = 1f; // Установите альфа-канал в 1 (непрозрачный)
        image.color = color;
    }
}