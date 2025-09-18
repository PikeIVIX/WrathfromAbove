using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BuffaloControllerD : MonoBehaviour
{
    public Animator animator;
    public HPBar healthBarUI;
    public GameObject audio;

    void Start()
    {
        Animator animator = GetComponent<Animator>();

    }

    void Update()
    {
        if (GlobalPlayer.isFighting)
        {
            animator.SetBool("isAttack", true);

        }
        else
        {
            animator.SetBool("isAttack", false);
        }

        if (GlobalPlayer.day4score >= 300)
        {
            SceneManager.LoadScene("EndingDialogue");
        }
    }

    void Reset()
    {
        var col = GetComponent<Collider2D>();
        if (col) col.isTrigger = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("NPC"))
        {
            animator.SetBool("isSad", true);
            StartCoroutine(ResetBuff());
        }

    }

    private IEnumerator ResetBuff()
    {
        yield return new WaitForSeconds(1f);
        animator.SetBool("isSad", false);
        animator.SetBool("isEatingHappy", false);
    }

    public void TakeDamage(int damageAmount)
    {
        Day3God buffsound = audio.GetComponent<Day3God>();
        buffsound.BuffaloHurt();
        GlobalPlayer.current_hitpoints -= damageAmount;
        GlobalPlayer.current_hitpoints = Mathf.Clamp(GlobalPlayer.current_hitpoints, 0, GlobalPlayer.max_hitpoints); // Ensure health stays within bounds
        animator.SetBool("isSad", true);
        StartCoroutine(ResetBuff());


        // Update the health bar
        if (healthBarUI != null)
        {
            healthBarUI.UpdateHealthBar(GlobalPlayer.current_hitpoints, GlobalPlayer.max_hitpoints);
        }

        if (GlobalPlayer.current_hitpoints <= 0)
        {
            Debug.Log("Player Died!");
            SceneManager.LoadScene("BadEnding");
        }
        Debug.Log(GlobalPlayer.current_hitpoints);
    }

    public void HealDamage(int healAmount)
    {
        if (GlobalPlayer.current_hitpoints < GlobalPlayer.max_hitpoints)
        {
            GlobalPlayer.current_hitpoints += healAmount;
            GlobalPlayer.current_hitpoints = Mathf.Clamp(GlobalPlayer.current_hitpoints, 0, GlobalPlayer.max_hitpoints);
        }

        animator.SetBool("isEatingHappy", true);
        StartCoroutine(ResetBuff());

        if (healthBarUI != null)
        {
            healthBarUI.UpdateHealthBar(GlobalPlayer.current_hitpoints, GlobalPlayer.max_hitpoints);
        }
        Debug.Log(GlobalPlayer.current_hitpoints);

    }


}