using System.Collections.Generic;
using UnityEngine;

public class ShuffleCards : MonoBehaviour
{
    public Transform grid;

    void Start()
    {
        Shuffle();
    }

    void Shuffle()
    {
        List<Transform> cards = new List<Transform>();

        foreach (Transform card in grid)
        {
            cards.Add(card);
        }

        for (int i = 0; i < cards.Count; i++)
        {
            Transform temp = cards[i];
            int randomIndex = Random.Range(i, cards.Count);
            cards[i] = cards[randomIndex];
            cards[randomIndex] = temp;
        }

        for (int i = 0; i < cards.Count; i++)
        {
            cards[i].SetSiblingIndex(i);
        }
    }
}