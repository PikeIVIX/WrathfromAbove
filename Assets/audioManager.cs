using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioManager : MonoBehaviour
{
    public AudioSource audioSource;      // 🔊 Audio source on buffalo
    public List<AudioClip> audioClips;
    public int index = 0;
    public bool stop = false;

    void Update()
    {
        if (stop)
        {
            StartCoroutine(StopSound(1));
        }
    }

    // Start is called before the first frame update
    public void PlaySound(int index)
    {
        if (index >= 0 && index < audioClips.Count && audioSource != null)
        {
            audioSource.clip = audioClips[index];
            audioSource.Play();
        }
    }

    public IEnumerator StopSound(float delay)
    {
        yield return new WaitForSeconds(delay); // Wait for the specified delay
        audioSource.Stop(); // Stop the audio
        stop = false;
    }
}
