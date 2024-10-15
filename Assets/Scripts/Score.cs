using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text scoreText; // Поле для отображения очков
    private int score = 0; // Переменная для хранения текущего количества очков
    private Card firstCard; // Первая выбранная карточка
    private Card secondCard; // Вторая выбранная карточка

    // Метод, который вызывается, когда карточка выбрана
    public void OnCardSelected(Card card)
    {
        if (firstCard == null)
        {
            firstCard = card;
        }
        else if (secondCard == null)
        {
            secondCard = card;
            StartCoroutine(CheckForMatch());
        }
    }

    private System.Collections.IEnumerator CheckForMatch()
    {
        yield return new WaitForSeconds(1); // Подождите секунду для визуального эффекта

        if (firstCard.id == secondCard.id) // Проверьте совпадение
        {
            score += 10; // Увеличьте очки
            scoreText.text = "Score: " + score; // Обновите текст очков
            // Добавьте здесь логику для уничтожения пары карточек, если нужно
        }
        else
        {
            // Добавьте здесь логику для скрытия карточек, если они не совпадают
        }

        firstCard = null; // Сбросьте первую карточку
        secondCard = null; // Сбросьте вторую карточку
    }
}