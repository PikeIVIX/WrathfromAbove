using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BuffaloHealer : MonoBehaviour
{

    [Header("References")]
    public Animator animator;
    public AudioSource audioSource;      // 🔊 Audio source on buffalo
    public AudioClip eatClip;            // one sound for both normal & rotten corn

    void Reset()
    {
        var col = GetComponent<Collider2D>();
        if (col) col.isTrigger = true;
    }

    private void PlaySound(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        var corn = other.GetComponent<DraggableCorn>();
        if (corn != null && corn.IsBeingCarried && !corn.consumed)
        {
            GameObject buffalo = GameObject.FindWithTag("Player");

            if (buffalo != null)
            {
                // 2. Get the script component
                BuffaloController buff_HP = buffalo.GetComponent<BuffaloController>();

                if (buff_HP != null)
                {
                    buff_HP.HealDamage(25);
                }
                else
                {
                    Debug.LogError("MyTargetScript not found on GameObject with tag 'MyTargetTag'.");
                }
            }
        }
        // Play eating sound (same for both)
        PlaySound(eatClip);
        Destroy(other.gameObject);
    }
}
