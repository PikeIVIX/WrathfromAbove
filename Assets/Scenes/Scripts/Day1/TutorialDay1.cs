using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class TutorialDay1 : MonoBehaviour
{
    [Header("UI")]
    public GameObject tutorialPanel;    
    public CanvasGroup canvasGroup;      

    [Header("Fade")]
    public bool useFade = true;
    public float fadeDuration = 0.6f;

    [Header("Behavior")]
    public KeyCode startKey = KeyCode.Space;

    [Header("Events")]
    public UnityEvent OnTutorialStarted; 

    
    float originalFixedDelta;

    bool started = false;
    bool waitingForInput = true;

    [Header("Other UI")]
    public Canvas growthCanvas;

    
    void Awake()
    {
        originalFixedDelta = Time.fixedDeltaTime;
    }

    void Start()
    {
        if (growthCanvas != null)
            growthCanvas.enabled = false;

        if (tutorialPanel != null) tutorialPanel.SetActive(true);

        if (canvasGroup != null)
        {
            canvasGroup.alpha = 1f;
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        }

        
        Time.timeScale = 0f;
        Time.fixedDeltaTime = originalFixedDelta * Time.timeScale;

        started = false;
        waitingForInput = true;
    }

    void Update()
    {
        if (!waitingForInput || started) return;

        if (Input.GetKeyDown(startKey))
        {
            waitingForInput = false;
            StartCoroutine(DoStartRoutine());
        }
    }

    IEnumerator DoStartRoutine()
    {
        
        if (useFade && canvasGroup != null && fadeDuration > 0f)
        {
            float t = 0f;
            float from = canvasGroup.alpha;
            while (t < fadeDuration)
            {
                t += Time.unscaledDeltaTime;
                canvasGroup.alpha = Mathf.Lerp(from, 0f, t / fadeDuration);
                yield return null;
            }
            canvasGroup.alpha = 0f;
        }
        else if (canvasGroup != null)
        {
            canvasGroup.alpha = 0f;
        }

        
        if (tutorialPanel != null) tutorialPanel.SetActive(false);

        if (growthCanvas != null)
            growthCanvas.enabled = true;



        Time.timeScale = 1f;
        Time.fixedDeltaTime = originalFixedDelta;

        started = true;

      

        OnTutorialStarted?.Invoke();
    }

    void OnDestroy()
    {
        
        if (Time.timeScale == 0f)
        {
            Time.timeScale = 1f;
            Time.fixedDeltaTime = originalFixedDelta;
        }
    }

    
    public bool HasStarted => started;
}