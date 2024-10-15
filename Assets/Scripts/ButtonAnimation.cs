using UnityEngine;
using UnityEngine.UI;

public class ButtonPrefabSpawner : MonoBehaviour
{
    public GameObject prefab; // Префаб, который будет создаваться
    public GameObject parentObject; // Объект, которому будет принадлежать созданный префаб
    public float destroyDelay = 2f; // Время перед удалением префаба

    private Button button;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(SpawnPrefab);
    }

    private void SpawnPrefab()
    {
        // Создание префаба в позиции кнопки
        GameObject spawnedPrefab = Instantiate(prefab, transform.position, Quaternion.identity);

        // Установка родительского объекта
        spawnedPrefab.transform.SetParent(parentObject.transform);

        // Удаление префаба через заданное время
        Destroy(spawnedPrefab, destroyDelay);
    }
}
