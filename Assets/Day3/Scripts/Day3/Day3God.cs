using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Day3God : MonoBehaviour
{
    public GameObject canvas;
    public TMP_Text ScoreObj;
    public AudioSource audioSource;      // 🔊 Audio source on buffalo
    public AudioClip combo;
    public AudioClip buffaloHurt;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.anyKeyDown)
        {
            PlaySound(combo);
        }

    }


    private void PlaySound(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }

    public void BuffaloHurt()
    {
        PlaySound(buffaloHurt);
    }

}
