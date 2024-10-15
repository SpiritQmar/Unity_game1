using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement; // Добавьте эту строку

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public List<Card> cards = new List<Card>(); // Список всех карт
    private Card firstCard, secondCard;
    private int pairsFound = 0;

    public TMP_Text scoreText; // Поле для отображения очков
    public TMP_Text timeText; // Поле для отображения оставшегося времени
    public TMP_Text movesText; // Поле для отображения оставшихся ходов

    private int score = 0; // Переменная для хранения текущего количества очков
    private float timeLimit = 60f; // Ограничение времени (в секундах)
    private int maxMoves = 20; // Максимальное количество ходов
    private int currentMoves = 0; // Текущее количество использованных ходов
    private bool gameOver = false; // Флаг окончания игры

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        ShuffleCards();
        UpdateScoreText(); // Обновите текст очков при старте игры
        StartCoroutine(TimeCountdown()); // Запускаем обратный отсчет времени
        UpdateMovesText(); // Обновляем текст ходов
    }

    private IEnumerator TimeCountdown()
    {
        while (timeLimit > 0 && !gameOver)
        {
            yield return new WaitForSeconds(1f);
            timeLimit--;
            UpdateTimeText(); // Обновляем текст времени
        }

        if (!gameOver) // Если игра не закончена
        {
            EndGame("Время вышло!");
        }
    }

    public void CardSelected(Card card)
    {
        if (firstCard == null)
        {
            firstCard = card;
            firstCard.ShowCard();
        }
        else if (secondCard == null && card != firstCard)
        {
            secondCard = card;
            secondCard.ShowCard();
            currentMoves++; // Увеличиваем количество ходов
            UpdateMovesText(); // Обновляем текст ходов

            StartCoroutine(CheckForMatch());
        }
    }

    private IEnumerator CheckForMatch()
    {
        yield return new WaitForSeconds(1f);

        if (firstCard.id == secondCard.id)
        {
            pairsFound++;
            score += 10; // Увеличьте очки за нахождение пары
            UpdateScoreText(); // Обновите текст очков
            StartCoroutine(AnimateScoreText("+10")); // Запустите анимацию текста
            // Логика для обработки нахождения пары, например, удалить карточки из игры
        }
        else
        {
            firstCard.HideCard();
            secondCard.HideCard();
        }

        firstCard = null;
        secondCard = null;

        // Проверяем, закончилась ли игра по количеству ходов
        if (currentMoves >= maxMoves)
        {
            EndGame("Вы исчерпали все ходы!");
        }
    }

    private void EndGame(string message)
    {
        gameOver = true; // Устанавливаем флаг окончания игры
        Debug.Log(message); // Временное сообщение в консоль
        
        // Замените "GameOverScene" на имя вашей сцены окончания игры
        SceneManager.LoadScene("StartMenu");
    }

    private void UpdateScoreText()
    {
        scoreText.text = "Очки: " + score; // Обновите текст очков
    }

    private void UpdateTimeText()
    {
        timeText.text = "Осталось времени: " + timeLimit; // Обновите текст времени
    }

    private void UpdateMovesText()
    {
        movesText.text = "Ходы: " + (maxMoves - currentMoves); // Обновите текст ходов
    }

    private IEnumerator AnimateScoreText(string additionalScoreText)
    {
        TMP_Text tempText = Instantiate(scoreText, scoreText.transform.parent);
        tempText.text = additionalScoreText;
        tempText.fontSize = 36; 
        tempText.color = Color.green; 
        tempText.transform.position = scoreText.transform.position; 

        float duration = 1f;
        float elapsedTime = 0f;
        Vector3 initialPosition = tempText.transform.position;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;

            tempText.transform.position = initialPosition + new Vector3(0, t * 50, 0);
            tempText.color = new Color(tempText.color.r, tempText.color.g, tempText.color.b, 1 - t);
            yield return null;
        }

        Destroy(tempText.gameObject); 
    }

    private void ShuffleCards()
    {
        for (int i = 0; i < cards.Count; i++)
        {
            Card temp = cards[i];
            int randomIndex = Random.Range(0, cards.Count);
            cards[i] = cards[randomIndex];
            cards[randomIndex] = temp;
        }
    }
}
