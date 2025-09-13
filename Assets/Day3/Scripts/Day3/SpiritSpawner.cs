using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritSpawner : MonoBehaviour
{
    public GameObject spiritPrefab;
    public Transform targetObj;
    public GameObject AudioManager;
    public Vector2 spawnAreaMinOffset; // Minimum offset from specificObject's position
    public Vector2 spawnAreaMaxOffset; // Maximum offset from specificObject's position
    public static bool isSpawning = true;
    public float spawnDelay = 2f;
    private float nextSpawnTime;
    public float storedX = 0f;

    void Start()
    {
        nextSpawnTime = Time.time + spawnDelay;
    }

    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnSpirits();
            nextSpawnTime = Time.time + spawnDelay;
        }
    }

    void SpawnSpirits()
    {
        float randomX = 0;
        if (spiritPrefab == null || targetObj == null)
        {
            Debug.LogError("Prefab to spawn or specific object is not assigned!");
            return;
        }

        storedX = randomX;

        // Generate random local coordinates within the defined area
        randomX = Random.Range(spawnAreaMinOffset.x, spawnAreaMaxOffset.x);

        if (storedX > 0 && storedX * randomX > 0)
        {
            randomX *= -1;
            storedX = randomX;
        }

        float randomY = Random.Range(spawnAreaMinOffset.y, spawnAreaMaxOffset.y);

        // Calculate world position
        Vector2 spawnPosition = (Vector2)targetObj.transform.position + new Vector2(randomX, randomY);

        // Instantiate the prefab
        //Instantiate(spiritPrefab, spawnPosition, Quaternion.identity);

        GameObject spirit = Instantiate(spiritPrefab, spawnPosition, Quaternion.identity);

        SpiritControls movescript = spirit.GetComponent<SpiritControls>();

        if (movescript != null)
        {
            movescript.target = targetObj;
            movescript.audio = AudioManager;
        }
    }

}
