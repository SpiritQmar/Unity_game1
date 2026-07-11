using UnityEngine;
using System.Collections;

public class SpawnObjects : MonoBehaviour
{
    public GameObject prefabToSpawn;
    public BoxCollider2D spawnArea;
    public Transform background;
    public float spawnIntervalMin = 3f;
    public float spawnIntervalMax = 10f;

    void Start()
    {
        StartCoroutine(SpawnObject());
    }

    IEnumerator SpawnObject()
    {
        while (true)
        {
            float waitTime = Random.Range(spawnIntervalMin, spawnIntervalMax);
            yield return new WaitForSeconds(waitTime);

            Vector3 spawnPosition = GetRandomPositionInCollider();

            GameObject spawnedObject = Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);

            spawnedObject.transform.SetParent(background, false);
        }
    }

    private Vector3 GetRandomPositionInCollider()
    {
        Bounds bounds = spawnArea.bounds;

        float randomX = Random.Range(bounds.min.x, bounds.max.x);
        float randomY = Random.Range(bounds.min.y, bounds.max.y);

        return new Vector3(randomX, randomY, 0);
    }
}