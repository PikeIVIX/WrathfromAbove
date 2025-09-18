using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BreakOut : MonoBehaviour
{
    public int breakCount = 0;
    public Animator animator;
    public Animator targetAnimator;
    public GameObject button;
    public GameObject dial;
    public float timer = 0f;
    public GameObject audio;
    public bool letGo = false;
    public bool nextScene = false;

    // Start is called before the first frame update
    void Start()
    {
        Animator animator = GetComponent<Animator>();
        timer = 0f;
         
    }

    // Update is called once per frame
    void Update()
    {
        
        timer += Time.deltaTime;
        Dialogue dialScript = dial.GetComponent<Dialogue>();
        if (Input.GetKeyDown(KeyCode.A))
        {
            animator.SetBool("isBreaking", true);
            breakCount++;
            timer = 0f;
            StartCoroutine(sweatySmashing());
            letGo = false;
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            animator.SetBool("isBreaking", false);
            letGo = true;

        }

        if (breakCount >= 10 && !nextScene) 
        {
            animator.SetBool("isBroke", true);
            Destroy(button);
            nextScene = true;
            Debug.Log("True");

            LoadNextScene(); 
        }

        if (timer >= 10)
        {
            targetAnimator.Play("ManYell");
            dial.SetActive(true);
            dialScript.CheckDialogueFinished(dialScript.finishLine);
        }
        else
        {
            targetAnimator.Play("HouseClose");
            dialScript.ClearDialogue();
            dial.SetActive(false);
        }
    }

    public void LoadNextScene()
    {
        if (nextScene == true)
        {
            StartCoroutine(LoadWait());
        }
    }

    IEnumerator sweatySmashing()
    {
        audioManager audioPlay = audio.GetComponent<audioManager>();
        AudioSource dj = audio.GetComponent<AudioSource>();

        audioPlay.PlaySound(1);
        yield return new WaitForSeconds(1f);
        
        if (letGo == true)
        {
            dj.Stop();
        }
    }

    

    IEnumerator LoadWait()
    {
        Debug.Log("Loading");
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Day2");
    }
}
