using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Dialogue : MonoBehaviour
{
    public Text textComponent;
    public string[] lines;
    public float textSpeed;
    public int index = 0;
    private bool talking = false;
    public bool finishLine = false;
    public GameObject audio;

    // Start is called before the first frame update
    void Start()
    {
        textComponent.text = string.Empty;
        finishLine = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartDialogue()
    {
        index = 0;
        if (!talking)
        {
            talking = true;
            StartCoroutine(TypeLine());
        }
        
    }

    IEnumerator TypeLine()
    {
        audioManager audioPlay = audio.GetComponent<audioManager>();
        AudioSource dj = audio.GetComponent<AudioSource>();
        audioPlay.PlaySound(0);
        Debug.Log(lines[index]);
        foreach (char c in lines[index].ToCharArray())
        {
            finishLine = false;
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
        dj.Stop();
        yield return new WaitForSeconds(3f);
        talking = false;
        finishLine = true;
        
    }

    public void NextLine()
    {
        if (index < lines.Length - 1)
        {
            textComponent.text = string.Empty;            
            StartCoroutine(TypeLine());
            index++;
        }
        //else
        //{
        //    gameObject.SetActive(false);
        //}
    }

    public void ClearDialogue()
    {
        StopAllCoroutines();
        textComponent.text = string.Empty;
    }

    public void CheckDialogueFinished(bool line_done)
    {
        if (line_done)
        {
            if (textComponent.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[index];
            }
        }
        
    }
}
