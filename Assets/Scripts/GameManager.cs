using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public List<Card> cards = new List<Card>();
    private Card firstCard, secondCard;
    private int pairsFound = 0;

    public TMP_Text scoreText;
    public TMP_Text timeText;
    public TMP_Text movesText;
    public TMP_Text gameOverText;
    public GameObject gameOverPanel;
    public CanvasGroup gameOverCanvasGroup;

    private int score = 0;
    private float timeLimit = 70f;
    private int maxMoves = 50;
    private int currentMoves = 0;
    private bool gameOver = false;

    private void Awake()
    {
        Instance = this;
    }

private void Start()
{
    SetGameParametersForCurrentScene();
    InitializeCards();
    ShuffleCards();
    UpdateScoreText();
    StartCoroutine(TimeCountdown());
    UpdateMovesText();
    gameOverPanel.SetActive(false);
}


private void SetGameParametersForCurrentScene()
{
    int sceneIndex = SceneManager.GetActiveScene().buildIndex;

    switch (sceneIndex)
    {
        case 5:
            timeLimit = 30f;
            maxMoves = 30;
            break;
        case 6:
            timeLimit = 120f;
            maxMoves = 90;
            break;
        default:
            timeLimit = 70f;
            maxMoves = 50;
            break;
    }
}


private void InitializeCards()
{
    cards.Clear();

    int numberOfPairs = 9;
    switch (SceneManager.GetActiveScene().buildIndex)
    {
        case 5:
            numberOfPairs = 9;
            break;
        case 6:
            numberOfPairs = 16;
            break;
        default:
            numberOfPairs = 9;
            break;
    }

    for (int i = 0; i < numberOfPairs; i++) 
    {
        Card card1 = CreateCard(i);
        Card card2 = CreateCard(i);
        cards.Add(card1);
        cards.Add(card2);
    }
}


    private Card CreateCard(int id)
    {
        Card newCard = new Card();
        newCard.id = id;
        return newCard;
    }

    private void CheckGameOver()
    {
        Debug.Log("Пары найдены: " + pairsFound + " из " + (cards.Count / 2));
        if (pairsFound == cards.Count / 2)
        {
            Debug.Log("Все пары найдены! Завершаем игру.");
            EndGame("Вы нашли все пары!");
        }
    }

    private IEnumerator TimeCountdown()
    {
        while (timeLimit > 0 && !gameOver)
        {
            yield return new WaitForSeconds(1f);
            timeLimit--;
            UpdateTimeText();
        }

        if (!gameOver)
        {
            EndGame("Время вышло!");
        }
    }

    public void CardSelected(Card card)
    {
        if (firstCard != null && secondCard != null)
            return;

        if (firstCard == null)
        {
            firstCard = card;
            firstCard.ShowCard();
        }
        else if (secondCard == null && card != firstCard)
        {
            secondCard = card;
            secondCard.ShowCard();
            currentMoves++;
            UpdateMovesText();

            if (currentMoves >= maxMoves)
            {
                EndGame("Ходы закончились!");
            }
            else
            {
                StartCoroutine(CheckForMatch());
            }
        }
    }

    private IEnumerator CheckForMatch()
    {
        yield return new WaitForSeconds(1f);

        if (firstCard.id == secondCard.id)
        {
            pairsFound++;
            score += 10;
            UpdateScoreText();
            StartCoroutine(AnimateScoreText("+10"));

            CheckGameOver();
        }
        else
        {
            firstCard.HideCard();
            secondCard.HideCard();
        }

        firstCard = null;
        secondCard = null;
    }

    private void EndGame(string message)
    {
        gameOver = true;
        gameOverText.text = message;
        gameOverPanel.SetActive(true);

        StartCoroutine(AnimateGameOverPanel());
        StartCoroutine(SavePlayerScore());
        StartCoroutine(DelaySceneChange());
    }

    private IEnumerator DelaySceneChange()
    {
        yield return new WaitForSeconds(4f);

        SceneManager.LoadScene("StartMenu");
    }

    private IEnumerator AnimateGameOverPanel()
    {
        float duration = 1f;
        float elapsedTime = 0f;

        gameOverCanvasGroup.alpha = 0;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            gameOverCanvasGroup.alpha = Mathf.Lerp(0, 1, elapsedTime / duration);
            yield return null;
        }
    }

    private IEnumerator SavePlayerScore()
    {
        string currentPlayer = PlayerPrefs.GetString("CurrentPlayer", "Guest");
        int currentScore = score;

        Web web = FindObjectOfType<Web>();
        if (web == null)
        {
            Debug.LogError("Web script not found in the scene!");
            yield break;
        }

        yield return web.SaveScore(currentPlayer, currentScore,
            onSuccess: () => Debug.Log("Очки успешно сохранены."),
            onFailure: (error) => Debug.LogError("Ошибка сохранения очков: " + error));
    }

    private void UpdateScoreText()
    {
        scoreText.text = "Очки: " + score;
    }

    private void UpdateTimeText()
    {
        timeText.text = "Осталось времени: " + Mathf.CeilToInt(timeLimit);
    }

    private void UpdateMovesText()
    {
        movesText.text = "Ходы: " + (maxMoves - currentMoves);
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
