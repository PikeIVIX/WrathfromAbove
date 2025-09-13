using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboSpawner3 : MonoBehaviour
{
    public GameObject spiritPrefab;
    public Transform targetObj;
    public Vector2 spawnAreaMinOffset; // Minimum offset from specificObject's position
    public Vector2 spawnAreaMaxOffset; // Maximum offset from specificObject's position
    public static bool isSpawning = true;
    public float spawnDelay = 2f;
    private float nextSpawnTime;

    void Start()
    {
        nextSpawnTime = Time.time + spawnDelay;
    }

    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnCombo();
            nextSpawnTime = Time.time + spawnDelay;
        }
    }

    void SpawnCombo()
    {
        if (spiritPrefab == null || targetObj == null)
        {
            Debug.LogError("Prefab to spawn or specific object is not assigned!");
            return;
        }

        // Generate random local coordinates within the defined area
        float randomX = Random.Range(spawnAreaMinOffset.x, spawnAreaMaxOffset.x);
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
        }
    }

}
