using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Prolouge1 : MonoBehaviour
{
    [Header("Animator")]
    public Animator cutsceneAnimator;
    public string phase1Trigger = "Phase1Trigger";
    public string phase2Trigger = "Phase2Trigger";

    [Header("UI (assign one)")]
    public CanvasGroup dialogueCanvasGroup;

    public Text dialogueText;

    [Header("Press prompt")]
    public GameObject pressSpaceObject;

    [Header("Dialogue")]
    [TextArea(2, 4)] public string phase1Text = "For generations, buffalo walked with farmers, plowing the land and bringing harvests to life.";
    [TextArea(2, 4)] public string phase2Text = "But as time passed, machines took their place, and the rhythm of work—and life—changed forever.";

    [Header("Timings")]
    public float fadeInDuration = 0.6f;
    public float fadeOutDuration = 0.4f;
    public float timeBeforePrompt = 0.5f;
    public bool inPhase2 = false;

    bool waitingForInput = false;

    void Start()
    {
        if (dialogueCanvasGroup != null) dialogueCanvasGroup.alpha = 0f;
        if (pressSpaceObject != null) pressSpaceObject.SetActive(false);

        StartCoroutine(PlayPhase1());
    }

    void Update()
    {
        if (waitingForInput && Input.GetKeyDown(KeyCode.Space))
        {
            if (!inPhase2)
            {
                // First Space → trigger Phase 2
                waitingForInput = false;
                if (pressSpaceObject != null) pressSpaceObject.SetActive(false);
                StartCoroutine(PlayPhase2());
                inPhase2 = true;
            }
            else
            {
                // Second Space → load Day1
                SceneManager.LoadScene("Day1");
            }
        }
    }

    IEnumerator PlayPhase1()
    {
        if (cutsceneAnimator != null && !string.IsNullOrEmpty(phase1Trigger))
            cutsceneAnimator.SetTrigger(phase1Trigger);

        SetDialogueText(phase1Text);
        yield return StartCoroutine(FadeCanvas(dialogueCanvasGroup, 0f, 1f, fadeInDuration));

        yield return new WaitForSeconds(timeBeforePrompt);

        if (pressSpaceObject != null) pressSpaceObject.SetActive(true);
        waitingForInput = true;
    }

    IEnumerator PlayPhase2()
    {
        if (cutsceneAnimator != null && !string.IsNullOrEmpty(phase2Trigger))
            cutsceneAnimator.SetTrigger(phase2Trigger);

        
        yield return StartCoroutine(FadeCanvas(dialogueCanvasGroup, 1f, 0f, fadeOutDuration));

        
        SetDialogueText(phase2Text);

        
        yield return StartCoroutine(FadeCanvas(dialogueCanvasGroup, 0f, 1f, fadeInDuration));

        
        yield return new WaitForSeconds(1f);

        if (pressSpaceObject != null) pressSpaceObject.SetActive(true);
        waitingForInput = true;
    }

    IEnumerator FadeCanvas(CanvasGroup cg, float from, float to, float duration)
    {
        if (cg == null) yield break;

        float t = 0f;
        cg.alpha = from;

        while (t < duration)
        {
            t += Time.deltaTime;
            cg.alpha = Mathf.Lerp(from, to, t / duration);
            yield return null;
        }
        cg.alpha = to;
    }

    void SetDialogueText(string s)
    {
#if TMP_PRESENT
        if (dialogueTMP != null) { dialogueTMP.text = s; return; }
#endif
        if (dialogueText != null) { dialogueText.text = s; return; }

        Debug.LogWarning("CutsceneController: No dialogue text assigned.");
    }
}