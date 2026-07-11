using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text scoreText;
    private int score = 0;
    private Card firstCard;
    private Card secondCard;

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
        yield return new WaitForSeconds(1);

        if (firstCard.id == secondCard.id)
        {
            score += 10;
            scoreText.text = "Score: " + score;
        }
        else
        {
        }

        firstCard = null;
        secondCard = null;
    }
}