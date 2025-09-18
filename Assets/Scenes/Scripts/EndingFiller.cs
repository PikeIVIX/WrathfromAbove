using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingFiller : MonoBehaviour
{
    public float scene_time = 7f;
    private float timer = 0f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;


        if (timer >= scene_time)
        {
            SceneManager.LoadScene("GoodEnding");
        }
    }


    
}
