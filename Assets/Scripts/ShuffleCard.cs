using System.Collections.Generic;
using UnityEngine;

public class ShuffleCards : MonoBehaviour
{
    public Transform grid; // Ссылка на родительский объект (Grid), который содержит карты

    void Start()
    {
        Shuffle();
    }

    void Shuffle()
    {
        // Получаем всех детей (карты) из Grid
        List<Transform> cards = new List<Transform>();

        foreach (Transform card in grid)
        {
            cards.Add(card);
        }

        // Перемешиваем список карт
        for (int i = 0; i < cards.Count; i++)
        {
            Transform temp = cards[i];
            int randomIndex = Random.Range(i, cards.Count);
            cards[i] = cards[randomIndex];
            cards[randomIndex] = temp;
        }

        // Обновляем позиции карт в иерархии (внутри Grid)
        for (int i = 0; i < cards.Count; i++)
        {
            cards[i].SetSiblingIndex(i);
        }
    }
}