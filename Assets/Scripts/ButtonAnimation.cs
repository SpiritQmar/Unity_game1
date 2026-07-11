using UnityEngine;
using UnityEngine.UI;

public class ButtonPrefabSpawner : MonoBehaviour
{
    public GameObject prefab;
    public GameObject parentObject;
    public float destroyDelay = 2f;

    private Button button;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(SpawnPrefab);
    }

    private void SpawnPrefab()
    {
        GameObject spawnedPrefab = Instantiate(prefab, transform.position, Quaternion.identity);

        spawnedPrefab.transform.SetParent(parentObject.transform);

        Destroy(spawnedPrefab, destroyDelay);
    }
}
