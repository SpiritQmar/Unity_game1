using UnityEngine;
using System.Collections;

public class SpawnObjects : MonoBehaviour
{
    public GameObject prefabToSpawn; // Префаб, который будем создавать
    public BoxCollider2D spawnArea; // Ссылка на BoxCollider2D, в котором будут спавниться объекты
    public Transform background; // Ссылка на родительский объект (например, background)
    public float spawnIntervalMin = 3f; // Минимальный интервал времени для спавна
    public float spawnIntervalMax = 10f; // Максимальный интервал времени для спавна

    void Start()
    {
        // Запускаем цикл спавна
        StartCoroutine(SpawnObject());
    }

    IEnumerator SpawnObject()
    {
        while (true)
        {
            // Ожидаем случайное время перед спавном нового объекта
            float waitTime = Random.Range(spawnIntervalMin, spawnIntervalMax);
            yield return new WaitForSeconds(waitTime);

            // Генерируем случайную позицию внутри BoxCollider2D
            Vector3 spawnPosition = GetRandomPositionInCollider();

            // Создаем объект на случайной позиции внутри коллайдера с родителем (например, под background)
            GameObject spawnedObject = Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);

            // Устанавливаем родителя для объекта
            spawnedObject.transform.SetParent(background, false);
        }
    }

    // Функция для получения случайной позиции внутри BoxCollider2D
    private Vector3 GetRandomPositionInCollider()
    {
        // Получаем размеры и позицию коллайдера
        Bounds bounds = spawnArea.bounds;

        // Генерируем случайную позицию внутри границ коллайдера
        float randomX = Random.Range(bounds.min.x, bounds.max.x);
        float randomY = Random.Range(bounds.min.y, bounds.max.y);

        return new Vector3(randomX, randomY, 0); // Z = 0, если в 2D
    }
}