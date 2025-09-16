using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboOrb : MonoBehaviour
{
    public static ComboOrb Instance { get; private set; }

    public List<int> randComboList = new List<int>();
    public List<GameObject> comboToSpawn;
    public Vector3[] spawnLocalPositions;
    private int sequenceIndex = 0;
    public List<KeyCode> keyCombo = new List<KeyCode>();
    public List<GameObject> tempCombo = new List<GameObject>();
    public GameObject audio;
    public bool isAttacking = false;
    public int win_counter = 0;
    public GodBuffSpam enduranceScript;
    public bool repeat = false;

    // Start is called before the first frame update
    void Start()
    {

        GenerateRandomCombo();
        StartCoroutine(CycleCount());
        //SpawnCombo();

    }

    // Update is called once per frame
    void Update()
    {
        if (!isAttacking)
        {

            if (!repeat)
            {
                foreach (GameObject obj in tempCombo)
                {
                    obj.SetActive(false);

                }
                deleteCombo();

                GenerateRandomCombo();
                StartCoroutine(CycleCount());

            }
                
            checkCombo();
            

        }
        else
        {
            foreach (GameObject obj in tempCombo)
            {
                obj.SetActive(false);
                
            }
            deleteCombo();
        }
            
    }

    public void GenerateRandomCombo()
    {
        // Clear the list to ensure it's fresh if called multiple times
        randComboList.Clear();
        keyCombo.Clear();

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

        SpawnCombo();
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
                    localSpawnPosition = spawnLocalPositions[i];
                }

                // Instantiate the object as a child of the parentTransform
                // The position and rotation arguments are local when a parent is provided
                GameObject spawnedCombo = Instantiate(comboToSpawn[randComboList[i]], localSpawnPosition, Quaternion.identity);

                SpriteRenderer comboSprite = spawnedCombo.GetComponent<SpriteRenderer>();

                comboSprite.sortingOrder = 5 + i;

                tempCombo.Add(spawnedCombo);

            }

        }
        repeat = true;
    }

    public void checkCombo()
    {
        if (Input.GetKeyDown(keyCombo[sequenceIndex]))
        {
            tempCombo[sequenceIndex].SetActive(false);
            sequenceIndex++;

            if (sequenceIndex == keyCombo.Count)
            {
                deleteCombo();
                GlobalPlayer.day4score += 10;
                GlobalPlayer.day4threshold += 1;
                GenerateRandomCombo();
                sequenceIndex = 0;

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

    public void deleteCombo()
    {
        foreach (GameObject obj in tempCombo)
        {
            Destroy(obj);
        }

        foreach (GameObject obj in tempCombo)
        {
            Destroy(obj);
        }
        tempCombo.Clear();
    }

    IEnumerator CycleCount()
    {
        yield return new WaitForSeconds(10f);

        Debug.Log("yeet");

        if (GlobalPlayer.day4threshold >= GlobalPlayer.day4_win_con)
        {
            win_counter += 1;
            GlobalPlayer.day4threshold = 0;
            Debug.Log(win_counter);
        }
        else
        {
            isAttacking = true;
            repeat = false;
            GlobalPlayer.isFighting = true;
            Debug.Log("Yo");
            enduranceScript.ChooseNewKey();
        }
    }
}
