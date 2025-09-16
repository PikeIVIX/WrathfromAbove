using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class day4corn : MonoBehaviour
{
    [Header("Corn Prefabs")]
    public GameObject freshCornPrefab;

    [Header("Cooldown Settings")]
    public float cooldownSeconds = 5f;

    private bool isOnCooldown = false;
    private SpriteRenderer sr;

    public Animator animator;

    private int cornCount = 0;

    [Header("Sound")]
    public AudioClip pickupSound;
    private AudioSource audioSource;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        Animator animator = GetComponent<Animator>();
    }

    void OnMouseDown()
    {
        animator.SetBool("isRotting", true);
        StartCoroutine(CornRotTimer());

        if (isOnCooldown) return;

        if (pickupSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(pickupSound);
        }


        GameObject prefabToSpawn = freshCornPrefab;
        if (prefabToSpawn == null)
        {
            Debug.LogError("Corn prefab is not assigned!");
            return;
        }


        Vector3 spawnPos = transform.position + new Vector3(0, 1.5f, 0);
        spawnPos.z = 0f;
        GameObject corn = Instantiate(prefabToSpawn, spawnPos, Quaternion.identity);
        Debug.Log("Spawned corn: " + corn.name);


        var cornSr = corn.GetComponent<SpriteRenderer>();
        if (cornSr != null)
        {
            cornSr.sortingLayerName = "Default";
            cornSr.sortingOrder = 999;
            cornSr.color = Color.white;
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Corn"))
        {
            cornCount++;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Corn"))
        {
            cornCount = Mathf.Max(0, cornCount - 1);
        }
    }

    IEnumerator CornRotTimer()
    {
        yield return new WaitForSeconds(1f);

        Destroy(gameObject);
        Debug.Log("yeet");

    }

}
