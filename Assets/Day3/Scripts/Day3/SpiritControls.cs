using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritControls : MonoBehaviour
{
    public Transform target; // The object to move towards
    private float speed = 1f;
    public float minSpeed = 0f;
    public float maxSpeed = 5f;
    int randomLayer = 0;
    public List<int> randComboList = new List<int>();
    public List<GameObject> comboToSpawn;
    public Vector3[] spawnLocalPositions;
    public Transform ghost;
    private int sequenceIndex = 0;
    public List<KeyCode> keyCombo = new List<KeyCode>();
    public List<GameObject> tempCombo = new List<GameObject>();
    public GameObject audio;
    public GameObject audio2;

    // Start is called before the first frame update
    void Start()
    {
        
        GenerateRandomCombo();
        SpawnCombo();
        speed = Mathf.Ceil(Random.Range(minSpeed, maxSpeed));
        randomLayer = Random.Range(6, 9);
        gameObject.layer = randomLayer;
       
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        checkCombo();
        
    }

    void OnTriggerEnter2D (Collider2D other)
    {
        Day3God buffsound = audio.GetComponent<Day3God>();
        // Check if the entering GameObject has the "blocker" tag
        if (other.CompareTag("Player"))
        {
            buffsound.BuffaloHurt();
            GlobalPlayer.day3score -= 20;
            Destroy(gameObject);
        }

    }

    public void GenerateRandomCombo()
    {
        // Clear the list to ensure it's fresh if called multiple times
        randComboList.Clear();

        for (int i = 0; i < 4; i++)
        {
            int randomNumber = Random.Range(0, 4); // For integers
                                                   // float randomNumber = Random.Range((float)minRange, (float)maxRange); // For floats
            randComboList.Add(randomNumber);

            if (randomNumber == 0)
            {
                keyCombo.Add(KeyCode.W);
            }
            else if (randomNumber == 1)
            {
                keyCombo.Add(KeyCode.A);
            }
            else if (randomNumber == 2)
            {
                keyCombo.Add(KeyCode.S);
            }
            else if (randomNumber == 3)
            {
                keyCombo.Add(KeyCode.D);
            }
        }
    }

    public void SpawnCombo()
    {
        // Iterate through the list of objects to spawn
        for (int i = 0; i < randComboList.Count; i++)
        {
            if (comboToSpawn[i] != null)
            {
                // Calculate the local position relative to the parent
                Vector3 localSpawnPosition = Vector3.zero;
                if (spawnLocalPositions != null && i < spawnLocalPositions.Length)
                {
                    localSpawnPosition = transform.position + spawnLocalPositions[i];
                }

                // Instantiate the object as a child of the parentTransform
                // The position and rotation arguments are local when a parent is provided
                GameObject spawnedCombo = Instantiate(comboToSpawn[randComboList[i]], localSpawnPosition, Quaternion.identity);

                SpriteRenderer comboSprite = spawnedCombo.GetComponent<SpriteRenderer>();

                comboSprite.sortingOrder = 5 + i;

                tempCombo.Add(spawnedCombo);

                if (spawnedCombo != null)
                {
                    spawnedCombo.transform.SetParent(ghost);
                }
                
            }

            
            
        }
    }

    public void checkCombo()
    {
        audioManager dj = audio2.GetComponent<audioManager>();

        if (Input.GetKeyDown(keyCombo[sequenceIndex]))
        {
            tempCombo[sequenceIndex].SetActive(false);
            sequenceIndex++;

            if (sequenceIndex == keyCombo.Count)
            {
                dj.PlaySound(0);
                dj.stop = true;
                GlobalPlayer.day3score += 10;
                Destroy(gameObject);
            }
        }
        else if (Input.anyKeyDown && Input.inputString != "") // Check if any key was pressed (and it's not just a modifier key)
        {
            
            sequenceIndex = 0; // Reset if wrong key pressed
            foreach (GameObject obj in tempCombo)
            {
                obj.SetActive(true);
            }
        }
    }

}
